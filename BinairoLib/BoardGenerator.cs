using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinairoLib
{
  public class BoardGenerator
  {
    private readonly BinairoRows validRows;
    private readonly int size;
    private readonly BinairoBoardChecker checker;
    private readonly ushort[] fullMask;
    BoardGeneratorStrategy strategy;

    public BoardGenerator(BinairoBoardChecker checker, BinairoRows validRows, int size)
    {
      this.validRows = validRows;
      this.size = size;
      this.checker = checker;
      ushort mask = (ushort)(((ushort)0b1111_1111_1111_1111) << (16 - size));
      fullMask = Enumerable.Repeat(mask, size).ToArray();
      switch (size)
      {
        case 6:
          strategy = new BoardGenerator6x6(validRows, checker, fullMask);
          break;
        case 8:
          strategy = new BoardGenerator8x8(validRows, checker, fullMask);
          break;

        default:
          throw new ArgumentException(message: "Only supports size 6, 8, 10 or 12");
      }
    }

    public ushort[] FullMask => fullMask;

    public IEnumerable<ushort[]> GenerateAllBoards()
      => strategy.GenerateAllBoards();

    interface BoardGeneratorStrategy
    {
      IEnumerable<ushort[]> GenerateAllBoards();
    }

    class BoardGenerator6x6 : BoardGeneratorStrategy
    {
      private BinairoBoardChecker boardChecker;
      private BinairoRows validRows;
      private ushort[] fullMask;

      public BoardGenerator6x6(BinairoRows validRows, BinairoBoardChecker boardChecker, ushort[] fullMask)
      {
        this.boardChecker = boardChecker;
        this.validRows = validRows;
        this.fullMask = fullMask;
      }

      public IEnumerable<ushort[]> GenerateAllBoards()
      {
        ushort[] board = new ushort[6];

        int nrOfRows = this.validRows.Length;
        for (int row0 = 0; row0 < nrOfRows; row0 += 1)
        {
          for (int row1 = 0; row1 < nrOfRows; row1 += 1)
          {
            if (row1 == row0)
            {
              continue;
            }
            for (int row2 = 0; row2 < nrOfRows; row2 += 1)
            {
              if (row2 == row1 || row2 == row0)
              {
                continue;
              }
              for (int row3 = 0; row3 < nrOfRows; row3 += 1)
              {
                if (row3 == row2 || row3 == row1 || row3 == row0)
                {
                  continue;
                }
                for (int row4 = 0; row4 < nrOfRows; row4 += 1)
                {
                  if (row4 == row3 || row4 == row2 || row4 == row1 || row4 == row0)
                  {
                    continue;
                  }
                  for (int row5 = 0; row5 < nrOfRows; row5 += 1)
                  {
                    if (row5 == row4 || row5 == row3 || row5 == row2 || row5 == row1 || row5 == row0)
                    {
                      continue;
                    }
                    board[0] = validRows[row0];
                    board[1] = validRows[row1];
                    board[2] = validRows[row2];
                    board[3] = validRows[row3];
                    board[4] = validRows[row4];
                    board[5] = validRows[row5];
                    if (boardChecker.IsValid(board, fullMask))
                    {
                      yield return board;
                      board = new ushort[6];
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    class BoardGenerator8x8 : BoardGeneratorStrategy
    {
      private BinairoBoardChecker boardChecker;
      private BinairoRows validRows;
      private ushort[] fullMask;

      public BoardGenerator8x8(BinairoRows validRows, BinairoBoardChecker boardChecker, ushort[] fullMask)
      {
        this.boardChecker = boardChecker;
        this.validRows = validRows;
        this.fullMask = fullMask;
      }

      public IEnumerable<ushort[]> GenerateAllBoards()
      {
        ushort[] board = new ushort[8];

        int nrOfRows = this.validRows.Length;
        for (int row0 = 0; row0 < nrOfRows; row0 += 1)
        {
          for (int row1 = 0; row1 < nrOfRows; row1 += 1)
          {
            if (row1 == row0)
            {
              continue;
            }
            for (int row2 = 0; row2 < nrOfRows; row2 += 1)
            {
              if (row2 == row1 || row2 == row0)
              {
                continue;
              }
              for (int row3 = 0; row3 < nrOfRows; row3 += 1)
              {
                if (row3 == row2 || row3 == row1 || row3 == row0)
                {
                  continue;
                }
                for (int row4 = 0; row4 < nrOfRows; row4 += 1)
                {
                  if (row4 == row3 || row4 == row2 || row4 == row1 || row4 == row0)
                  {
                    continue;
                  }
                  for (int row5 = 0; row5 < nrOfRows; row5 += 1)
                  {
                    if (row5 == row4 || row5 == row3 || row5 == row2 || row5 == row1 || row5 == row0)
                    {
                      continue;
                    }
                    for (int row6 = 0; row6 < nrOfRows; row6 += 1)
                    {
                      if (row6 == row5 || row6 == row3 || row6 == row2 || row6 == row1 || row6 == row0)
                      {
                        continue;
                      }
                      for (int row7 = 0; row7 < nrOfRows; row7 += 1)
                      {
                        if (row7 == row6 || row7 == row5 || row7 == row4 || row7 == row3 || row7 == row2 || row7 == row1 || row7 == row0)
                        {
                          continue;
                        }
                        board[0] = validRows[row0];
                        board[1] = validRows[row1];
                        board[2] = validRows[row2];
                        board[3] = validRows[row3];
                        board[4] = validRows[row4];
                        board[5] = validRows[row5];
                        board[6] = validRows[row6];
                        board[7] = validRows[row7];
                        if (boardChecker.IsValid(board, fullMask))
                        {
                          yield return board;
                          board = new ushort[8];
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
