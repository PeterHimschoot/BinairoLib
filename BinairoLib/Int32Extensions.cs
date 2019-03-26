using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public static class Int32Extensions
  {
    public static ushort ToMask(this int size)
      => (ushort) (0b1111_1111_1111_1111 << 16 - size);
  }
}
