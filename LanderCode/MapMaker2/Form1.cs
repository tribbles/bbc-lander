using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace MapMaker2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _map = new();
            _map.OnMapGenerated += OnMapGenerated;
            _map.WaterLevel = 0.28;
            _waterHeight.Value = 28;
            _map.Regenerate(12345);
        }

        private void WaterHeight_ValueChanged(object sender, EventArgs e)
        {
            _waterHeightValue.Text = $"{_waterHeight.Value}%";
            _map.WaterLevel = _waterHeight.Value / 100.0;
            _changedTimer.Enabled = false;
            _changedTimer.Enabled = true;
        }

        private void ChangedTimer(object? s, EventArgs e)
        {
            _changedTimer.Enabled = false;
            OnMapGenerated();
        }

        private void OnMapGenerated()
        {
            var lastBitmap = _bitmap;
            _bitmap = new Bitmap(64, 64, PixelFormat.Format8bppIndexed);
            ColorPalette pal = _bitmap.Palette;
            _mapPicture.Image = _bitmap;
            lastBitmap?.Dispose();

            for (int n = 0; n < 64; n++)
            {
                var r = Math.Min(n * 16, 127);
                var g = Math.Min(Math.Max((n - 32) * 8, 0), 127);
                var b = Math.Min(Math.Max((n - 16) * 8, 0), 127);
                if (n == 47)
                {
                    r = 0; g = 0; b = 127;
                }
                else if (n >= 48)
                {
                    r = 127; g = 0; b = 127;
                }
                pal.Entries[n] = Color.FromArgb(r + 128, g + 128, b + 128);
                pal.Entries[n + 64] = Color.FromArgb(r, g | 128, b);
                pal.Entries[n + 128] = Color.FromArgb(r, 255, b);
                pal.Entries[n + 192] = Color.FromArgb(255, 0, 255);
            }
            pal.Entries[47] = Color.FromArgb(0, 0, 255);
            pal.Entries[47 + 64] = Color.FromArgb(127, 127, 255);
            _bitmap.Palette = pal;


            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    var h = 47 * _map.GetHeight(x, y);
                    _actualMap[x + y * 64] = (byte)h;
                }
            }

            Random rnd = new(10112);
            for (int tree = 0; tree < 64;)
            {
                int offset = rnd.Next() & (64 * 64 - 1);
                if (((_actualMap[offset] & 128) == 0) &&
                    (_actualMap[offset] != 47))
                {
                    _actualMap[offset] |= 128;
                    tree++;
                }
            }

            for (int x = 31; x <= 33; x++)
            {
                for (int y = 31; y <= 33; y++)
                {
                    int h = Math.Min(x, y) > 31 ? 72 : 8;
                    _actualMap[x + y * 64] = (byte)h;
                }
            }

            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    if (GetHeight(x, y) == 47 &&
                        GetHeight(x - 1, y) == 47 &&
                        GetHeight(x, y - 1) == 47)
                    {
                        _actualMap[x + y * 64] |= 64;
                    }
                }
            }

            var data = _bitmap.LockBits(new Rectangle(0, 0, 64, 64), ImageLockMode.ReadWrite, _bitmap.PixelFormat);
            var scan0 = data.Scan0;

            for (int y = 0; y < 64; y++)
            {
                Marshal.Copy(_actualMap, y * 64, scan0, 64);
                scan0 = new IntPtr(scan0.ToInt64() + data.Stride);
            }
            _bitmap.UnlockBits(data);
        }

        private byte GetHeight(int x, int y) => (byte)(_actualMap[(x & 63) + ((y & 63) * 64)] & 63);

        private Map _map;
        private Bitmap? _bitmap;
        private byte[] _actualMap = new byte[64 * 64];

        private void SaveButton_Click(object sender, EventArgs e)
        {
            File.WriteAllBytes(@"..\..\..\..\..\map.bin", _actualMap);
        }
    }
}
