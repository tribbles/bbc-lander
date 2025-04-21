using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipMaker
{
    internal class ShipData
    {
        public Bitmap DrawShip(int rotY, int rotZ)
        {
            var bitmap = new Bitmap(ImageSize, ImageSize, PixelFormat.Format24bppRgb);
            /*var pal = bitmap.Palette;
            pal.Entries[0] = Color.FromArgb(0, 0, 0);
            pal.Entries[1] = Color.FromArgb(255, 255, 255);
            bitmap.Palette = pal;*/

            var points3D = RotateShip(rotY, rotZ);
            var points2D = To2D(points3D);

            List<int[]> vertices = new List<int[]>();

            for (int t = 0; t < _shipTriangles.GetLength(0); t++)
            {
                var p0 = _shipTriangles[t, 0];
                var p1 = _shipTriangles[t, 1];
                var p2 = _shipTriangles[t, 2];

                var x0 = points2D[p0, 0];
                var y0 = points2D[p0, 1];
                var x1 = points2D[p1, 0];
                var y1 = points2D[p1, 1];
                var x2 = points2D[p2, 0];
                var y2 = points2D[p2, 1];

                if ((x1 - x0) * (y1 - y2) - (y1 - y0) * (x1 - x2) >= 0) continue;

                // Draw this triangle!
                AddVertex(vertices, p0, p1);
                AddVertex(vertices, p1, p2);
                AddVertex(vertices, p0, p2);
            }

            using (var g = Graphics.FromImage(bitmap))
            {
                foreach (var vertex in vertices)
                {
                    g.DrawLine(
                        Pens.White,
                        ToImagePosition(points2D[vertex[0], 0]),
                        ToImagePosition(points2D[vertex[0], 1]),
                        ToImagePosition(points2D[vertex[1], 0]),
                        ToImagePosition(points2D[vertex[1], 1]));
                }
            }

            VisibleCount?.Invoke(vertices, points2D);

            return bitmap;
        }

        public void AddVertex(List<int[]> vertices, int p0, int p1)
        {
            if (!vertices.Any(x => (x[0] == p0 && x[1] == p1) || (x[0] == p1 && x[1] == p0)))
            {
                vertices.Add(new int[2] { p0, p1 });
            }
        }

        public Action<List<int[]>, double[,]>? VisibleCount { get; set; }

        private int ToImagePosition(double value) => (int)((value * ImageSize / 15.0) + (ImageSize / 2.0));

        private double[,] To2D(double[,] points)
        {
            double[,] to2D = new double[points.GetLength(0), 2];

            for (int p = 0; p < points.GetLength(0); p++)
            {
                double x = points[p, 0];
                double y = points[p, 1];
                double z = points[p, 2];

                to2D[p, 0] = x * Scale * ZDepth / (ZDepth - z);
                to2D[p, 1] = y * Scale * ZDepth / (ZDepth - z);
            }

            return to2D;
        }

        private double[,] RotateShip(int rotY, int rotZ)
        {
            var points = new double[_shipPoints.GetLength(0), 3];

            var siny = (double)Math.Sin(rotY * Math.PI / 180.0);
            var cosy = (double)Math.Cos(rotY * Math.PI / 180.0);
            var sinz = (double)Math.Sin(rotZ * Math.PI / 180.0);
            var cosz = (double)Math.Cos(rotZ * Math.PI / 180.0);

            for (int p = 0; p < points.GetLength(0); p++)
            {
                double x = _shipPoints[p, 0];
                double y = _shipPoints[p, 1];
                double z = _shipPoints[p, 2];

                // Rotate by Z
                {
                    var nx = x * cosz - y * sinz;
                    var ny = y * cosz + x * sinz;
                    x = nx;
                    y = ny;
                }

                // Rotate by Y
                {
                    var nx = x * cosy - z * siny;
                    var nz = z * cosy + x * siny;
                    x = nx;
                    z = nz;
                }

                points[p, 0] = x;
                points[p, 1] = y;
                points[p, 2] = z;
            }

            return points;
        }

        double[,] _shipPoints = new double[,]
        {
            {0, -10, 5 },
            {0, 0, -15 },
            {18, 0, 0 },
            {5, 0, 18 },
            {-5, 0, 18 },
            {-18, 0, 0 },
        };

        int[,] _shipTriangles = new int[,]
        {
            {0, 1, 2 },
            {0, 2, 3 },
            {0, 3, 4 },
            {0, 4, 5 },
            {0, 5, 1 },
            {1, 3, 2 },
            {1, 4, 3 },
            {1, 5, 4 },
        };

        private const double ZDepth = 50.0;
        private const double Scale = 5.5 / 20.0;
        private const int ImageSize = 127;
    }
}
