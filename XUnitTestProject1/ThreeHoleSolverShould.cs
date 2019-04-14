using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class ThreeHoleSolverShould
  {
    private readonly ITestOutputHelper output;

    public ThreeHoleSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteRows
    {
      get
      {
        yield return new object[] {
          "1XXX1001",
          "1XXX1001",
          false
        };
        yield return new object[] {
          "10XXX010",
          "10XXX010",
          false
        };
        yield return new object[] {
          "01XXX101",
          "01XXX101",
          false
        };
        yield return new object[] {
          "01XXX100110010",
          "01101100110010",
          true
        };
        yield return new object[] {
          "011001XXX01001",
          "011001XX101001",
          true
        };
        yield return new object[] {
          "1100110XXX0XX1",
          "11001100100XX1",
          true
        };
        yield return new object[] {
          "11001XXX010110",
          "110010XX010110",
          true
        };
        yield return new object[] {
          "11010XXX100110",
          "11010XX0100110",
          true
        };
        yield return new object[] {
          "00110XXX101001",
          "001101XX101001",
          true
        };
        yield return new object[] {
          "00101XXX011001",
          "00101XX1011001",
          true
        };
        yield return new object[] {
        "10XXX010110010",
        "10XXX010110010",
           false
        };
        yield return new object[] {
        "1001100110XXX1",
        "1001100110XX01",
           true
        };

      }
    }

    [Theory]
    [MemberData(nameof(IncompleteRows))]
    public void SolveThreeHoleCorrectly(string rowString, string expectedString, bool expectedSolved)
    {
      (ushort row, ushort mask, int size) = rowString.ToRowWithMaskAndSize();
      (ushort expectedRow, ushort expectedMask, int expectedSize) = expectedString.ToRowWithMaskAndSize();

      var sut = new ThreeHoleSolver(new BitCounter());

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
