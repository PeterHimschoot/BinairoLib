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
        yield return new object[] {
          "XX001X1XX01100XX",
          "X100101XX011001X",
          true
        };
        yield return new object[] {
          "0100101100110011",
          "0100101100110011",
          false
        };
        yield return new object[] {
          "0010101X010X1X",
          "0010101X010X1X",
          false
        };
        yield return new object[] {
        //  0 1 00   1101
          "X0X1X00XXX1101",
          "10011001001101",
          true
        };
        yield return new object[] {
          "X0XX101X011011",
          "00101010011011",
          true
        };
        yield return new object[] {
        "X0XX1XXX110100",
        "10XX1XX0110100",
        true
        };   
        //yield return new object[] {
        //"",
        //"",
        //true
        //};
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteRows))]
    public void SolveRowsCorrectly(
      string rowString, 
      string expectedString, 
      bool expectedSolved)
    {
      (ushort row, ushort mask, int size) =
        rowString.ToRowWithMaskAndSize();
      (ushort expectedRow, ushort expectedMask, int expectedSize) =
        expectedString.ToRowWithMaskAndSize();

      var sut = new BinairoRowSolver(size);
      sut.Output = new BoardPrinter(output);

      string problem = $"Trying to solve {row.ToBinaryString(mask)[0..size]}";
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
