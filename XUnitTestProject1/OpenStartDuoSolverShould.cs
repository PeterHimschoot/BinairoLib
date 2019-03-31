using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class OpenStartDuoSolverShould
  {
    private readonly ITestOutputHelper output;

    public OpenStartDuoSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteRows
    {
      get
      {
        yield return new object[] {
          "X00X_XXXX_X11X_XX00",
          "100X_XXXX_011X_X100",
          true
        };
        yield return new object[] {
          "X11X_XX00_XXXX_XX11",
          "011X_X100_XXXX_X011",
          true
        };
        yield return new object[] {
          "00XX_X00X_XXXX_XX",
          "00XX_100X_XXXX_XX",
          true
        };
        yield return new object[] {
          "XX00_1X1X_X01X_00",
          "X100_1X1X_X011_00",
          true
        };
        yield return new object[] {
        "0X1100XX01101",
        "001100XX01101",
          true
        };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteRows))]
    public void SolveDuosCorrectly(string rowString, string expectedString, bool expectedSolved)
    {
      var (row, mask, size) = 
        rowString.ToRowWithMaskAndSize();
      var (expectedRow, expectedMask, expectedSize) =
        expectedString.ToRowWithMaskAndSize();
      var sut = new OpenStartDuosSolver();

      string problem = $"Trying to solve {row.ToBinaryString(mask)[0..size]}";
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
