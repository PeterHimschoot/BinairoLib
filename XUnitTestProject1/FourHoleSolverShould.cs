using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class FourHoleSolverShould
  {
    private readonly ITestOutputHelper output;

    public FourHoleSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteOneRows
    {
      get
      {
        // Special cases at start
        yield return new object[] {
          "X0XX1011",
          "00101011",
          true
        };
        yield return new object[] {
          "X0XX1101",
          "00101101",
          true
        };
        //Normal cases
        yield return new object[] {
          "XX0X1101",
          "01001101",
          true
        };
        yield return new object[] {
          "XX0X1011",
          "01001011",
          true
        };
        yield return new object[] {
          "01XX0X11",
          "01010011",
          true
        };
        yield return new object[] {
          "011XX0X1",
          "01101001",
          true
        };
        // Special cases at end
        yield return new object[] {
          "1101XX0X",
          "11010100",
          true
        };
        yield return new object[] {
          "1101X0XX",
          "11010010",
          true
        };
        yield return new object[]
        {
          "11001101XX1X0X",
          "11001101XX1X0X",
          false
        };
      }
    }

    //0010XX1X
    public static IEnumerable<object[]> IncompleteZeroRows
    {
      get
      {
        // Special cases at start
        yield return new object[] {
          "X1XX0100",
          "11010100",
          true
        };
        yield return new object[] {
          "X1XX0010",
          "11010010",
          true
        };
        //Normal cases
        yield return new object[] {
          "XX1X0010",
          "10110010",
          true
        };
        yield return new object[] {
          "XX1X0100",
          "10110100",
          true
        };
        yield return new object[] {
          "10XX1X00",
          "10101100",
          true
        };
        yield return new object[] {
          "100XX1X0",
          "10010110",
          true
        };
        // Special cases at end
        yield return new object[] {
          "0010XX1X",
          "00101011",
          true
        };
        yield return new object[] {
          "0010X1XX",
          "00101101",
          true
        };
        yield return new object[]    {
          "00110010XX0X1X",
          "00110010XX0X1X",
          false
    };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteOneRows))]
    public void SolveMissingOneCorrectly(string rowString, string expectedString, bool expectedSolved)
    {
      (ushort row, ushort mask, int size) = rowString.ToRowWithMaskAndSize();
      (ushort expectedRow, ushort expectedMask, int expectedSize) = expectedString.ToRowWithMaskAndSize();

      var bitCounter = new BitCounter();
      int ones = bitCounter.CountOnes(row, size, mask, includeHoles: false);
      Assert.Equal(ones + 1, size / 2);

      var sut = new FourHoleSolver(new BitCounter());

      string problem = $"Trying to solve {rowString}";
      this.output.WriteLine(problem);

      bool solved = sut.Solve(ref row, ref mask, size);
      string solution = $"Got             {row.ToBinaryString(mask)[0..size]}";
      this.output.WriteLine(solution);

      Assert.Equal(expectedSolved, solved);
      Assert.Equal(expectedRow, row);
      Assert.Equal(expectedMask, mask);
    }

    [Theory]
    [MemberData(nameof(IncompleteZeroRows))]
    public void SolveMissingZeroCorrectly(string rowString, string expectedString, bool expectedSolved)
    {
      (ushort row, ushort mask, int size) = rowString.ToRowWithMaskAndSize();
      (ushort expectedRow, ushort expectedMask, int expectedSize) = expectedString.ToRowWithMaskAndSize();

      var bitCounter = new BitCounter();
      int zeros = bitCounter.CountZeros(row, size, mask, includeHoles: false);
      Assert.Equal(zeros + 1, size / 2);

      var sut = new FourHoleSolver(new BitCounter());

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
