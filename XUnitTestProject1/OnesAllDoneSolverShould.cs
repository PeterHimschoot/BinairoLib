using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
 public class OnesAllDoneSolverShould
  {
    private ITestOutputHelper output;

    public OnesAllDoneSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteRows
    {
      get
      {
        yield return new object[] {
          "1XX0011X",
          "1XX00110",
          true
        };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteRows))]
    public void SolveMissingOneCorrectly(string rowString, string expectedString, bool expectedSolved)
    {
      var (row, mask, size) = rowString.ToRowWithMaskAndSize();
      var (expectedRow, expectedMask, expectedSize) = expectedString.ToRowWithMaskAndSize();

      var sut = new LastMissingSolver();

      string problem = $"Trying to solve {rowString}";
      output.WriteLine(problem);

      bool solved = sut.Solve(ref row, ref mask, size);
      string solution = $"Got             {row.ToBinaryString(mask)[0..size]}";
      output.WriteLine(solution);

      Assert.Equal(expectedSolved, solved);
      Assert.Equal(expectedRow, row);
      Assert.Equal(expectedMask, mask);
    }
  }
}
