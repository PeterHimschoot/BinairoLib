using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public class BitCounter
  { 
    // TODO Add support for masks
    public int CountOnes(ushort row, int size)
    {
      int ones = 0;
      ushort mask = 0b1000_0000_0000_0000;
      for(int i = 0; i < size; i += 1)
      {
       if((row & mask) == mask)
        {
          ones += 1;
        }
        mask >>= 1;
      }
      return ones;
    }
    public int CountZeros(ushort row, int size)
    {
      int zeros = 0;
      ushort mask = 0b1000_0000_0000_0000;
      for (int i = 0; i < size; i += 1)
      {
        if ((row & mask) == 0)
        {
          zeros += 1;
        }
        mask >>= 1;
      }
      return zeros;
    }
  }
}
