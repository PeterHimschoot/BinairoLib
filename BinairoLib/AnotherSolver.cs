using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  /// <summary>
  /// 001X0110
  /// </summary>
  public class AnotherSolver : IRowSolver
  {
    private static ushort leadingOne = 0b1000_0000_0000_0000;

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      return false;
    }
  }
}
