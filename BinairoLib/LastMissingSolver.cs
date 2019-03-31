namespace BinairoLib
{
  /// <summary>
  /// Fill the one missing element
  /// </summary>
  public class LastMissingSolver : IRowSolver
  {
    private readonly BitCounter counter = new BitCounter();

    public bool Solve(ref ushort row, ref ushort mask, int size)
    {
      ushort sizeMask = size.ToMask();
      if (mask == sizeMask) return false; // finished
      int ones = this.counter.CountOnes(row, size, mask, includeHoles: true);
      int zeros = this.counter.CountZeros(row, size, mask, includeHoles: true);
      int halfSize = size / 2;
      if (zeros == halfSize)
      {
        // complete with '1's, skipping over holes
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
            {
              i += 3;
              patternMask >>= 4;
              patternMatch >>= 4;
              case1XX0 >>= 4;
              case0XX1 >>= 4;
              missingOne >>= 4;
              continue;
            }
          }
          else
          {
            if ((~mask & missingOne) == missingOne)
            {
              row |= missingOne;
              mask |= missingOne;
        return true;
            }
            patternMask >>= 1;
            patternMatch >>= 1;
            case1XX0 >>= 1;
            case0XX1 >>= 1;
            missingOne >>= 1;
          }
        }
      }
      else if (ones == halfSize)
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
      else if (ones + zeros + 1 == size)
      {
        // we have a match, now find the missing 1-size hole
        ushort missingPattern = 0b1010_0000_0000_0000;
        ushort missingMask = 0b1110_0000_0000_0000;
        for (int i = 0; i < size - 1; i += 1)
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
      else if (ones + 1 == halfSize)
      {
        // Look for a 3-size hole, with zeros at the edge
        ushort missingPattern = 0b1000_1000_0000_0000;
        ushort missingMask = 0b1111_1000_0000_0000;
        for (int i = 0; i < size - 5; i += 1)
        {
          if ((mask & missingMask) == missingPattern)
          {
            //if ((row & missingPattern) == missingPattern)
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
      else if (zeros + 1 == halfSize)
      {
        // Look for a 3-size hole, with ones at the edge
        ushort missingPattern = 0b1000_1000_0000_0000;
        ushort missingMask = 0b1111_1000_0000_0000;
        for (int i = 0; i < size - 5; i += 1)
        {
          if ((mask & missingMask) == missingPattern)
          {
            //if ((row & missingPattern) == missingPattern)
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
