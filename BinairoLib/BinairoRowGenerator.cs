using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("BinairoLib.Tests")]

namespace BinairoLib
{
  public class BinairoRowGenerator
  {
    const byte zero = 0b0000_0000;
    const byte one = 0b0000_0001;

    private bool IsBalanced(byte[] row)
    {
      int zeros = 0;
      int ones = 0;
      foreach (byte b in row)
      {
        if (b == zero)
        {
          zeros += 1;
        }
        else if (b == one)
        {
          ones += 1;
        }
      }
      return zeros == ones;
    }

    public ushort[] GenerateAllValid(int size)
    {
      if (size > 14)
      {
        throw new ArgumentException(message: "This method supports up to size = 14");
      }
      var results = new List<byte[]>();
      byte[] row = new byte[size];
      for (byte pos0 = zero; pos0 <= one; pos0 += 1)
      {
        row[0] = pos0;
        for (byte pos1 = zero; pos1 <= one; pos1 += 1)
        {
          row[1] = pos1;
          for (byte pos2 = zero; pos2 <= one; pos2 += 1)
          {
            if (pos2 == row[1] && pos2 == row[0])
            {
              continue;
            }
            row[2] = pos2;
            for (byte pos3 = zero; pos3 <= one; pos3 += 1)
            {
              if (pos3 == row[2] && pos3 == row[1])
              {
                continue;
              }
              row[3] = pos3;
              for (byte pos4 = zero; pos4 <= one; pos4 += 1)
              {
                if (pos4 == row[3] && pos4 == row[2])
                {
                  continue;
                }
                row[4] = pos4;
                for (byte pos5 = zero; pos5 <= one; pos5 += 1)
                {
                  if (pos5 == row[4] && pos5 == row[3])
                  {
                    continue;
                  }
                  row[5] = pos5;
                  if (size == 6)
                  {
                    results.Add(row);
                    row = (byte[])row.Clone();
                  }
                  else
                  {
                    for (byte pos6 = zero; pos6 <= one; pos6 += 1)
                    {
                      if (pos6 == row[5] && pos6 == row[4])
                      {
                        continue;
                      }
                      row[6] = pos6;
                      for (byte pos7 = zero; pos7 <= one; pos7 += 1)
                      {
                        if (pos7 == row[6] && pos7 == row[5])
                        {
                          continue;
                        }
                        row[7] = pos7;
                        if (size == 8)
                        {
                          results.Add(row);
                          row = (byte[])row.Clone();
                        }
                        else
                        {
                          for (byte pos8 = zero; pos8 <= one; pos8 += 1)
                          {
                            if (pos8 == row[7] && pos8 == row[6])
                            {
                              continue;
                            }
                            row[8] = pos8;
                            for (byte pos9 = zero; pos9 <= one; pos9 += 1)
                            {
                              if (pos9 == row[8] && pos9 == row[7])
                              {
                                continue;
                              }
                              row[9] = pos9;
                              if (size == 10)
                              {
                                results.Add(row);
                                row = (byte[])row.Clone();
                              }
                              else
                              {
                                for (byte pos10 = zero; pos10 <= one; pos10 += 1)
                                {
                                  if (pos10 == row[9] && pos10 == row[8])
                                  {
                                    continue;
                                  }
                                  row[10] = pos10;
                                  for (byte pos11 = zero; pos11 <= one; pos11 += 1)
                                  {
                                    if (pos11 == row[10] && pos11 == row[9])
                                    {
                                      continue;
                                    }
                                    row[11] = pos11;
                                    if (size == 12)
                                    {
                                      results.Add(row);
                                      row = (byte[])row.Clone();
                                    }
                                    else
                                    {
                                      for (byte pos12 = zero; pos12 <= one; pos12 += 1)
                                      {
                                        if (pos12 == row[11] && pos12 == row[10])
                                        {
                                          continue;
                                        }
                                        row[12] = pos12;
                                        for (byte pos13 = zero; pos13 <= one; pos13 += 1)
                                        {
                                          if (pos13 == row[12] && pos13 == row[11])
                                          {
                                            continue;
                                          }
                                          row[13] = pos13;
                                          results.Add(row);
                                          row = (byte[])row.Clone();
                                        }
                                      }
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      ArrayToRowConverter converter = new ArrayToRowConverter();
      return results.Where(r => IsBalanced(r))
                    .Select(r => converter.Convert(r, size))
                    .ToArray();
    }
  }
}
