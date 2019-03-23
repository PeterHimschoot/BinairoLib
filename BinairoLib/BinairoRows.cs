using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public class BinairoRows
  {
    private readonly int size;
    private readonly ushort[] rows;
    private readonly Memory<ushort> rowSpan;

    public BinairoRows(BinairoRowGenerator generator, int size)
      : this(generator.GenerateAllValid(size), size) { }

    public BinairoRows(ushort[] rows, int size)
    {
      this.size = size;
      this.rows = rows;
      this.rowSpan = new Memory<ushort>(this.rows);
    }

    public int Size => this.size;

    public int Length => this.rowSpan.Length;

    public ref ushort this[int index] => ref rowSpan.Span[index];

  }
}
