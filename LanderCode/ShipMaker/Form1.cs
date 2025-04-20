namespace ShipMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _updateTimer.Enabled = true;
            _shipData.VisibleCount += OnVisibleCount;
        }

        private void DoUpdate(object? s, EventArgs e)
        {
            _updateTimer.Enabled = false;
            var lastBitmap = _bitmap;
            _bitmap = _shipData.DrawShip(_hTrackBar.Value * 360 / 32, _vTrackBar.Value * 180 / 32);
            _shipImage.Image = _bitmap;
            lastBitmap?.Dispose();
        }

        private void TrackerMoved(object? s, EventArgs e)
        {
            _updateTimer.Enabled = false;
            _updateTimer.Enabled = true;
        }

        private void OnVisibleCount(List<int[]> vertices, double[,] coordinates)
        {
            var hexData = CreateHexDump(vertices, coordinates);

            var hexString = "";
            for (int i = 0; i < hexData.Length; i++)
            {
                hexString += $"{_tempData[i]:x2} ";
            }
            _hexDump.Text = hexString;
            _visibleCount.Text = $"{vertices.Count} | {coordinates.GetLength(0)} || {hexData.Length}";
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                _shipData.VisibleCount -= OnVisibleCount;
                _shipData.VisibleCount += AddToHex;
                _imageDataOffset = 2 * YSteps * ZSteps;
                _imageDataCount = 0;

                for (int z = 0; z < ZSteps; z++)
                {
                    for (int y = 0; y < YSteps; y++)
                    {
                        var bitmap = _shipData.DrawShip(z * 360 / ZSteps, y * 90 / YSteps);
                        bitmap?.Dispose();
                    }
                }

                byte[] outputHex = new byte[_imageDataOffset];
                Array.Copy(_imageData, 0, outputHex, 0, _imageDataOffset);
                File.WriteAllBytes(@"..\..\..\..\..\shipdata.bin", outputHex);
            }
            finally
            {
                _shipData.VisibleCount -= AddToHex;
                _shipData.VisibleCount += OnVisibleCount;
            }
        }

        private void AddToHex(List<int[]> vertices, double[,] coordinates)
        {
            _imageData[_imageDataCount++] = (byte)(_imageDataOffset & 0xff);
            _imageData[_imageDataCount++] = (byte)(_imageDataOffset >> 8);
            var hexData = CreateHexDump(vertices, coordinates);
            Array.Copy(hexData, 0, _imageData, _imageDataOffset, hexData.Length);
            _imageDataOffset += hexData.Length;
        }

        private byte[] CreateHexDump(List<int[]> vertices, double[,] coordinates)
        {
            Dictionary<int, int> usedPoints = new();
            List<double> xPoints = new();
            List<double> yPoints = new();
            foreach (var v in vertices)
            {
                if (!usedPoints.ContainsKey(v[0]))
                {
                    usedPoints.Add(v[0], xPoints.Count);
                    xPoints.Add(coordinates[v[0], 0]);
                    yPoints.Add(coordinates[v[0], 1]);
                }
                if (!usedPoints.ContainsKey(v[1]))
                {
                    usedPoints.Add(v[1], xPoints.Count);
                    xPoints.Add(coordinates[v[1], 0]);
                    yPoints.Add(coordinates[v[1], 1]);
                }
            }

            int byteOffset = 1;
            foreach (var v in vertices)
            {
                _tempData[byteOffset++] = (byte)((usedPoints[v[0]] << 4) | usedPoints[v[1]]);
            }
            for (int i = 0; i < xPoints.Count; i++)
            {
                _tempData[byteOffset++] = (byte)(ToCoordinate(xPoints[i]) | (ToCoordinate(yPoints[i]) << 4));
            }
            _tempData[0] = (byte)((xPoints.Count << 4) | vertices.Count);
            byte[] actualData = new byte[byteOffset];
            Array.Copy(_tempData, actualData, byteOffset);
            return actualData;
        }

        private byte ToCoordinate(double d)
        {
            var value = (d + 7.5);
            if (value > 15) value = 15;
            if (value < 0) value = 0;

            return (byte)(value);
        }

        private ShipData _shipData = new();
        private Bitmap? _bitmap;
        private byte[] _tempData = new byte[256];
        private byte[] _imageData = new byte[4096];
        private int _imageDataOffset = 0;
        private int _imageDataCount = 0;
        private const int YSteps = 16;
        private const int ZSteps = 8;
    }
}
