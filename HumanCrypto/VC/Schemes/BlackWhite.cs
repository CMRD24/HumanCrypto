using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace SecHCI.Schemes
{
    internal class BlackWhite
    {
        Bitmap key;
        public Bitmap generateKey()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            Bitmap bmp = new Bitmap(100, 100);
            
            for (int i = 0; i < bmp.Width; i ++)
            {
                for (int j = 0; j < bmp.Height; j ++)
                {
                    byte[] random = new byte[1];
                    rng.GetBytes(random);
                    bool setWhite = random[0] % 2 == 0;
                    Color c = Color.Black;
                    if (setWhite)
                    {
                        c = Color.White;
                    }
                    bmp.SetPixel(i, j, c);
                }
            }
            key = bmp;
            return bmp;
        }

        public Bitmap generateChallenge()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            Random r = new Random();
            Bitmap bmp = new Bitmap(100, 100);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    byte[] random = new byte[1];
                    rng.GetBytes(random);
                    bool setWhite = random[0] % 2 == 0;
                    Color c = Color.Black;
                    if (setWhite)
                    {
                        c = Color.White;
                    }
                    bmp.SetPixel(i, j, c);
                }
            }

            FileInfo[] files = new DirectoryInfo("imgs").EnumerateFiles().ToArray();
            string path = files[r.Next(files.Length)].FullName;
            Bitmap secret = (Bitmap)Image.FromFile(path);
            //get random offsets
            //todo
            int x_offset = r.Next(100 - secret.Width);
            int y_offset = r.Next(100 - secret.Height);

            for (int i = x_offset; i < x_offset + secret.Width; i++)
            {
                for (int j = y_offset; j < y_offset + secret.Height; j++)
                {
                    if (secret.GetPixel(i - x_offset, j - y_offset).R == 0)
                    {
                        //is black
                        Color keyColor = key.GetPixel(i, j);

                        if (keyColor.R==0)
                        {
                            bmp.SetPixel(i, j, Color.White);
                        }
                        else
                        {
                            bmp.SetPixel(i, j, Color.Black);
                        }
                    }

                }
            }
            return bmp;
        }

    }
}
