using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public interface IOutputHelper
  {
    void PrintBoard(ushort[] board, ushort[] masks, int size);
  }
}
