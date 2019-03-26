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
      ushort missingBit = (ushort) (0b1000_0000_0000_0000 >> (size-1));
      ushort missingMask = (size-1).ToMask();
      ushort sizeMask = size.ToMask();

      for (int i = 0; i < size; i += 1)
      {
        if (mask == missingMask)
        {
          var ones = counter.CountOnes(row, size);
          if (ones < size / 2)
          {
            row |= (ushort)(~missingMask & sizeMask);
          }
          mask |= (ushort)(~missingMask & sizeMask);
          // found it!
          return true;
        }
        missingMask = (ushort)((missingMask <<= 1) | missingBit);
      }
      return false;
    }
  }
}
