using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public class MatrixFlipper
  {
    //   0101        0001
    //   0101   =>   1110
    //   0110        0010
    //   1001        1101

    public void Flip(in ushort[] rows, ref ushort[] result, int size)
      => Flip(new Span<ushort>(rows), new Span<ushort>(result), size);

    private void Flip(in Span<ushort> rows, Span<ushort> result, int size)
    {
      // non-destructive on rows
      for (int i = 0; i < size; i += 1)
      {
        ushort mask = (ushort)((ushort)0b1000_0000_0000_0000 >> i);
        for (int j = 0; j < size; j += 1)
        {
          result[i] <<= 1;
          if ((rows[j] & mask) == mask)
          {
            result[i] |= 0b1;
          }
        }
        // Move all bits to the most left position
        result[i] <<= (16 - size);
      }
    }
  }
}
