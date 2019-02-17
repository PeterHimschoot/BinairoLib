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

    public static IEnumerable<object[]> MemberData
    {
      get
      {
        yield return new object[] {
          new byte [] { zero, one, one, zero, one, zero, zero, one },
          (uint)0b0110_1001
        };
      }
    }
    
    [Theory]
    [MemberData(nameof(MemberData))]
    public void ConvertCorrectly(byte[] row, uint expected)
    {
      var converter = new ArrayToRowConverter();
      var result = converter.Convert(row, 8);
      Assert.Equal(expected, result);

    }
  }
}
