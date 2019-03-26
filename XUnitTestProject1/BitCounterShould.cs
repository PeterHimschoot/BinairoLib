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
      int actual = sut.CountOnes(0b1001_0001_0000_0000, 8);
      Assert.Equal(3, actual);
    }
  }
}
