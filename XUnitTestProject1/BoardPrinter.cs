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

    public void PrintBoard(ushort[] board, ushort[] masks, int size, bool horizontal = true)
    {
      output.WriteLine($">>> Printing board {(horizontal ? "HOR" : "VER")}");
      for (int i = 0; i < size; i += 1)
      {
        PrintRow(board[i], masks[i], size);
      }
      output.WriteLine("<<< Printing board");
    }

    public void PrintRow(ushort row, ushort mask, int size, string format = null)
    {
      format = format ?? "{0}";
      output.WriteLine(string.Format(format, row.ToBinaryString(mask)[0..size]));
    }

    public void WarningIfNotValid()
      => output.WriteLine("*** ERROR ***"); 

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
