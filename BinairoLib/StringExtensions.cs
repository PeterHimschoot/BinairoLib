using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public static class StringExtensions
  {
    public static string ToBinaryString(this ushort nr, ushort valid = 0b1111_1111_1111_1111)
    {
      const ushort mask = 0b1000_0000_0000_0000;
      StringBuilder bob = new StringBuilder();
      for(int i = 0; i < 16; i += 1)
      {
        if((valid & mask) == mask)
        {

        if( (nr & mask) == mask )
        {
          bob.Append("1");
        } else
        {
          bob.Append("0");
        }
        } else
        {
          bob.Append("X");
        }
        nr <<= 1;
        valid <<= 1;
      }
      return bob.ToString();
    }
  }
}
