using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class BinairoBoardSolverShould : IClassFixture<Valid14x14>
  {
    private readonly Valid14x14 validRows;
    private readonly ITestOutputHelper output;

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
          int.MaxValue
        };
        yield return new object[]
        {
          new string[]
          {
            "XX00XXX_XXXX00X",
            "1XXX1XX_1XX1X1X",
            "XXX0XXX_XXXXXX1",
            "XX0X11X_0X1XX0X",
            "0XXXXXX_0XXXXX1",
            "00XXX1X_XXX1XXX",
            "XXXXXX0_XXXXXX1",
            "XXXX0XX_XXX1XX1",
            "10XX0X1_XXXX0XX",
            "1XXXXXX_XXXX0X0",
            "XX0XX1X_X0XXXXX",
            "0X0X0XX_XX1XXXX",
            "XXX1XXX_XX1XX00",
            "XXXXXXX_XXXXXXX",
          },
          int.MaxValue
        };
        yield return new object[]
        {
          new string[]
          {
            "1X00XX0_XX1XXX0",
            "1XXXX0X_X1XXXX0",
            "XX00XXX_11XXXXX",
            "XX0X1XX_XXX1XX1",
            "XXXXX0X_1XXX0XX",
            "XXXXXXX_XX1XXXX",
            "XX0XXXX_XXXXXXX",
            "X0XXXXX_0XX1X0X",
            "XXXXXX0_X1XXXX1",
            "XXX0XXX_XXX0XXX",
            "XXXXXXX_XX1XX0X",
            "XXX1X1X_0XXXXXX",
            "1X0XX11_01XX00X",
            "XX1XXXX_X1X0X1X",
          },
          int.MaxValue
        };
        yield return new object[]
        {
          new string[]
          {
            "XX0XXX0_0XXXX00",
            "X1XX0X0_0X1XXXX",
            "XXXX0XX_XXXX11X",
            "XX0XXXX_XXXXXXX",
            "XXXXXXX_XXX0XXX",
            "0XXXXXX_XX1XX0X",
            "XXXXXXX_0XX1X0X",
            "0X0XX0X_XXXXXXX",
            "XXXXXXX_1X1XXX0",
            "X0XXXXX_X0XXXXX",
            "0XXXXXX_XXXX0XX",
            "01XXXXX_01XXXX0",
            "XX00XXX_XXXX11X",
            "0XXXXXX_1X00XXX",
          },
          int.MaxValue
        };
        yield return new object[]
        {
          new string[]
          {
            "XXXX0XX_0XXX00X",
            "X0XXXXX_0XXX0XX",
            "X0XX1XX_XXXXXXX",
            "1XXXXXX_XX00X1X",
            "X11XXX0_XXXXXXX",
            "XXXXXXX_XXXX0XX",
            "XXX1XXX_1XXXX1X",
            "1X0XXXX_X0XXXXX",
            "X1X0X00_XXXXXXX",
            "1XXXXXX_XXXXXXX",
            "1X0X0XX_XXXXX1X",
            "XXXXXXX_0XXX01X",
            "0XX1XXX_XXXX0XX",
            "X1XXX1X_0XX1XXX",
          },
          int.MaxValue
        };
        //yield return new object[]
        //{
        //  new string[]
        //  {
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //  },
        //  int.MaxValue
        //};
        //yield return new object[]
        //{
        //  new string[]
        //  {
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //  },
        //  int.MaxValue
        //};
        //yield return new object[]
        //{
        //  new string[]
        //  {
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //    "XXXXXXX_XXXXXXX",
        //  },
        //  int.MaxValue
        //};
      }
    }

    [Theory]
    [MemberData(nameof(IncompleteBoards))]
    public void SolveIncompleteBoards(string[] rowStrings, int iterations)
    {
      IEnumerable<(ushort, ushort, int)> coll = rowStrings.Select(rowString => rowString.ToRowWithMaskAndSize());
      ushort[] rows = coll.Select(trio => trio.Item1).ToArray();
      ushort[] masks = coll.Select(trio => trio.Item2).ToArray();
      int size = coll.First().Item3;

      var printer = new BoardPrinter(this.output);
      printer.PrintBoard(rows, masks, size);
      var rowChecker = new BinairoRowChecker(this.validRows, size);
      var flipper = new MatrixFlipper();
      var checker = new BinairoBoardChecker(rowChecker, flipper, size);
      var rowSolver = new BinairoRowSolver(size);
      var boardSolver = new DuplicateRowSolver(size, new BitCounter());
      var solver = new BinairoBoardSolver(checker, rowSolver, boardSolver, flipper, size)
      {
        Output = printer,
        Iterations = iterations
      };
      //rowSolver.RowChecker = rowChecker;
      //rowSolver.Output = printer;
      bool solved = solver.Solve(rows, masks);
      Assert.True(solved);
    }

    [Fact]
    public void RecognizeCompleteBoard()
    {
      for (int size = 6; size < 16; size += 2)
      {
        ushort mask = size.ToMask();
        ushort[] masks = Enumerable.Repeat(mask, size).ToArray();
        var solver = new BinairoBoardSolver(null, null, null, null, size);
        Assert.True(solver.BoardIsComplete(masks));
      }
    }
  }
}
