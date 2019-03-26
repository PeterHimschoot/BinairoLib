using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit.Abstractions;

namespace BinairoLib.Tests
{
  public class BoardPrinter : IOutputHelper
  {
    const ushort mask = 0b1000_0000_0000_0000;
    private ITestOutputHelper output;

    public BoardPrinter(ITestOutputHelper output)
      => this.output = output;

    public void PrintBoard(ushort[] board, ushort[] masks, int size)
    {
      output.WriteLine(">>> Printing board");
      for (int i = 0; i < size; i += 1)
      {
        string row = board[i].ToBinaryString(masks[i]);
        output.WriteLine(row[0..size]);
      }
      output.WriteLine("<<< Printing board");
    }

    //private void PrintRow(ushort row, int size)
    //{
    //  for (int i = 0; i < size; i += 1)
    //  {
    //    if ((row & mask) == mask)
    //    {
    //      Debug.Write("1");
    //    }
    //    else
    //    {
    //      Debug.Write("0");
    //    }
    //    row <<= 1;
    //  }
    //  Debug.Write(Environment.NewLine);
    //}
  }
}
