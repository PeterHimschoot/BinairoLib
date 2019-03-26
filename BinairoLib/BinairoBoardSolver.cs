using System.Linq;

namespace BinairoLib
{
  public class BinairoBoardSolver
  {
    private readonly int completeMask;
    private int size;
    private BinairoBoardChecker checker;
    private BinairoRowSolver rowSolver;
    private MatrixFlipper flipper;
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

      var columns = new ushort[size];
      var colMasks = new ushort[size];

      while (!BoardIsComplete(masks))
      {
        int rowSolvers = 1;
        while (rowSolvers > 0)
        {
          rowSolvers = 0;
          // Solve rows
          bool solving = true;
          while (solving)
          {
            for (int iRow = 0; iRow < size; iRow += 1)
            {
              solving = rowSolver.Solve(ref rows[iRow], ref masks[iRow], size);
              if (solving) rowSolvers += 1;
            }
          }
          // Solve columns
          flipper.Flip(rows, ref columns, size);
          flipper.Flip(masks, ref colMasks, size);
          solving = true;
          while (solving)
          {
            for (int iRow = 0; iRow < size; iRow += 1)
            {
              solving = rowSolver.Solve(ref columns[iRow], ref colMasks[iRow], size);
              if (solving) rowSolvers += 1;
            }
          }
          flipper.Flip(columns, ref rows, size);
          flipper.Flip(colMasks, ref masks, size);
        }
        Output.PrintBoard(rows, masks, size);
        return false;
      }
      return true;
    }

    public bool BoardIsComplete(ushort[] masks)
      => masks.All(mask => (mask & this.completeMask) == this.completeMask);
  }
}
