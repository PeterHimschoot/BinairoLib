using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public interface IRowSolver
  {
    bool Solve(ref ushort row, ref ushort mask, int size);
  }
}
