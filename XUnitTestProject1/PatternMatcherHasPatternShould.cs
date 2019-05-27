using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class PatternMatcherHasPatternShould
  {
    private readonly ITestOutputHelper output;

    public PatternMatcherHasPatternShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> PositiveMatches
    {
      get
      {
        yield return new object[] {
          0b0001_0000_0000_0000, 0b1001_0000_0000_0000, 0b0001_0000_0000_0000
        };
        yield return new object[] {
          0b1000_0000_0000_0000, 0b1001_0000_0000_0000, 0b1000_0000_0000_0000
        };
        yield return new object[] {
          0b1001_0000_0000_0000, 0b1001_0000_0000_0000, 0b1001_0000_0000_0000
        };
        yield return new object[] {
          0b0000_0000_0000_0000, 0b1001_0000_0000_0000, 0b0000_0000_0000_0000
        };
      }
    }
    public static IEnumerable<object[]> NegativeMatches
    {
      get
      {
        yield return new object[] {
          0b0000_0000_0000_0000, 0b1001_0000_0000_0000, 0b0001_0000_0000_0000
        };
        yield return new object[] {
          0b0111_0000_0000_0000, 0b1001_0000_0000_0000, 0b0001_0000_0000_0000
        };
        yield return new object[] {
          0b0001_0000_0000_0000, 0b1001_0000_0000_0000, 0b1000_0000_0000_0000
        };
        yield return new object[] {
          0b0101_0000_0000_0000, 0b1001_0000_0000_0000, 0b1001_0000_0000_0000
        };
        yield return new object[] {
          0b1001_0000_0000_0000, 0b1001_0000_0000_0000, 0b0000_0000_0000_0000
        };
      }
    }

    [Theory()]
    [MemberData(nameof(PositiveMatches))]
    public void BePositiveMatch(ushort mask, ushort patternMask, ushort pattern)
    {
      // Check against invalid parameters
      Assert.Equal(pattern & patternMask, pattern);
      output.WriteLine($"Checking {mask.ToBinaryString()}");
      output.WriteLine($"-------- {patternMask.ToBinaryString()}");
      output.WriteLine($"         {pattern.ToBinaryString()}");
      Assert.True(mask.HasPattern(patternMask, pattern));
    }

    [Theory()]
    [MemberData(nameof(NegativeMatches))]
    public void BeNegativeMatch(ushort mask, ushort patternMask, ushort pattern)
    {    
      // Check against invalid parameters
      Assert.Equal(pattern & patternMask, pattern);
      output.WriteLine($"Checking {mask.ToBinaryString()}");
      output.WriteLine($"-------- {patternMask.ToBinaryString()}");
      output.WriteLine($"         {pattern.ToBinaryString()}");
      Assert.False(mask.HasPattern(patternMask, pattern));
    }
  }
}
