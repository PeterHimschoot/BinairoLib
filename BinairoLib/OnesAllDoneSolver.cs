namespace BinairoLib
{
  /// <summary>
  /// Fill the one missing element
  /// </summary>
  public class OnesAllDoneSolver : IRowSolver
  {
    private readonly BitCounter counter = new BitCounter();

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      ushort sizeMask = size.ToMask();
      if (mask == sizeMask) return false; // finished
      int ones = this.counter.CountOnes(row, size, mask, includeHoles: true);
      int zeros = this.counter.CountZeros(row, size, mask, includeHoles: true);
      int halfSize = size / 2;
      if (ones == halfSize)
      {
        // complete with '0's, skipping over holes
        ushort patternMask = 0b1111_0000_0000_0000;
        ushort patternMatch = 0b1001_0000_0000_0000;
        ushort case1XX0 = 0b1000_0000_0000_0000;
        ushort case0XX1 = 0b0001_0000_0000_0000;
        ushort missingOne = 0b1000_0000_0000_0000;
        for (int i = 0; i < size - 4; i += 1)
        {
          if ((mask & patternMask) == patternMatch)
          {
            ushort pattern = (ushort)(row & patternMatch);
            if (pattern == case1XX0 || pattern == case0XX1)
            {
            }
            else
            {
              if ((~mask & missingOne) == missingOne)
              {
                mask |= missingOne;
              }
            }
          }
          patternMask >>= 1;
          patternMatch >>= 1;
          case1XX0 >>= 1;
          case0XX1 >>= 1;
          missingOne >>= 1;
        }
        return true;
      }
      return false;
    }
  }
}
