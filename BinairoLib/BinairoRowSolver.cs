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
    private List<IRowSolver> solvers = new List<IRowSolver>();

    public void Add(IRowSolver rowSolver)
      => solvers.Add(rowSolver);

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      bool solving = false;
      do
      {
        foreach (var solver in solvers)
        {
          solving = solver.Solve(ref row, ref mask, size);
        }
      } while (solving);
      return false;
    }
  }
  /// <summary>
  /// Look for open duo's like x00x or x11x, and add the missing
  /// </summary>
  public class OpenDuosSolver : IRowSolver
  {
    // row    11
    // mask   110
    // xor    001
    //        001


    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      ushort originalRow = row;
      ushort twoZeros = 0b0000_0000_0000_0000;
      ushort newOne = 0b0010_0000_0000_0000;
      ushort twoOnes = 0b1100_0000_0000_0000;
      ushort newZero = 0b1101_1111_1111_1111;
      ushort twoMask = 0b0010_0000_0000_0000;
      for (int i = 0; i < size-2; i += 1)
      {
        //First check if there is a missing element
        if ((mask & twoMask) == twoMask)
        {
          if ((row & twoZeros) == twoZeros)
          {
            row |= newOne; // Fill in missing one
            mask |= twoMask;
          }
          else if ((row & twoOnes) == twoOnes)
          {
            row &= newZero; // set 0
            mask |= twoMask;
          }
        }
        twoZeros >>= 1;
        newOne >>= 1;
        twoOnes >>= 1;
        newZero >>= 1;
        twoMask >>= 1;
      }
      return !(originalRow == row);
    }
  }
}
