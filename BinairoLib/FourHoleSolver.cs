namespace BinairoLib
{
  /// <summary>
  /// If the number of 0's or 1's is one less then size / 2
  /// then it is possible there is a 3-sized hole which can be filled.
  /// </summary>
  public class FourHoleSolver : IRowSolver
  {
    private readonly BitCounter counter;

    public FourHoleSolver(BitCounter counter) => this.counter = counter;

    // If #1's == size/2 = 1 then we need to place another 1.
    // With following patterns we know where to place that 1, 
    // the rest becomes 0.
    //
    // Special case only allowed at start:
    //   "X0XX1101", "X0XX1011
    //   "00101101",
    // The Pattern:
    //   "1X0XX1",
    //   "100101",
    // Special case only allowed at end:
    //   "1011XX0X","1101XX0X"
    //   "10110100","11010100"

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      int halfSize = size / 2;
      if (this.counter.CountOnes(row, size, mask, includeHoles: false) + 1 == halfSize)
      {
        // X0XX1011
        ushort holeMask = 0b1111_1000_0000_0000;
        ushort beginPattern = 0b0000_1000_0000_0000;
        ushort beginMask = 0b0100_1000_0000_0000;
        if ((mask & holeMask) == beginMask && (row & beginMask) == beginPattern)
        {
          row |= (ushort)0b0010_0000_0000_0000;
          mask |= (ushort)0b1111_0000_0000_0000;
          return true;
        }
        // Look for a 4-size hole with a 0 in the middle (3 possibilities)
        ushort missingPattern1 = 0b0000_1000_0000_0000;
        ushort missingMask1 = 0b0010_1000_0000_0000;
        ushort missingPattern2 = 0b1000_0000_0000_0000;
        ushort missingMask2 = 0b1010_0000_0000_0000;
        ushort missingPattern3 = 0b1000_0000_0000_0000;
        ushort missingMask3 = 0b1001_0000_0000_0000;
        for (int i = 0; i < size - 4; i += 1)
        {
          //"XX0X1011",
          //"01001011",
          if ((mask & holeMask) == missingMask1 && (row & missingMask1) == missingPattern1)
          {
            ushort updateRow = (ushort)(0b0100_0000_0000_0000 >> i);
            row |= updateRow;
            ushort updateMask = (ushort)(0b1111_0000_0000_0000 >> i);
            mask |= updateMask;
            return true;
          }
          //"1101X0XX",
          //"11010100",
          if ((mask & holeMask) == missingMask2 && (row & missingMask2) == missingPattern2)
          {
            ushort updateRow = (ushort)(0b0001_0000_0000_0000 >> i);
            row |= updateRow;
            ushort updateMask = (ushort)(0b0111_1000_0000_0000 >> i);
            mask |= updateMask;
            return true;
          }
          // "1101XX0X",
          // "11010100"
          if ((mask & holeMask) == missingMask3 && (row & missingMask3) == missingPattern3)
          {
            ushort updateRow = (ushort)(0b0010_0000_0000_0000 >> i);
            row |= updateRow;
            ushort updateMask = (ushort)(0b0111_1000_0000_0000 >> i);
            mask |= updateMask;
            return true;
          }

          missingPattern1 >>= 1;
          missingPattern2 >>= 1;
          missingPattern3 >>= 1;
          missingMask1 >>= 1;
          missingMask2 >>= 1;
          missingMask3 >>= 1;
          holeMask >>= 1;
        }
      }
      else if (this.counter.CountZeros(row, size, mask, includeHoles: false) + 1 == halfSize)
      {
        // X1XX0100
        // Look for a 4-size hole with a 1 in the middle (3 possibilities)
        ushort holeMask = 0b1111_1000_0000_0000;
        ushort beginPattern = 0b0100_0000_0000_0000;
        ushort beginMask = 0b0100_1000_0000_0000;
        if ((mask & holeMask) == beginMask && (row & beginMask) == beginPattern)
        {
          row |= (ushort)0b1101_0000_0000_0000;
          mask |= (ushort)0b1111_0000_0000_0000;
          return true;
        }
        ushort missingPattern1 = 0b0010_0000_0000_0000;
        ushort missingMask1 = 0b0010_1000_0000_0000;
        ushort missingPattern2 = 0b0010_0000_0000_0000;
        ushort missingMask2 = 0b1010_0000_0000_0000;
        ushort missingPattern3 = 0b0001_0000_0000_0000;
        ushort missingMask3 = 0b1001_0000_0000_0000;
        for (int i = 0; i < size - 4; i += 1)
        {
          //"XX1X0100",
          //"10110100",
          if ((mask & holeMask) == missingMask1 && (row & missingMask1) == missingPattern1)
          {
            ushort updateRow = (ushort)(0b1011_0000_0000_0000 >> i);
            row |= updateRow;
            ushort updateMask = (ushort)(0b1111_0000_0000_0000 >> i);
            mask |= updateMask;
            return true;
          }
          //"0010X1XX",
          //"00101101",
          if ((mask & holeMask) == missingMask2 && (row & missingMask2) == missingPattern2)
          {
            ushort updateRow = (ushort)(0b0110_1000_0000_0000 >> i);
            row |= updateRow;
            ushort updateMask = (ushort)(0b0111_1000_0000_0000 >> i);
            mask |= updateMask;
            return true;
          }
          // "001XX1X0",
          // "001XX1X0"
        //0XX1X
          if ((mask & holeMask) == missingMask3 && (row & missingMask3) == missingPattern3)
          {
            ushort updateRow = (ushort)(0b0101_1000_0000_0000 >> i);
            row |= updateRow;
            ushort updateMask = (ushort)(0b0111_1000_0000_0000 >> i);
            mask |= updateMask;
            return true;
          }

          missingPattern1 >>= 1;
          missingPattern2 >>= 1;
          missingPattern3 >>= 1;
          missingMask1 >>= 1;
          missingMask2 >>= 1;
          missingMask3 >>= 1;
          holeMask >>= 1;
        }
      }
      return false;
    }
  }
}
