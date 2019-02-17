using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public class ArrayToRowConverter
  {
    const byte zero = 0b0000_0000;
    const byte one = 0b0000_0001;

    public uint Convert(byte[] row, int size)
    {
      uint result = 0;
      for (int i = 0; i < size; i += 1)
      {
        result <<= 1;
        result |= (uint)((row[i] != zero) ? 0b0000_0001 : 0b0000_0000);
      }
      return result;
    }
  }
}
