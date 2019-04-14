using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class FiveHoleSolverShould
  {
    private readonly ITestOutputHelper output;

    public FiveHoleSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteOneRows
    {
      get
      {
        yield return new object[] {
          "110010110XXX1X",
          "1100101100101X",
          true
        };
        yield return new object[] {
          "0110010110XXX1",
          "01100101100101",
          true
        };
        yield return new object[] {
          "1XXX0110010110",
          "10100110010110",
          true
        };
        yield return new object[] {
          "100101101XXX01",
          "10010110101001",
          true
        };
        yield return new object[] {
        "11001XXX010110",
        "11001XXX010110",
          false
        };
      }
    }

    //0010XX1X
    public static IEnumerable<object[]> IncompleteZeroRows
    {
      get
      {
        yield return new object[] {
          "001101001XXX0X",
          "0011010011010X",
          true
        };
        yield return new object[] {
          "1001101001XXX0",
          "10011010011010",
          true
        };
        yield return new object[] {
          "0XXX1001101001",
          "01011001101001",
          true
        };
        yield return new object[] {
          "011010010XXX10",
          "01101001010110",
          true
        };
      }
    }

    [Theory(Skip = "Ignore")]
    [MemberData(nameof(IncompleteOneRows))]
    public void SolveMissingOneCorrectly(string rowString, string expectedString, bool expectedSolved)
    {
      (ushort row, ushort mask, int size) = rowString.ToRowWithMaskAndSize();
      (ushort expectedRow, ushort expectedMask, int expectedSize) = expectedString.ToRowWithMaskAndSize();

      var bitCounter = new BitCounter();
      int ones = bitCounter.CountOnes(row, size, mask, includeHoles: false);
      Assert.Equal(ones + 1, size / 2);

      var sut = new FiveHoleSolver(new BitCounter());

      string problem = $"Trying to solve {rowString}";
      this.output.WriteLine(problem);

      bool solved = sut.Solve(ref row, ref mask, size);
      string solution = $"Got             {row.ToBinaryString(mask)[0..size]}";
      this.output.WriteLine(solution);

      Assert.Equal(expectedSolved, solved);
      Assert.Equal(expectedRow, row);
      Assert.Equal(expectedMask, mask);
    }

    [Theory(Skip = "Ignore")]
    [MemberData(nameof(IncompleteZeroRows))]
    public void SolveMissingZeroCorrectly(string rowString, string expectedString, bool expectedSolved)
    {
      (ushort row, ushort mask, int size) = rowString.ToRowWithMaskAndSize();
      (ushort expectedRow, ushort expectedMask, int expectedSize) = expectedString.ToRowWithMaskAndSize();

      var bitCounter = new BitCounter();
      int zeros = bitCounter.CountZeros(row, size, mask, includeHoles: false);
      Assert.Equal(zeros + 1, size / 2);

      var sut = new FiveHoleSolver(new BitCounter());

      string problem = $"Trying to solve {rowString}";
      this.output.WriteLine(problem);

      bool solved = sut.Solve(ref row, ref mask, size);
      string solution = $"Got             {row.ToBinaryString(mask)[0..size]}";
      this.output.WriteLine(solution);

      Assert.Equal(expectedSolved, solved);
      Assert.Equal(expectedRow, row);
      Assert.Equal(expectedMask, mask);
    }
  }
}
