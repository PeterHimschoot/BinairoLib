using System;
using System.Collections.Generic;
using System.Text;

namespace BinairoLib.Tests
{
  public class Valid6X6 : BinairoRows
  {
    public Valid6X6() : base(new BinairoRowGenerator(), 6) { }
  }

  public class Valid14x14 : BinairoRows
  {
    public Valid14x14() : base(new BinairoRowGenerator(), 14) { }
  }

}
