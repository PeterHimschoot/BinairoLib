using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public class BinairoBoardChecker
  {
    private readonly BinairoRowChecker rowChecker;
    private readonly MatrixFlipper flipper;
    private readonly int size;
    private ushort[] columns;
    private ushort[] columnMasks;

    public BinairoBoardChecker(BinairoRowChecker rowChecker, MatrixFlipper flipper, int size)
    {
      this.rowChecker = rowChecker;
      this.flipper = flipper;
      this.size = size;
      this.columns = new ushort[size];
      this.columnMasks = new ushort[size];
    }

    public bool IsValid(in ushort[] rows, in ushort[] masks)
    {
      // First check the rows
      if (rowChecker.IsValid(in rows, in masks))
      {
        // Now check the columns by flipping the rows
        flipper.Flip(in rows, ref this.columns, this.size);
        flipper.Flip(in masks, ref columnMasks, this.size);
        return rowChecker.IsValid(in columns, columnMasks);
      }
      return false;
    }

    public bool IsInvalid(in ushort[] rows, in ushort[] masks)
      => !IsValid(in rows, in masks);
  }
}
