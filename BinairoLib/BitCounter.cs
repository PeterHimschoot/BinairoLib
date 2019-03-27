using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public class BitCounter
  {
    // TODO Add support for masks
    public int CountOnes(ushort row, int size,
      ushort mask = 0b1111_1111_1111_1111,
      bool includeHoles = false)
    {
      int ones = 0;
      ushort bitMask = 0b1000_0000_0000_0000;
      for (int i = 0; i < size; i += 1)
      {
        if ((mask & bitMask) == bitMask && (row & bitMask) == bitMask)
        {
          ones += 1;
        }
        bitMask >>= 1;
      }
      if (includeHoles)
      {
        ones += CountHoles(row, mask, size);
      }
      return ones;
    }
    public int CountZeros(ushort row, int size,
      ushort mask = 0b1111_1111_1111_1111,
      bool includeHoles = false)
    {
      int zeros = 0;
      ushort bitMask = 0b1000_0000_0000_0000;
      for (int i = 0; i < size; i += 1)
      {
        if ((mask & bitMask) == bitMask && (row & bitMask) == 0)
        {
          zeros += 1;
        }
        bitMask >>= 1;
      }
      if( includeHoles )
      {
        zeros += CountHoles(row, mask, size);
      }
      return zeros;
    }

    /// <summary>
    /// A hole is a pattern like 0XX1 or 1XX0.
    /// In the middle there has to be a one and zero.
    /// </summary>
    public int CountHoles(ushort row, ushort mask, int size)
    {
      int holes = 0;
      size -= 4;
      ushort patternMask = 0b1111_0000_0000_0000;
      ushort patternMatch = 0b1001_0000_0000_0000;
      ushort case1XX0 = 0b1000_0000_0000_0000;
      ushort case0XX1 = 0b0001_0000_0000_0000;
      for (int i = 0; i < size; i += 1)
      {
        if ((mask & patternMask) == patternMatch)
        {
          ushort pattern = (ushort)(row & patternMatch);
          if (pattern == case1XX0 || pattern == case0XX1)
          {
            holes += 1;
          }
        }
        patternMask >>= 1;
        patternMatch >>= 1;
        case1XX0 >>= 1;
        case0XX1 >>= 1;
      }
      return holes;
    }
  }
}
