using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public interface IBoardSolver
  {
    bool Solve(ushort[] rows, ushort[] masks, int size);
  }
}
