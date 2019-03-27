using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  /// <summary>
  /// Fill the one missing element
  /// </summary>
  public class LastMissingSolver : IRowSolver
  {
    private BitCounter counter = new BitCounter();

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      int ones = counter.CountOnes(row, size, mask, includeHoles: true);
      int zeros = counter.CountZeros(row, size, mask, includeHoles: true);
      ushort sizeMask = size.ToMask();
      if (ones + zeros + 1 == size)
      {
        // we have a match, now find the missing element
        ushort missingPattern = 0b1010_0000_0000_0000;
        ushort missingMask = 0b1110_0000_0000_0000;
        for (int i = 0; i < size; i += 1)
        {
          if ((mask & missingMask) == missingPattern)
          {
            // we found the missing element!
            ushort missingBit = (ushort)(0b1000_0000_0000_0000 >> i + 1);
            if (ones < zeros)
            {
              row |= missingBit;
            }
            mask |= missingBit;
            return true;
          }
          missingPattern = (ushort) ((missingPattern >> 1) & sizeMask);
          missingMask = (ushort) ((missingMask >> 1) & sizeMask);
        }
      }
      return false;
    }
  }
}
