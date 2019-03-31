using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BinairoLib
{
  public interface IRowSolver
  {
    bool Solve(ref ushort row, ref ushort mask, int size);
  }

  public class BinairoRowSolver : IRowSolver
  {
    private ushort allDoneMask;
    public IOutputHelper Output { get; set; }
    public BinairoRowChecker RowChecker { get; set; }

    public BinairoRowSolver(int size)
    {
      allDoneMask = size.ToMask();
      BitCounter bitCounter = new BitCounter();

      this.Add(new OpenStartDuosSolver());
      this.Add(new OpenEndDuosSolver());
      this.Add(new HoleAtStartSolver());
      this.Add(new HoleAtEndSolver());
      this.Add(new SingleHoleSolver());
      this.Add(new ZerosAllDoneSolver(bitCounter));
      this.Add(new OnesAllDoneSolver(bitCounter));
      this.Add(new ThreeHoleSolver(bitCounter));
    }

    private List<IRowSolver> solvers = new List<IRowSolver>();

    public void Add(IRowSolver rowSolver)
      => solvers.Add(rowSolver);

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      // Nothing to do for rows that have been solved
      if ((mask & allDoneMask) == allDoneMask) return false;
      // Keep track of changes
      ushort originalMask = mask;
      do
      {
        ushort prevMask = mask;
        foreach (var solver in solvers)
        {
          Output?.PrintRow(row, mask, size, $">>>>>> {"{0}"} ({solver.GetType().FullName})");
          bool solved =  solver.Solve(ref row, ref mask, size);
          if (solved)
          {
            Output?.PrintRow(row, mask, size, $"<<<<<< {"{0}"} ({solver.GetType().FullName})");
          }
        }
        // stop when no more changes
        if (prevMask == mask) break;
      } while (true);
      return originalMask != mask; // return true if any solutions were found
    }
  }

}
