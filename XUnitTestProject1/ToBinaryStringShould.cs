using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinairoLib.Tests
{
  public class ToBinaryStringShould
  {
    public static IEnumerable<object[]> TestSubjects
    {
      get
      {
        yield return new object[] {
          0b0000_0000_0000_0000, // row
          0b1100_0000_0000_0000, // mask,
          "00XXXXXXXXXXXXXX"
        };
        yield return new object[] {
          0b1100_0011_0000_0000, // row
          0b1100_0011_0000_0000, // mask,
          "11XXXX11XXXXXXXX"
        };
        yield return new object[] {
          0b1111_1111_0000_0000, // row
          0b1100_0101_0000_0000, // mask,
          "11XXX1X1XXXXXXXX"
        };
      }
    }

    [Theory]
    [MemberData(nameof(TestSubjects))]
    public void ConvertTo(ushort input, ushort mask, string output)
    {
      var result = input.ToBinaryString(mask);
      Assert.Equal(output, result);
    }
  }
}
