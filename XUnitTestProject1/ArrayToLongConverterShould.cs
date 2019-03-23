using BinairoLib;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject1
{
  public class ArrayToLongConverterShould
  {
    const byte zero = 0b0000_0000;
    const byte one = 0b0000_0001;

    public static IEnumerable<object[]> TestData
    {
      get
      {
        yield return new object[] {
          new byte [] { zero, one, one, zero, one, zero, zero, one },
          (ushort)0b0110_1001_0000_0000
        };
      }
    }
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void ConvertCorrectly(byte[] row, ushort expected)
    {
      var converter = new ArrayToRowConverter();
      var result = converter.Convert(row, 8);
      Assert.Equal(expected, result);
    }
  }
}
