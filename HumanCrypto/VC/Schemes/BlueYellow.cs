using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;

namespace SecHCI.Schemes
{
    internal class BlueYellow
    {

        public Bitmap GenerateKey()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            Bitmap bmp = new Bitmap(100, 100);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    byte[] rgb = new byte[3];
                    rng.GetBytes(rgb);
                    
                    bmp.SetPixel(i, j, Color.FromArgb(255 - rgb[0], 255 - rgb[1], 255 - rgb[2]));
                }
            }
            return bmp;
        }
    }
}
