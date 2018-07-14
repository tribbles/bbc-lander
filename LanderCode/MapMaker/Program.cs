using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMaker
{
  class Program
  {
    static void Main(string[] args)
    {
      map = new double[64, 64];

      map[0, 0] = 24.0;
      divide(64);

      byte[] data = new byte[64 * 64];

      for (int x = 0; x < 64; x++)
      {
        for (int y = 0; y < 63; y++)
        {
          var h = map[x, y] - 16;
          if (h < 0)
          {
            h = 0;
          }
          if(h > 32)
          {
            h = 32;
          }

          data[x + y * 64] = (byte)h;
        }
      }

      File.WriteAllBytes(@"..\map.bin", data);
    }

    static void divide(int size)
    {
      var half = size >> 1;
      if (half == 0)
      {
        return;
      }

      var scale = ((double)size) * 0.9;
      for(int y = half; y < 64; y += size)
      {
        for(int x = half; x < 64; x += size)
        {
          square(x, y, half, r.NextDouble() * scale * 2 - scale);
        }
      }

      for (int y = 0; y < 64; y += half)
      {
        for (int x = (y + half) % size; x < 64; x += size)
        {
          diamond(x, y, half, r.NextDouble() * scale * 2 - scale);
        }
      }

      divide(size / 2);
    }

    static void diamond(int x, int y, int size, double offset)
    {
      var avg = (height(x, y - size) + height(x + size, y) + height(x, y + size) + height(x - size, y)) / 4;
      setHeight(x, y, avg + offset);
    }

    static void square(int x, int y, int size, double offset)
    {
      var avg = (height(x - size, y - size) + height(x + size, y - size) + height(x - size, y + size) + height(x + size, y + size)) / 4;
      setHeight(x, y, avg + offset);
    }

    static void setHeight(int x, int y, double h)
    {
      map[x & 63, y & 63] = h;
    }

    static double height(int x, int y)
    {
      return map[x & 63, y & 63];
    }

    static double[,] map;
    static Random r = new Random();
  }
}
