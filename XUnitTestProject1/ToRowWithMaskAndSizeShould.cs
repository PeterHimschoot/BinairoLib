using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinairoLib.Tests
{
  public class ToRowWithMaskAndSizeShould
  {
    public static IEnumerable<object[]> RowsAndMasks
    {
      get
      {
        yield return new object[]
        {
          "XX001X1XX01100",
          0b0000_1010_0011_0000, // row
          0b0011_1010_0111_1100, // mask
          14
        };
        yield return new object[]
        {
          "XX00_1X1X_X011_00",
          0b0000_1010_0011_0000, // row
          0b0011_1010_0111_1100, // mask
          14
        };
      }
    }

    [Theory]
    [MemberData(nameof(RowsAndMasks))]
    public void Convert(string rowString, ushort row, ushort mask, int size)
    {
      var (_row, _mask, _size) = rowString.ToRowWithMaskAndSize();
      Assert.Equal(row, _row);
      Assert.Equal(mask, _mask);
      Assert.Equal(size, _size);
    }
  }
}
