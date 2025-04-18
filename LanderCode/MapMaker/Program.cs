using System;
using System.IO;

namespace MapMaker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            map = new double[64, 64];
            const byte MaxY = 47;

            map[0, 0] = 48.0;
            divide(64);

            double min = map[0, 0];
            double max = map[0, 0];
            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    min = Math.Min(min, map[x, y]);
                    max = Math.Max(max, map[x, y]);
                }
            }

            var delta = max - min;

            byte[] data = new byte[64 * 64];

            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    var h = (byte)((map[x, y] - min) / delta * 64);

                    if (h < 0)
                    {
                        h = 0;
                    }
                    if (h > MaxY)
                    {
                        h = MaxY;
                    }

                    data[x + y * 64] = h;
                }
            }

            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    if (GetY(x, y, data) != MaxY) continue;
                    if (GetY(x + 1, y, data) != MaxY) continue;
                    //if (GetY(x - 1, y, data) != MaxY) continue;
                    if (GetY(x, y + 1, data) != MaxY) continue;
                    if (GetY(x + 1, y + 1, data) != MaxY) continue;
                    //if (GetY(x, y - 1, data) != MaxY) continue;
                    data[x + y * 64] |= 64;
                }
            }

            File.WriteAllBytes(@"..\map.bin", data);
        }

        private static byte GetY(int x, int y, byte[] map)
        {
            return map[(x & 63) + ((y & 63) * 64)];
        }

        private static void divide(int size)
        {
            var half = size >> 1;
            if (half == 0)
            {
                return;
            }

            var scale = size * 1.1;
            for (int y = half; y < 64; y += size)
            {
                for (int x = half; x < 64; x += size)
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

        private static void diamond(int x, int y, int size, double offset)
        {
            var avg = (height(x, y - size) + height(x + size, y) + height(x, y + size) + height(x - size, y)) / 4;
            setHeight(x, y, avg + offset);
        }

        private static void square(int x, int y, int size, double offset)
        {
            var avg = (height(x - size, y - size) + height(x + size, y - size) + height(x - size, y + size) + height(x + size, y + size)) / 4;
            setHeight(x, y, avg + offset);
        }

        private static void setHeight(int x, int y, double h)
        {
            map[x & 63, y & 63] = h;
        }

        private static double height(int x, int y)
        {
            return map[x & 63, y & 63];
        }

        private static double[,] map;
        private static Random r = new Random();
    }
}
