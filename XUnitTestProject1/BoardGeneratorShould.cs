using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;

namespace BinairoLib.Tests
{
  public class BoardGeneratorShould
  {
    //[Theory(/*Timeout = 60000 /*ms*/)]
    //[InlineData(6)]
    //[InlineData(8)]
    //[InlineData(10)]
    public void GenerateBoardInReasonableTime(int size)
    {
      Debug.WriteLine($"Generating all boards for size {size}.");
      var validRows = new BinairoRows(new BinairoRowGenerator(), size);
      var rowChecker = new RowChecker(validRows, size);
      var flipper = new MatrixFlipper();
      var checker = new BoardChecker(rowChecker, flipper, size);
      var sut = new BoardGenerator(checker, validRows, size);
      //var boardPrinter = new BoardPrinter();
      var allBoards = sut.GenerateAllBoards(); // .Take(1000);
      //Debug.WriteLine($"Found {allBoards.Count()} boards.");
      int count = 0;
      foreach (ushort[] board in allBoards)
      {
        count += 1;
        if( count % 100 == 0)
        {
          Debug.WriteLine(count);
        }
        //boardPrinter.PrintBoard(board, size);
        Assert.True(checker.IsValid(board, sut.FullMask));
      }
    }
  }
}
