using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinairoLib.Tests
{
  public class MatrixFlipperShould
  {
    private static readonly ushort[] M4x4 = {
      0b1101_0000_0000_0000,
      0b0101_0000_0000_0000,
      0b0011_0000_0000_0000,
      0b1100_0000_0000_0000 };
    private static readonly ushort[] F4x4 = {
      0b1001_0000_0000_0000,
      0b1101_0000_0000_0000,
      0b0010_0000_0000_0000,
      0b1110_0000_0000_0000 };

    public static IEnumerable<object[]> MatricesWithFlipped
    {
      get
      {
        object[] MWF(ushort[] matrix, int size, ushort[] flipped)
        => new object[] { matrix, size, flipped };

        yield return MWF(M4x4, 4, F4x4);
      }
    }


    [Theory]
    [MemberData(nameof(MatricesWithFlipped))]
    public void FlipMatrixCorrectly(ushort[] rows, int size, ushort[] expected)
    {
      var sut = new MatrixFlipper();

      ushort[] result = new ushort[size];
      sut.Flip(rows, ref result, size);

      Assert.Equal(expected, result);
    }
  }
}
