using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMaker2
{
    internal class Map
    {
        public Map()
        {
            _random = new();
        }

        /// <summary>
        /// Regenerates the map based on the seed value
        /// </summary>
        /// <param name="seed">The seed number</param>
        public void Regenerate(int seed)
        {
            _random = new Random(seed);
            Regenerate();
        }


        /// <summary>
        /// Gets the height at a particular coordinate
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        /// <returns>The height, from 0 to 1</returns>
        /// <remarks>
        /// This will permit wrap-around
        /// </remarks>
        public double GetHeight(int x, int y) => Math.Min((GetRawHeight(x, y) - _minLevel) / ((1.0 - WaterLevel) * RawDelta), 1.0);

        /// <summary>
        /// Gets the raw map data
        /// </summary>
        public double[,] MapData => _map;

        /// <summary>
        /// Gets the raw delta value
        /// </summary>
        public double RawDelta { get; private set; }

        /// <summary>
        /// Gets the water level (0..1)
        /// </summary>
        public double WaterLevel { get; set; }

        /// <summary>
        /// Action called when the map is generated
        /// </summary>
        public Action? OnMapGenerated { get; set; }

        private double GetRawHeight(int x, int y) => _map[x & 63, y & 63];

        private void Regenerate()
        {
            _map[0, 0] = 48.0;
            Divide(64);
            Scale();
            OnMapGenerated?.Invoke();
        }

        private void Scale()
        {
            _minLevel = _map[0, 0];
            _maxLevel = _map[0, 0];

            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    _minLevel = Math.Min(_minLevel, _map[x, y]);
                    _maxLevel = Math.Max(_maxLevel, _map[x, y]);
                }
            }

            RawDelta = _maxLevel - _minLevel;
        }

        private void Divide(int size)
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
                    Square(x, y, half, _random.NextDouble() * scale * 2 - scale);
                }
            }

            for (int y = 0; y < 64; y += half)
            {
                for (int x = (y + half) % size; x < 64; x += size)
                {
                    Diamond(x, y, half, _random.NextDouble() * scale * 2 - scale);
                }
            }

            Divide(size / 2);
        }

        private void Diamond(int x, int y, int size, double offset)
        {
            var avg = (GetRawHeight(x, y - size) + GetRawHeight(x + size, y) + GetRawHeight(x, y + size) + GetRawHeight(x - size, y)) / 4;
            SetHeight(x, y, avg + offset);
        }

        private void Square(int x, int y, int size, double offset)
        {
            var avg = (GetRawHeight(x - size, y - size) + GetRawHeight(x + size, y - size) + GetRawHeight(x - size, y + size) + GetRawHeight(x + size, y + size)) / 4;
            SetHeight(x, y, avg + offset);
        }

        private void SetHeight(int x, int y, double h)
        {
            _map[x & 63, y & 63] = h;
        }

        private double[,] _map = new double[64, 64];
        private Random _random;
        private double _minLevel;
        private double _maxLevel;
    }
}
