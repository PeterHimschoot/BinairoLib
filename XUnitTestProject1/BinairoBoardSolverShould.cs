using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class BinairoBoardSolverShould : IClassFixture<Valid14x14>
  {
    private Valid14x14 validRows;
    private ITestOutputHelper output;

    public BinairoBoardSolverShould(Valid14x14 validRows, ITestOutputHelper output)
    {
      this.validRows = validRows;
      this.output = output;
    }

    public static IEnumerable<object[]> IncompleteBoards
    {
      get
      {
        yield return new object[]
        {
          new ushort[]
          {
            0b0000_0000_0000_0000,
            0b0000_0000_1000_0000,
            0b0000_0000_0000_1000,
            0b1000_0010_0000_0000,

            0b0010_0000_1000_0000,
            0b0100_0000_0001_0000,
            0b0000_0000_0000_0000,
            0b1000_0000_0000_0000,

            0b0001_0000_0000_0000,
            0b0000_0010_0000_0000,
            0b0000_1000_0011_0000,
            0b0000_0001_1011_0000,

            0b0000_0000_0000_0000,
            0b0001_1000_0000_0000
          },
          new ushort[]
          {
            0b0001_1000_0001_0100,
            0b0001_1010_1001_0000,
            0b0100_0000_0100_1000,
            0b1001_1010_0000_0000,

            0b0010_0000_1000_0000,
            0b0100_0000_0101_0100,
            0b0010_0001_0100_0000,
            0b1000_1001_0000_0000,

            0b0001_0000_0000_0000,
            0b0000_0010_0000_0000,
            0b0001_1000_0011_0100,
            0b0000_0001_1011_0000,

            0b0110_0010_0000_0000,
            0b0001_1000_0000_0000
          }
        };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteBoards))]
    public void SolveIncompleteBoards(ushort[] rows, ushort[] masks)
    {
      BoardPrinter printer = new BoardPrinter(output);
      printer.PrintBoard(rows, masks, 14);
      int size = 14;
      var rowChecker = new BinairoRowChecker(this.validRows, size);
      var flipper = new MatrixFlipper();
      var checker = new BinairoBoardChecker(rowChecker, flipper, size);
      var rowSolver = new BinairoRowSolver();
      var solver = new BinairoBoardSolver(checker, rowSolver, flipper, size);
      solver.Output = printer;
      solver.Solve(rows, masks);
    }

    [Fact]
    public void RecognizeCompleteBoard()
    {
      for (int size = 6; size < 16; size += 2)
      {
        ushort mask = size.ToMask();
        ushort[] masks = Enumerable.Repeat(mask, size).ToArray();
        var solver = new BinairoBoardSolver(null, null, null, size);
        Assert.True(solver.BoardIsComplete(masks));
      }
    }
  }
}
