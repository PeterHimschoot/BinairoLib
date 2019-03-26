using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  /// <summary>
  /// Look for singleHoles such as 0X0 or 1X1 and fill the hole
  /// </summary>
  public class SingleHoleSolver : IRowSolver
  {
    private static ushort leadingOne = 0b1000_0000_0000_0000;

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      ushort originalMask = mask;
      ushort Pattern = 0b1110_0000_0000_0000;
      ushort Ones = 0b1010_0000_0000_0000;
      ushort Zeros = 0b0000_0000_0000_0000;
      ushort UpdateMask = 0b0100_0000_0000_0000;

      for(int i = 0; i < size; i += 1)
      {
        // mask = 101
        // Ones = 101
        if( (mask & Pattern) == Ones) {
          if ((row & Pattern) == Ones)
          {
            // put 0 in the middle
            mask |= UpdateMask;
          }
          else if ((row & Pattern) == Zeros)
          {
            // put 1 in the middle
            row |= UpdateMask;
            mask |= UpdateMask;
          }
        }
        Pattern >>= 1;
        UpdateMask >>= 1;
        Ones >>= 1;
      }
      return originalMask != mask;
    }
  }
}
