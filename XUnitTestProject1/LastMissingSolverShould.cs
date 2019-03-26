using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
 public class LastMissingSolverShould
  {
    private ITestOutputHelper output;

    public LastMissingSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteRows
    {
      get
      {
        // Before: 00XX_XXXX_XXXX_XXXX
        // After : 001X_XXXX_XXXX_XXXX
        yield return new object[] {
          0b0011_0100_0000_0000, // row
          0b1111_1101_0000_0000, // mask
          0b0011_0110_0000_0000, // expectedRow
          0b1111_1111_0000_0000, // expectedMask
          true
        };
        // Before: 11XX_XXXX_XXXX_XXXX
        // After : 110X_XXXX_XXXX_XXXX
        yield return new object[] {
          0b1100_0110_0000_0000, // row
          0b1111_1110_0000_0000, // mask
          0b1100_0110_0000_0000, // expectedRow
          0b1111_1111_0000_0000, // expectedMask
          true
        };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteRows))]
    public void SolveDuosCorrectly(ushort row, ushort mask, ushort expectedRow, ushort expectedMask, bool expectedSolved)
    {
      var sut = new LastMissingSolver();

      string problem = $"Trying to solve {row.ToBinaryString(mask)[0..8]}";
      output.WriteLine(problem);

      bool solved = sut.Solve(ref row, ref mask, 8);
      string solution = $"Got {row.ToBinaryString(mask)[0..8]}";
      output.WriteLine(solution);

      Assert.Equal(expectedSolved, solved);
      Assert.Equal(expectedRow, row);
      Assert.Equal(expectedMask, mask);
    }
  }
}
