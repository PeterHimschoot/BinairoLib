﻿using System;
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
          "001101X0",
          "00110110",
          true
        };
        yield return new object[] {
          "1100011X",
          "11000110",
          true
        };
        yield return new object[] {
          "1XX0011X",
          "1XX00110",
          true
        };
        yield return new object[] {
          "1XXX1001",
          "10101001",
          true
        };
        yield return new object[] {
          "10XXX010",
          "10101010",
          true
        };
        yield return new object[] {
          "01XXX100110010",
          "01101100110010",
          true
        };
        yield return new object[] {
          "X1010101010011",
          "01010101010011",
          true
        };
        yield return new object[] {
          "X101X101010011",
          "01010101010011",
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
