namespace BinairoLib
{
  /// <summary>
  /// When all 1's have been filled, look for the missing 0's
  /// This includes 1XX0 and 0XX1 holes  /// </summary>
  public class OnesAllDoneSolver : IRowSolver
  {
    private readonly BitCounter counter;

    public OnesAllDoneSolver(BitCounter bitCounter) => this.counter = bitCounter;

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      ushort originalMask = mask;
      int ones = this.counter.CountOnes(row, size, mask, includeHoles: true);
      if (ones == size / 2)
      {
        // complete with '0's, skipping over holes
        ushort patternMask = 0b1111_0000_0000_0000;
        ushort patternMatch = 0b1001_0000_0000_0000;
        ushort case1XX0 = 0b1000_0000_0000_0000;
        ushort case0XX1 = 0b0001_0000_0000_0000;
        ushort missingOne = 0b1000_0000_0000_0000;
        for (int i = 0; i < size; i += 1)
        {
          if ((mask & patternMask) == patternMatch)
          {
            ushort pattern = (ushort)(row & patternMatch);
            if (pattern == case1XX0 || pattern == case0XX1)
            { // skip hole
              i += 2;
              patternMask >>= 3;
              patternMatch >>= 3;
              case1XX0 >>= 3;
              case0XX1 >>= 3;
              missingOne >>= 3;
              continue;
            }
          }
          if ((~mask & missingOne) == missingOne)
          {
            mask |= missingOne;
          }
          patternMask >>= 1;
          patternMatch >>= 1;
          case1XX0 >>= 1;
          case0XX1 >>= 1;
          missingOne >>= 1;
        }
      }
      return originalMask != mask;
    }
  }
}
