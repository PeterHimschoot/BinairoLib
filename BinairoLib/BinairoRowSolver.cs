using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public interface IRowSolver
  {
    bool Solve(ref ushort row, ref ushort mask, int size);
  }

  public class BinairoRowSolver : IRowSolver
  {
    public BinairoRowSolver()
    {
      this.Add(new OpenStartDuosSolver());
      this.Add(new OpenEndDuosSolver());
      this.Add(new HoleAtStartSolver());
      this.Add(new HoleAtEndSolver());
      this.Add(new SingleHoleSolver());
      this.Add(new ZerosAllDoneSolver());
      this.Add(new OnesAllDoneSolver());
      this.Add(new LastMissingSolver());
    }

    private List<IRowSolver> solvers = new List<IRowSolver>();

    public void Add(IRowSolver rowSolver)
      => solvers.Add(rowSolver);

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      int solved = 0;
      bool solving = false;
      do
      {
        foreach (var solver in solvers)
        {
          solving = solver.Solve(ref row, ref mask, size);
          if (solving) solved += 1;
        }
      } while (solving);
      return solved > 0;
    }
  }

}
