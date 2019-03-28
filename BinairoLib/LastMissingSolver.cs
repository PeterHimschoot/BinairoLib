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
        // we have a match, now find the missing 1-size hole
        ushort missingPattern = 0b1010_0000_0000_0000;
        ushort missingMask = 0b1110_0000_0000_0000;
        for (int i = 0; i < size-1; i += 1)
        {
          if ((mask & missingMask) == missingPattern)
          {
            // we found the missing element!
            ushort updateRow = (ushort)(0b0100_0000_0000_0000 >> i);
            if (ones < zeros)
            {
              row |= updateRow;
            }
            mask |= updateRow /*updateMask*/;
            return true;
          }
          missingPattern = (ushort)((missingPattern >> 1) & sizeMask);
          missingMask = (ushort)((missingMask >> 1) & sizeMask);
        }
      }
      else if (ones + 1 == size / 2)
      {
        // Look for a 3-size hole, with zeros at the edge
        ushort missingPattern = 0b1000_1000_0000_0000;
        ushort missingMask = 0b1111_1000_0000_0000;
        for (int i = 0; i < size - 5; i += 1)
        {
          if ((mask & missingMask) == missingPattern)
          {
            if ((row & missingPattern) == missingPattern)
            {
              ushort updateRow = (ushort)(0b0010_0000_0000_0000 >> i);
              row |= updateRow;
              ushort updateMask = (ushort)(0b0111_0000_0000_0000 >> i);
              mask |= updateMask;
              return true;
            }
          }
          missingPattern >>= 1;
          missingMask >>= 1;
        }
      }
      else if (zeros + 1 == size / 2)
      {
        // Look for a 3-size hole, with ones at the edge
        ushort missingPattern = 0b1000_1000_0000_0000;
        ushort missingMask = 0b1111_1000_0000_0000;
        for (int i = 0; i < size - 5; i += 1)
        {
          if ((mask & missingMask) == missingPattern)
          {
            if ((row & missingPattern) == 0b0000_0000_0000_0000)
            {
              ushort updateRow = (ushort)(0b0101_0000_0000_0000 >> i);
              row |= updateRow;
              ushort updateMask = (ushort)(0b0111_0000_0000_0000 >> i);
              mask |= updateMask;
              return true;
            }
          }
          missingPattern >>= 1;
          missingMask >>= 1;
        }
      }
      return false;
    }
  }
}
