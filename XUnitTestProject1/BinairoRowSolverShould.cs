using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class BinairoRowSolverShould
  {
    private ITestOutputHelper output;

    public BinairoRowSolverShould(ITestOutputHelper output)
      => this.output = output;

    public static IEnumerable<object[]> IncompleteRows
    {
      get
      {
        //// Before: 00XX_XXXX_XXXX_XXXX
        //// After : 001X_XXXX_XXXX_XXXX
        //yield return new object[] {
        //  0b0000_0000_0000_0000, // row
        //  0b0110_0000_0000_0000, // mask
        //  0b1000_0000_0000_0000, // expectedRow
        //  0b1110_0000_0000_0000, // expectedMask
        //  true
        //};
        //// Before: 11XX_XXXX_XXXX_XXXX
        //// After : 110X_XXXX_XXXX_XXXX
        //yield return new object[] {
        //  0b0110_0000_0000_0000, // row
        //  0b0110_0000_0000_0000, // mask
        //  0b0110_0000_0000_0000, // expectedRow
        //  0b1110_0000_0000_0000, // expectedMask
        //  true
        //};
        //// Before: 00XX_X00X_XXXX_XXXX
        //// After : 001X_X001_XXXX_XXXX
        //yield return new object[] {
        //  0b0000_0011_0000_0000, // row
        //  0b0110_0011_0000_0000, // mask
        //  0b1000_0011_0000_0000, // expectedRow
        //  0b1110_0111_0000_0000, // expectedMask
        //  true
        //};
        // Before : XX001X1XX01100XX
        yield return new object[] {
          0b0000_1010_0011_0000, // row
          0b0011_1010_0111_1100, // mask
          0b0100_1010_0011_0000, // expectedRow
          0b0111_1110_0111_1100, // expectedMask
          true
        };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteRows))]
    public void SolveRowsCorrectly(ushort row, ushort mask, ushort expectedRow, ushort expectedMask, bool expectedSolved)
    {
      var sut = new BinairoRowSolver();

      string problem = $"Trying to solve {row.ToBinaryString(mask)[0..14]}";
      output.WriteLine(problem);

      bool solved = sut.Solve(ref row, ref mask, 14);
      string solution = $"Got {row.ToBinaryString(mask)[0..14]}";
      output.WriteLine(solution);

      Assert.Equal(expectedSolved, solved);
      Assert.Equal(expectedRow, row);
      Assert.Equal(expectedMask, mask);
    }
  }
}
