using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinairoLib.Tests
{
  public class BinaryRowGeneratorShould
  {
    [Theory]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(10)]
    [InlineData(12)]
    [InlineData(14)]
    public void GenerateSize(int size)
    {
      BinairoRowGenerator brg = new BinairoRowGenerator();

      ushort[] result = brg.GenerateAllValid(size);
      foreach(ushort nr in result)
      {
        string s = nr.ToBinaryString().Substring(0, size);
        Assert.DoesNotContain("000", s);
        Assert.DoesNotContain("111", s);
      }
    }
  }
}
