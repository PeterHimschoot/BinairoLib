using System;
using System.Runtime;

namespace BinairoLib
{
  public class Board
  {
    const byte zero = 0b0000_0000;
    const byte one = 0b0000_0001;

    public int Size { get; }

    private ushort[] rows;
    private ushort[] mask;
    private RowChecker checker;

    public Board(int size, RowChecker checker)
    {
      this.Size = size;
      this.rows = new ushort[size];
      this.mask = new ushort[size];
      this.checker = checker;
    }

    public bool IsValid() 
      => checker.IsValid(rows, mask);
  }
}
