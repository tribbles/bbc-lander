using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitleMaker
{
  class Program
  {
    static void Main(string[] args)
    {
      var width = 256;
      var height = 16;
      var srcName = string.Empty;
      var dstName = string.Empty;

      for(int i = 0; i < args.Length; i++)
      {
        switch (args[i])
        {
          case "-w":
            width = int.Parse(args[++i]);
            break;
          case "-h":
            height = int.Parse(args[++i]);
            break;
          case "-s":
            srcName = args[++i];
            break;
          case "-d":
            dstName = args[++i];
            break;
        }
      }

      if(string.IsNullOrEmpty(srcName))
      {
        Console.WriteLine("Missing '-s' parameter for source name");
        return;
      }

      if (string.IsNullOrEmpty(dstName))
      {
        Console.WriteLine("Missing '-d' parameter for destination name");
        return;
      }

      if(!File.Exists(srcName))
      {
        Console.WriteLine("Can't read '{0}'", srcName);
        return;
      }

      byte[] srcData = File.ReadAllBytes(srcName);
      var pixWidth = (width + 7) & 0xfff8;
      var pixHeight = (height + 7) & 0xfff8;
      byte[] dstData = new byte[pixWidth * pixHeight / 8];

      if (srcData.Length != width * height)
      {
        Console.WriteLine("File size doesn't match expected");
        return;
      }

      for (var y = 0; y < height; y++)
      {
        var yp = (y >> 3) * pixWidth + (y & 7);
        for (var x = 0; x < width; x++)
        {
          var xp = x & 0xfff8;
          if (srcData[x + y * width] != 0)
          {
            dstData[yp + xp] |= (byte)(128 >> (x & 7));
          }
        }
      }

      File.WriteAllBytes(dstName, dstData);
    }
  }
}
