using Xunit;

namespace BinairoLib.Tests
{
  public class BitCounterShould
  {
    [Fact]
    public void CountOnes()
    {
      var sut = new BitCounter();
      int actual = sut.CountOnes(0b1001_0001_0000_0000, 8);
      Assert.Equal(3, actual);
    }

    [Fact]
    public void CountZeros()
    {
      var sut = new BitCounter();
      int actual = sut.CountZeros(0b1001_0001_0000_0000, 8);
      Assert.Equal(5, actual);
      actual = sut.CountZeros(0b1100_1010_0000_0000, 8, 0b1111_1110_0000_0000);
      Assert.Equal(3, actual);
    }

    [Fact]
    public void CountHolesCorrectly()
    {
      var sut = new BitCounter();
      int actual = sut.CountHoles(
        row: 0b0100_0101_1001_0000,
        mask: 0b1100_1111_1111_0100,
        size: 14);
      Assert.Equal(1, actual);
    }

    [Fact]
    public void CountWithPatternAndHoles()
    {
      var sut = new BitCounter();
      int actual = sut.CountOnes(
        row: 0b0100_0101_1001_0000,
        size: 14,
        mask: 0b1100_1111_1111_0100,
        includeHoles: true);
      Assert.Equal(6, actual);
    }

    [Fact]
    public void CountWithPatternAndHoles2()
    {
      var (row, mask, size) = "1100110XXX0XX1".ToRowWithMaskAndSize();
      var sut = new BitCounter();
      int actual = sut.CountOnes(row, size, mask, includeHoles: true);
      Assert.Equal(6, actual);
    }
  }
}
