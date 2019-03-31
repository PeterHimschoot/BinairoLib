using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  /// <summary>
  /// Look for singleHoles such as 0X0 or 1X1 and fill the hole
  /// </summary>
  public class HoleAtStartSolver : IRowSolver
  {
    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      // check if hole is at start
      ushort firstOneMask = 0b1000_0000_0000_0000;
      ushort fullMask = size.ToMask();
      if ((mask | firstOneMask) == fullMask)
      {
        var bitCounter = new BitCounter();
        int countOnes = bitCounter.CountOnes(row, size);
        if (countOnes < size / 2)
        {
          row |= firstOneMask;
        }
        mask |= firstOneMask;
        return true;
      }
      return false;
    }
  }
}
