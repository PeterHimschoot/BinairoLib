using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib
{
  public static class PatternMatcher
  {
    public static bool HasPattern(this ushort mask, ushort patternMask, ushort pattern)
      //=> (mask ^ pattern) == ~pattern;
      => (mask & patternMask) == pattern;

    public static bool HasValue(this ushort row, ushort valueMask, ushort value)
      => (row & valueMask) == value;
  }
}
