using System.Diagnostics;
using System.Linq;

namespace BinairoLib
{
  public class BinairoBoardSolver
  {
    private readonly int completeMask;
    private readonly int size;
    private readonly BinairoBoardChecker checker;
    private readonly BinairoRowSolver rowSolver;
    private readonly MatrixFlipper flipper;
    private ushort[] rows;
    private ushort[] masks;

    public IOutputHelper Output { get; set; }

    public BinairoBoardSolver(BinairoBoardChecker checker, BinairoRowSolver rowSolver, MatrixFlipper flipper, int size)
    {
      this.completeMask = size.ToMask();
      this.size = size;
      this.checker = checker;
      this.rowSolver = rowSolver;
      this.flipper = flipper;
    }

    public bool Solve(ushort[] rows, ushort[] masks)
    {
      this.rows = rows;
      this.masks = masks;

      ushort[] columns = new ushort[this.size];
      ushort[] colMasks = new ushort[this.size];

      //int iterations = 4;
      while (!BoardIsComplete(masks))
      {
        Output.PrintBoard(rows, masks, this.size);
        int rowSolvers = 1;
        while (rowSolvers > 0)
        {
          rowSolvers = 0;
          // Solve rows
          bool solving = true;
          while (solving)
          {
            solving = false;
            for (int iRow = 0; iRow < this.size; iRow += 1)
            {
              Debug.WriteLine($"{iRow} - ROW {rows[iRow].ToBinaryString(masks[iRow])}");
              solving = solving || this.rowSolver.Solve(ref rows[iRow], ref masks[iRow], this.size);
            }
            if (solving)
            {
              rowSolvers += 1;
            }
          }
          Output.PrintBoard(rows, masks, this.size);
          // Solve columns
          this.flipper.Flip(rows, ref columns, this.size);
          this.flipper.Flip(masks, ref colMasks, this.size);
          solving = true;
          while (solving)
          {
            solving = false;
            for (int iRow = 0; iRow < this.size; iRow += 1)
            {
              solving = solving || this.rowSolver.Solve(ref columns[iRow], ref colMasks[iRow], this.size);
            }
            if (solving)
            {
              rowSolvers += 1;
            }
          }
          this.flipper.Flip(columns, ref rows, this.size);
          this.flipper.Flip(colMasks, ref masks, this.size);
          Output.PrintBoard(rows, masks, this.size);
          //iterations -= 1;
          //if (iterations <= 0) return false;
        }
        return false;
      }
      return true;
    }

    public bool BoardIsComplete(ushort[] masks)
      => masks.All(mask => (mask & this.completeMask) == this.completeMask);
  }
}
