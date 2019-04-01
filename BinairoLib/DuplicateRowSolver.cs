using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public class DuplicateRowSolver : IBoardSolver
  {
    private ushort fullMask;
    private BitCounter bitCounter;

    public DuplicateRowSolver(int size, BitCounter counter)
    {
      fullMask = size.ToMask();
      this.bitCounter = counter;
    }
    public bool Solve(ushort[] rows, ushort[] masks, int size)
    {
      for (int i = 0; i < size - 1; i += 1)
      {
        // This only makes sense for rows that are completely solved
        if (masks[i] == fullMask)
        {
          for (int j = i + 1; j < size; j += 1)
          {
            // Only check rows that match
            if ((rows[i] & masks[j]) == rows[j])
            {
              int zeros = bitCounter.CountZeros(rows[j], masks[j], includeHoles: false);
              if (zeros + 1 == size / 2)
              {
                ushort missing = (ushort)(rows[i] & ~masks[j]);
                rows[j] |= missing;
                return true;
              }
              else
              {
                int ones = bitCounter.CountOnes(rows[j], masks[j], includeHoles: false);
                if (ones + 1 == size / 2)
                {
                  ushort missing = (ushort)(rows[i] & ~masks[j]);
                  rows[j] |= missing;
                  return true;
                }
              }
            }
          }
        }
      }
      return false;
    }
  }
}
