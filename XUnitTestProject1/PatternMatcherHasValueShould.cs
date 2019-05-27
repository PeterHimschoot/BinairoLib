using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinairoLib.Tests
{
  public class valueMatcherHasValueShould
  {
    public IEnumerable<object[]> PositiveMatches
    {
      get
      {
        yield return new object[] { };
      }
    }
    public IEnumerable<object[]> NegativeMatches
    {
      get
      {
        yield return new object[] { };
      }
    }

    public void BePositiveMatch(ushort row, ushort valueMask, ushort value)
    {
      Assert.True(row.HasValue(valueMask, value));
    }

    public void BeNegativeMatch(ushort row, ushort valueMask, ushort value)
    {
      Assert.False(row.HasValue(valueMask, value));
    }
  }

}
