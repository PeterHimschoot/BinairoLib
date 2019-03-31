using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class HoleAtEndSolverShould
  {
    private readonly ITestOutputHelper output;

    public HoleAtEndSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteRows
    {
      get
      {
        yield return new object[] {
        "0101010101001X",
        "01010101010011",
        true
        };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteRows))]
    public void FillHoles(string rowString, string expectedString, bool expectedSolved)
    {
      var sut = new HoleAtEndSolver();
      var (row, mask, size) = rowString.ToRowWithMaskAndSize();
      var (expectedRow, expectedMask, expectedSize) = expectedString.ToRowWithMaskAndSize();

      string problem = $"Trying to solve {row.ToBinaryString(mask)[0..size]}";
      this.output.WriteLine(problem);

      bool solved = sut.Solve(ref row, ref mask, size);
      string solution = $"Got {row.ToBinaryString(mask)[0..size]}";
      this.output.WriteLine(solution);

      Assert.Equal(expectedSolved, solved);
      Assert.Equal(expectedRow, row);
      Assert.Equal(expectedMask, mask);
    }
  }
}
