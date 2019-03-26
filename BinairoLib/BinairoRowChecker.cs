using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public class BinairoRowChecker
  {
    private readonly BinairoRows validRows;
    private readonly int validRowsLength;
    private readonly int size;

    public BinairoRowChecker(BinairoRows validRows, int size)
    {
      this.validRows = validRows;
      this.validRowsLength = validRows.Length;
      this.size = size;
    }

    // Example:   validRow       = 001101
    //            mask           = 111000
    //            maskedValidRow = 001000
    internal bool IsValid(in ushort row, in ushort mask)
    {
      for(int i = 0; i < this.validRows.Length; i += 1)
      {
        ushort validRow = validRows[i];
        ushort maskedValidRow = (ushort) (validRow & mask);
        // row should match one of the valid rows
        if ((row & maskedValidRow) == maskedValidRow) return true;
      }
      return false;
    }

    public bool IsValid(in ushort[] rows, in ushort[] masks)
    {
      // First check rows
      for(int i = 0; i < size; i += 1)
      {
        if(!IsValid(rows[i], masks[i]))
        {
          return false;
        }
      }
      // Next check if all rows are unique
      bool[] found = new bool[this.validRows.Length];
      for(int i = 0; i < size; i += 1)
      {
        ushort row = rows[i];
        for(int j = 0; j < validRowsLength; j +=1 )
        {
          if(row == validRows[j])
          {
            if(found[j])
            {
              return false;
            }
            found[j] = true;
            break; // no more need to check the other valid rows
          }
        }
      }
      return true;
    }
  }
}
