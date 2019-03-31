using System.Text;

namespace BinairoLib
{
  public static class StringExtensions
  {
    public static string ToBinaryString(this ushort nr, ushort valid = 0b1111_1111_1111_1111)
    {
      const ushort mask = 0b1000_0000_0000_0000;
      var bob = new StringBuilder();
      for (int i = 0; i < 16; i += 1)
      {
        if ((valid & mask) == mask)
        {

          if ((nr & mask) == mask)
          {
            bob.Append("1");
          }
          else
          {
            bob.Append("0");
          }
        }
        else
        {
          bob.Append(" ");
        }
        nr <<= 1;
        valid <<= 1;
      }
      return bob.ToString();
    }

    public static (ushort, ushort, int) ToRowWithMaskAndSize(this string rowString)
    {
      ushort row = 0b0000_0000_0000_0000;
      ushort mask = 0b0000_0000_0000_0000;
      int size = rowString.Length;
      ushort bit = 0b1000_0000_0000_0000;
      foreach (char ch in rowString)
      {
        switch (ch)
        {
          case '0':
            mask |= bit;
            break;
          case '1':
            row |= bit;
            mask |= bit;
            break;
          case '_':
            // skip  
            size -= 1;
            break;
          default:
            break;
        }
        if (ch != '_')
        {
          bit >>= 1;
        }
      }
      return (row, mask, size);
    }
  }
}
