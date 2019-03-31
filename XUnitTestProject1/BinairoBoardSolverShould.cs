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
          new string[]
          {
            "XXXXXXX_X1XXXX0",
            "XXXX0XX_1XXXXXX",
            "X0XXXXX_X0X0X1X",
            "1XXXXXX_XXXXXXX",
            "1XX11XX_0X0X00X",
            "XXXXXXX_X1XX0XX",
            "XXX0XXX_XXXXX1X",
            "X1XXX1X_XXX0X1X",
            "0XX1XXX_XXXXXX0",
            "XX0XXX1_1X0X1XX",
            "XXX1X0X_XXXXXXX",
            "1XXXXXX_XXX10X0",
            "X0X1XX0_XXX1XXX",
            "XXXXXX0_0XXX0X0",
          },
        };
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteBoards))]
    public void SolveIncompleteBoards(string[] rowStrings)
    {

      var coll = rowStrings.Select(rowString => rowString.ToRowWithMaskAndSize());
      var rows = coll.Select(trio => trio.Item1).ToArray();
      var masks = coll.Select(trio => trio.Item2).ToArray();
      int size = coll.First().Item3;

      BoardPrinter printer = new BoardPrinter(output);
      printer.PrintBoard(rows, masks, size);
      var rowChecker = new BinairoRowChecker(this.validRows, size);
      var flipper = new MatrixFlipper();
      var checker = new BinairoBoardChecker(rowChecker, flipper, size);
      var rowSolver = new BinairoRowSolver();
      var solver = new BinairoBoardSolver(checker, rowSolver, flipper, size);
      solver.Output = printer;
      //solver.Solve(rows, masks);
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
