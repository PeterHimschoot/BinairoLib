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
        ushort missingPattern = 0b1000_1000_0000_0000;
        ushort missingMask = 0b1111_1000_0000_0000;
        for (int i = 0; i < size - 5; i += 1)
        {
          // row should have same bit at begin & end of pattern
          if ((row & missingPattern) == missingPattern || (row & missingPattern) == 0)
          {
            if ((mask & missingMask) == missingPattern)
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
      else if (this.counter.CountZeros(row, size, mask, includeHoles: true) + 1 == halfSize)
      {
        // Look for a 3-size hole
        ushort missingPattern = 0b1000_1000_0000_0000;
        ushort missingMask = 0b1111_1000_0000_0000;
        for (int i = 0; i < size - 5; i += 1)
        {
          // row should have same bit at begin & end of pattern
          if ((row & missingPattern) == missingPattern || (row & missingPattern) == 0)
          {
            if ((mask & missingMask) == missingPattern)
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
