namespace BinairoLib
{
  /// <summary>
  /// If the number of 0's or 1's is one less then size / 2
  /// then it is possible there is a 3-sized hole which can be filled.
  /// </summary>
  public class ThreeHoleSolver : IRowSolver
  {
    private readonly BitCounter counter;

    public ThreeHoleSolver(BitCounter counter) => this.counter = counter;

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      int halfSize = size / 2;
      if (this.counter.CountOnes(row, size, mask, includeHoles: true) + 1 == halfSize)
      {
        // Look for a 3-size hole
        ushort missingPattern1 = 0b1000_1000_0000_0000;
        ushort missingPattern2 = 0b0000_1000_0000_0000;
        ushort missingPattern3 = 0b1000_0000_0000_0000;
        ushort missingMask = 0b1111_1000_0000_0000;
        ushort patternMask = 0b1000_1000_0000_0000;
        for (int i = 0; i < size - 5; i += 1)
        {
          // row should have same bit at begin & end of pattern
          if ((~row & missingPattern1) == missingPattern1)
          {
            if ((mask & missingMask) == missingPattern1)
            {
              ushort updateRow = (ushort)(0b0010_0000_0000_0000 >> i);
              row |= updateRow;
              ushort updateMask = (ushort)(0b0111_0000_0000_0000 >> i);
              mask |= updateMask;
              return true;
            }
          }
          else if ((mask & missingMask) == patternMask)
          {
            // row has 3-sized hole with 0 at start and 1 at end
            if ((row & patternMask) == missingPattern2)
            {
              ushort updateMask = (ushort)(0b0001_0000_0000_0000 >> i);
              mask |= updateMask;
              return true;

            }
            // row has 3-sized hole with 1 at start and 0 at end
            else if ((row & patternMask) == missingPattern3)
            {
              ushort updateMask = (ushort)(0b0100_0000_0000_0000 >> i);
              mask |= updateMask;
              return true;

            }
          }
          missingPattern1 >>= 1;
          missingPattern2 >>= 1;
          missingPattern3 >>= 1;
          patternMask >>= 1;
          missingMask >>= 1;
        }
      }
      else if (this.counter.CountZeros(row, size, mask, includeHoles: true) + 1 == halfSize)
      {
        // Look for a 3-size hole
        ushort missingPattern1 = 0b1000_1000_0000_0000;
        ushort missingPattern2 = 0b0000_1000_0000_0000;
        ushort missingPattern3 = 0b1000_0000_0000_0000;
        ushort missingMask = 0b1111_1000_0000_0000;
        ushort patternMask = 0b1000_1000_0000_0000;
        for (int i = 0; i < size - 5; i += 1)
        {
          // row should have same bit at begin & end of pattern
          if ((row & missingPattern1) == missingPattern1)
          {
            if ((mask & missingMask) == missingPattern1)
            {
              ushort updateRow = (ushort)(0b0101_0000_0000_0000 >> i);
              row |= updateRow;
              ushort updateMask = (ushort)(0b0111_0000_0000_0000 >> i);
              mask |= updateMask;
              return true;
            }
          }
          else if ((mask & missingMask) == patternMask)
          {
            // row has 3-sized hole with 0 at start and 1 at end
            if ((row & patternMask) == missingPattern2)
            {
              ushort updateRow = (ushort)(0b0100_0000_0000_0000 >> i);
              row |= updateRow;
              ushort updateMask = (ushort)(0b0100_0000_0000_0000 >> i);
              mask |= updateMask;
              return true;

            }
            // row has 3-sized hole with 1 at start and 0 at end
            else if ((row & patternMask) == missingPattern3)
            {
              ushort updateRow = (ushort)(0b0001_0000_0000_0000 >> i);
              row |= updateRow;
              ushort updateMask = (ushort)(0b0001_0000_0000_0000 >> i);
              mask |= updateMask;
              return true;
            }
          }
          missingPattern1 >>= 1;
          missingPattern2 >>= 1;
          missingPattern3 >>= 1;
          patternMask >>= 1;
          missingMask >>= 1;
        }
      }
      return false;
    }
  }
}
