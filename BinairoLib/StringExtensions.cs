using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public static class StringExtensions
  {
    public static string ToBinaryString(this uint nr)
    {
      const uint mask = 0b1000_0000_0000_0000;
      StringBuilder bob = new StringBuilder();
      for(int i = 0; i < 16; i += 1)
      {
        if( (nr & mask) == mask )
        {
          bob.Append("1");
        } else
        {
          bob.Append("0");
        }
        nr <<= 1;
      }
      return bob.ToString();
    }
  }
}
