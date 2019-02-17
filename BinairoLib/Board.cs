using System;
using System.Runtime;

namespace BinairoLib
{
  [Flags]
  public enum Stone : byte
  {
    Blank = 0b00,
    Zero = 0b01,
    One = 0b10
  }

  public class Board
  {
    public int Size { get; }
    public int sqSize { get; }
    private Stone[] stones;

    public Board(int size)
    {
      this.Size = size;
      this.sqSize = size * size;
      this.stones = new Stone[sqSize];
      Clear();
    }

    public void Clear()
    {
      Span<Stone> sp = new Span<Stone>(stones);
      for (int i = 0; i < this.sqSize; i += 1)
      {
        sp[i] = Stone.Blank;
      }
    }

    //public Stone this[int row, int col]
    //{
    //  get => stones[row, col];
    //  set => stones[row, col] = value;
    //}

    public bool IsValid() => Constraint1();

    private bool Constraint1()
    {
      Span<Stone> sp = new Span<Stone>(stones);
      for (int iRow = 0; iRow < this.Size; iRow += 1)
      {
        for(int iCol = 0; iCol < this.Size-3; iCol +=1)
        {

        }
      }
      return true;
    }
  }
}
