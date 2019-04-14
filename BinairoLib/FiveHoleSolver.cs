namespace BinairoLib
{
  /// <summary>
  /// If the number of 0's or 1's is one less then size / 2
  /// then it is possible there is a 3-sized hole which can be filled.
  /// </summary>
  public class FiveHoleSolver : IRowSolver
  {
    private readonly BitCounter counter;

    public FiveHoleSolver(BitCounter counter) => this.counter = counter;

    // If #1's == size/2 = 1 then we need to place another 1.
    // With following patterns we know where to place that 1, 
    // the rest becomes 0.
    //

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      int halfSize = size / 2;
      if (this.counter.CountOnes(row, size, mask, includeHoles: false) + 1 == halfSize)
      {
        // Look for a 5-size hole
        // 10XXX1
        // 100101
        ushort missingPattern1 = 0b1000_0100_0000_0000;
        ushort missingMask1 = 0b1100_0100_0000_0000;
        // 1XXX01
        // 101001
        ushort missingPattern2 = 0b1000_0100_0000_0000;
        ushort missingMask2 = 0b1000_1100_0000_0000;
        ushort holeMask = 0b1111_1100_0000_0000;
        for (int i = 0; i < size - 5; i += 1)
        {
          if ((mask & holeMask) == missingMask1 && (row & missingMask1) == missingPattern1)
          {
            ushort updateRow = (ushort)(0b0001_0000_0000_0000 >> i);
            row |= updateRow;
            ushort updateMask = (ushort)(0b0011_1000_0000_0000 >> i);
            mask |= updateMask;
            return true;
          }
          if ((mask & holeMask) == missingMask2 && (row & missingMask2) == missingPattern2)
          {
            ushort updateRow = (ushort)(0b0010_0000_0000_0000 >> i);
            row |= updateRow;
            ushort updateMask = (ushort)(0b0111_0000_0000_0000 >> i);
            mask |= updateMask;
            return true;
          }

          missingPattern1 >>= 1;
          missingPattern2 >>= 1;
          missingMask1 >>= 1;
          missingMask2 >>= 1;
          holeMask >>= 1;
        }
      }
      else if (this.counter.CountZeros(row, size, mask, includeHoles: false) + 1 == halfSize)
      {
        // Look for a 5-size hole
        // 01XXX0
        // 0XXX10
        ushort holeMask = 0b1111_1100_0000_0000;
        ushort missingPattern1 = 0b0100_0000_0000_0000;
        ushort missingMask1 = 0b1100_0100_0000_0000;
        ushort missingPattern2 = 0b0000_1000_0000_0000;
        ushort missingMask2 = 0b1000_1100_0000_0000;
        for (int i = 0; i < size - 5; i += 1)
        {
          if ((mask & holeMask) == missingMask1 && (row & missingMask1) == missingPattern1)
          {
            ushort updateRow = (ushort)(0b0010_1000_0000_0000 >> i);
            row |= updateRow;
            ushort updateMask = (ushort)(0b0011_1000_0000_0000 >> i);
            mask |= updateMask;
            return true;
          }
          if ((mask & holeMask) == missingMask2 && (row & missingMask2) == missingPattern2)
          {
            ushort updateRow = (ushort)(0b0101_0000_0000_0000 >> i);
            row |= updateRow;
            ushort updateMask = (ushort)(0b0111_0000_0000_0000 >> i);
            mask |= updateMask;
            return true;
          }
          missingPattern1 >>= 1;
          missingPattern2 >>= 1;
          missingMask1 >>= 1;
          missingMask2 >>= 1;
          holeMask >>= 1;
        }
      }
      return false;
    }
  }
}
