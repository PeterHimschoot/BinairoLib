using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BinairoLib
{
  public class BoardPrinter
  {
    const ushort mask = 0b1000_0000_0000_0000;
    public void PrintBoard(ushort[] board, int size)
    {
      for (int i = 0; i < size; i += 1)
      {
        PrintRow(board[i], size);
      }
    }

    private void PrintRow(ushort row, int size)
    {
      for (int i = 0; i < size; i += 1)
      {
        if ((row & mask) == mask)
        {
          Debug.Write("1");
        }
        else
        {
          Debug.Write("0");
        }
        row <<= 1;
      }
      Debug.Write(Environment.NewLine);
    }
  }
}
