using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public interface IOutputHelper
  {
    void PrintRow(ushort row, ushort mask, int size, string format = null);

    void PrintBoard(ushort[] board, ushort[] masks, int size, bool horizontal = true);

    void WarningIfNotValid();
  }
}
