using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class OpenEndDuoSolverShould
  {
    private readonly ITestOutputHelper output;

    public OpenEndDuoSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteRows
    {
      get
      {
        yield return new object[] {
          "00XX_11XX_XXX0_0X",
          "001X_110X_XXX0_01",
          true
        };
        yield return new object[] {
          "11XX_X00X_XXXX_XX",
          "110X_X001_XXXX_XX",
          true
        };
        yield return new object[] {
          "00XX_X00X_11XX_11",
          "001X_X001_110X_11",
          true
        };
        yield return new object[] {
           "XX00_1X1X_XX11_00",
           "XX00_1X1X_XX11_00",
          false
        };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteRows))]
    public void SolveDuosCorrectly(string rowString, string expectedString, bool expectedSolved)
    {
      (ushort row, ushort mask, int size) = 
        rowString.ToRowWithMaskAndSize();
      (ushort expectedRow, ushort expectedMask, int expectedSize) = 
        expectedString.ToRowWithMaskAndSize();
      var sut = new OpenEndDuosSolver();

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
