using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  /// <summary>
  /// Look for open duo's like x00 or x11, and add the missing
  /// </summary>
  public class OpenStartDuosSolver : IRowSolver
  {
    private static ushort leadingOne = 0b1000_0000_0000_0000;

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      row &= mask; // make sure all zeros where mask is zero
      ushort originalMask = mask;
      ushort twoZeros = 0b0110_0000_0000_0000;
      ushort twoOnes = 0b0110_0000_0000_0000;
      ushort twoMask = 0b1000_0000_0000_0000;
      for (int i = 0; i < size - 2; i += 1)
      {
        //First check if there is a missing element
        if (((~mask & twoMask) == twoMask) && ((mask & twoZeros) == twoZeros))
        {
          // mask indicates there is nothing yet
          if ((row & twoZeros) == 0)
          {
            // yes, two zero's in a row!
            row |= twoMask; // Fill in missing one
            mask |= twoMask;
          }
          else if ((row & twoOnes) == twoOnes)
          {
            // yes, two one's in a row!
            // no need to clear out zero, look at first line of method
            mask |= twoMask;
          }
        }
        twoZeros >>= 1;
        twoOnes >>= 1;
        twoMask >>= 1;
      }
      return originalMask != mask;
    }
  }
}
