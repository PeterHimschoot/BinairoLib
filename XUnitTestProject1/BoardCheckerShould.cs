using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinairoLib.Tests
{
  public class BoardCheckerShould : IClassFixture<Valid6X6>
  {
    private BinairoRows validRows;

    public static ushort[] Mask6x6 = new ushort[] {
      0b1111_1100_0000_0000,
      0b1111_1100_0000_0000,
      0b1111_1100_0000_0000,
      0b1111_1100_0000_0000,
      0b1111_1100_0000_0000,
      0b1111_1100_0000_0000
    };

    public static IEnumerable<object[]> SomeValidRowsWithMasks
    {
      get => new List<object[]>
      {
        new object[] { 0b1101_0000_0000_0000, 0b1111_1100_0000_0000 },
        new object[] { 0b1101_0000_0000_0000, 0b1110_0000_0000_0000  },
        new object[] { 0b1101_0000_0000_0000, 0b0001_1100_0000_0000  },
        new object[] { 0b1101_0000_0000_0000, 0b0001_0000_0000_0000  }
      };
    }
    public static IEnumerable<object[]> SomeInvalidRowsWithMasks
    {
      get => new List<object[]>
      {
        new object[] { 0b1000_1000_0000_0000, 0b1111_1100_0000_0000  },
        new object[] { 0b0001_0000_0000_0000, 0b1110_0000_0000_0000 },
        new object[] { 0b1000_0000_0000_0000, 0b0001_1100_0000_0000 }
      };
    }

    public static IEnumerable<object[]> SomeValidBoards
    {
      get 
      {
        yield return new object[] {
          new ushort[]
          {
            0b1101_0000_0000_0000,
            0b1001_0100_0000_0000,
            0b0010_1100_0000_0000,
            0b0101_1000_0000_0000,
            0b1010_0100_0000_0000,
            0b0110_1000_0000_0000
          }, Mask6x6
        };
      }
    }

    public static IEnumerable<object[]> SomeInvalidBoards
    {
      get
      {
        yield return new object[] {
          new ushort[]
          {
            0b1101_0000_0000_0000,
            0b1001_0100_0000_0000,
            0b0010_1100_0000_0000,
            0b0101_1000_0000_0000, // duplicate row
            0b1101_0000_0000_0000,
            0b0110_1000_0000_0000
          }, Mask6x6
        };
      }
    }

    public BoardCheckerShould(Valid6X6 validRows)
    {
      this.validRows = validRows;
    }

    [Theory]
    [MemberData(nameof(SomeValidRowsWithMasks))]
    public void RecognizeValidRows(ushort row, ushort mask)
    {
      var boardChecker = new BinairoRowChecker(validRows, size: 6);
      Assert.True(boardChecker.IsValid(row, mask));
    }

    [Theory]
    [MemberData(nameof(SomeInvalidRowsWithMasks))]
    public void RecognizeInvalidRows(ushort row, ushort mask)
    {
      var boardChecker = new BinairoRowChecker(validRows, size: 6);
      Assert.False(boardChecker.IsValid(row, mask));
    }

    [Theory]
    [MemberData(nameof(SomeValidBoards))]
    public void RecognizeValidBoards(ushort[] board, ushort[] mask)
    {
      var boardChecker = new BinairoRowChecker(validRows, size: 6);
      Assert.True(boardChecker.IsValid(board, mask));
    }

    [Theory]
    [MemberData(nameof(SomeInvalidBoards))]
    public void RecognizeInvalidBoards(ushort[] board, ushort[] mask)
    {
      var boardChecker = new BinairoRowChecker(validRows, size: 6);
      Assert.False(boardChecker.IsValid(board, mask));
    }
  }
}
