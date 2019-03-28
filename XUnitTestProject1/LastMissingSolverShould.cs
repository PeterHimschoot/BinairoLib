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
        yield return new object[] {
          0b0011_0100_0000_0000, // row
          0b1111_1101_0000_0000, // mask
          0b0011_0110_0000_0000, // expectedRow
          0b1111_1111_0000_0000, // expectedMask
          true
        };
        // 1100011X
        // 11000110
        yield return new object[] {
          0b1100_1010_0000_0000, // row
          0b1111_1110_0000_0000, // mask
          0b1100_1010_0000_0000, // expectedRow
          0b1111_1111_0000_0000, // expectedMask
          true
        };
        // 1XX0011X
        // 1XX00110
        yield return new object[] {
          0b1000_0110_0000_0000, // row
          0b1001_1110_0000_0000, // mask
          0b1000_0110_0000_0000, // expectedRow
          0b1001_1111_0000_0000, // expectedMask
          true
        };
        // 1XXX1001
        // 10101001
        yield return new object[] {
          0b1000_1001_0000_0000, // row
          0b1000_1111_0000_0000, // mask
          0b1010_1001_0000_0000, // expectedRow
          0b1111_1111_0000_0000, // expectedMask
          true
        };
        // 10XXX010
        // 10101010
        yield return new object[] {
          0b1000_0010_0000_0000, // row
          0b1100_0111_0000_0000, // mask
          0b1010_1010_0000_0000, // expectedRow
          0b1111_1111_0000_0000, // expectedMask
          true
        };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteRows))]
    public void SolveMissingOneCorrectly(ushort row, ushort mask, ushort expectedRow, ushort expectedMask, bool expectedSolved)
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
