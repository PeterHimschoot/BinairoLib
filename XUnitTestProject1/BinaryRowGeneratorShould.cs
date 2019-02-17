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
    public void GenerateSize(int size)
    {
      BinairyRowGenerator brg = new BinairyRowGenerator();

      uint[] result = brg.GenerateAllValid(size);
      foreach(uint nr in result)
      {
        string s = nr.ToBinaryString().Substring(16-size);
        Assert.DoesNotContain("000", s);
        Assert.DoesNotContain("111", s);
      }
    }
  }
}
