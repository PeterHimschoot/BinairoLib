using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
 public class ZerosAllDoneSolverShould
  {
    private ITestOutputHelper output;

    public ZerosAllDoneSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteRows
    {
      get
      {
        yield return new object[] {
          "001101X0",
          "00110110",
          true
        };
        yield return new object[] {
          "01XX0100110X10",
          "01101100110010",
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
