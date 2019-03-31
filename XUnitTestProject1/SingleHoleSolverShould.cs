using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class SingleHoleSolverShould
  {
    private readonly ITestOutputHelper output;

    public SingleHoleSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteRows
    {
      get
      {
        yield return new object[] {
          "1X1X",
          "101X",
          true
        };
        yield return new object[] {
          "0X0X",
          "010X",
          true
        };
        yield return new object[] {
          "0X0X_X1X1",
          "010X_X101",
          true
        };
        yield return new object[] {
          "X1001X1XX011001X",
          "X100101XX011001X",
          true
        };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteRows))]
    public void FillHoles(string rowString, string expectedString, bool expectedSolved)
    {
      var sut = new SingleHoleSolver();
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
