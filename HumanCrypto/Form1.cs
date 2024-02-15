using SecHCI;
using SecHCI.Schemes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;

namespace HumanCrypto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void expectedEffortAddition(int n)
        {
            int t_plus = 0;
            int t_carry = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i < 10 && j < 10)
                    {
                        if (i % 10 + j % 10 < 10)
                        {
                            t_plus++;
                        }
                        else
                        {
                            t_carry++;
                            t_plus++;
                        }


                    }
                    //lambda=1
                    else if (i < 10 || j < 10)
                    {
                        if (i % 10 + j % 10 < 10)
                        {
                            t_plus++;
                            t_carry++;
                        }
                        else
                        {
                            t_plus += 2;
                            t_carry++;
                        }

                    }
                    //lambda=2
                    else
                    {
                        if (i % 10 + j % 10 < 10)
                        {
                            t_plus += 2;
                        }
                        else
                        {
                            t_plus += 3;
                            t_carry++;
                        }
                    }

                }
            }
            MessageBox.Show($"t_plus: {t_plus}\nt_carry: {t_carry}");
        }

        private void expectedEffortAdditionZ(int n)
        {
            int t_plus = 0;
            int t_carry = 0;
            int j = n - 1;
            for (int i = 0; i < n; i++)
            {

                if (i < 10 && j < 10)
                {
                    if (i % 10 + j % 10 < 10)
                    {
                        t_plus++;
                    }
                    else
                    {
                        t_carry++;
                        t_plus++;
                    }


                }
                //lambda=1
                else if (i < 10 || j < 10)
                {
                    if (i % 10 + j % 10 < 10)
                    {
                        t_plus++;
                        t_carry++;
                    }
                    else
                    {
                        t_plus += 2;
                        t_carry++;
                    }

                }
                //lambda=2
                else
                {
                    if (i % 10 + j % 10 < 10)
                    {
                        t_plus += 2;
                    }
                    else
                    {
                        t_plus += 3;
                        t_carry++;
                    }
                }


            }
            MessageBox.Show($"t_plus: {t_plus}\nt_carry: {t_carry}");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            expectedEffortAdditionZ(26);
            expectedEffortAddition(26);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Panel p = new Panel();
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Location = new Point(496, 318);
            p.Size = new Size(200, 200);
            int a = 1;
            int b = 2;
            int c = 3;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Label l = new Label();
                    l.Name = i + ":" + j;
                    l.Text = ((a * i + b * j + c) % 10).ToString();//i + "," + j;
                    l.Location = new Point(i * 20, j * 20);
                    l.Visible = true;
                    l.TextAlign = ContentAlignment.MiddleCenter;
                    l.Size = new Size(20, 20);
                    if (j == 0 && i == 5)
                    {
                        l.BackColor = Color.FromArgb(120, 120, 255);
                    }
                    else if (i == 5)
                    {
                        l.BackColor = Color.FromArgb(160, 160, 255);
                    }



                    if (j == 3 && i == 0)
                    {
                        l.BackColor = Color.FromArgb(255, 255, 120);
                    }
                    else if (j == 3)
                    {
                        l.BackColor = Color.FromArgb(255, 255, 160);
                    }

                    if (j == 0 && i == 0)
                    {
                        l.BackColor = Color.FromArgb(255, 120, 120);
                    }
                    if (j == 3 && i == 5)
                    {
                        l.BackColor = Color.FromArgb(120, 255, 120);
                    }

                    l.BringToFront();
                    p.Controls.Add(l);
                }
            }
            Controls.Add(p);
        }

        Bitmap key;
        Bitmap challenge;

        BlackWhite blackWhite;

        private void button3_Click(object sender, EventArgs e)
        {
            blackWhite = new BlackWhite();
            Bitmap bmp = blackWhite.generateKey();

            pictureBox1.Image = bmp;
            key = bmp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            challengeEvent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = superimpose(key, challenge);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox4.Image = drawSamePixels((Bitmap)pictureBox5.Image, challenge, Color.Red);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            challengeEvent();
            pictureBox3.Image = superimpose(key, challenge);
            pictureBox4.Image = drawSamePixels((Bitmap)pictureBox5.Image, challenge, Color.Red);
        }

        private void challengeEvent()
        {
            Bitmap bmp2 = blackWhite.generateChallenge();

            if (challenge != null)
            {
                pictureBox5.Image = challenge;
            }

            pictureBox2.Image = bmp2;
            challenge = bmp2;
        }

        private Bitmap superimpose(Bitmap b1, Bitmap b2)
        {
            Bitmap bmp = new Bitmap(100, 100);
            List<Color> colors = new List<Color>();
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    CMYKColor color = CMYKColor.FromRGB(b1.GetPixel(i, j));
                    color.Superimpose(CMYKColor.FromRGB(b2.GetPixel(i, j)));
                    Color overlay = color.ConvertToRgb();
                    colors.Add(overlay);
                    bmp.SetPixel(i, j, overlay);
                }
            }
            //int r = colors.Select(x => (int)x.R).Sum() / colors.Count;
            //int g = colors.Select(x => (int)x.G).Sum() / colors.Count;
            //int b = colors.Select(x => (int)x.B).Sum() / colors.Count;
            return bmp;
        }

        private Bitmap drawSamePixels(Bitmap b1, Bitmap b2, Color marker)
        {
            Bitmap bmp = new Bitmap(100, 100);
            List<Color> colors = new List<Color>();
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pixel = b1.GetPixel(i, j);
                    Color compare = b2.GetPixel(i, j);
                    if (pixel.R == compare.R
                        && pixel.G == compare.G
                        && pixel.B == compare.B
                        )
                    {
                        bmp.SetPixel(i, j, marker);
                    }
                    else
                    {
                        bmp.SetPixel(i, j, pixel);
                    }

                }
            }
            //int r = colors.Select(x => (int)x.R).Sum() / colors.Count;
            //int g = colors.Select(x => (int)x.G).Sum() / colors.Count;
            //int b = colors.Select(x => (int)x.B).Sum() / colors.Count;
            return bmp;
        }

        private byte[] getRandomSTMLMap()
        {
            byte[] data = new byte[10];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(data);
            return data.Select(x => (byte)(x % 10)).ToArray();
        }

        private byte[] getRandomSTMLSeed5(int length)
        {
            byte[] data = new byte[10+2*length];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(data);
            return data.Where(x => x<255).Select(x => (byte)(x % 5)).Take(length).ToArray();
        }

        private byte[] skipInsertN(byte[] data, int n)
        {
            return data.Where((x, i) => i % (2 * n) >= n).ToArray();
        }

        private byte[] stml_prg(byte[] data, byte[] map, byte initial)
        {
            byte s = initial;
            List<byte> result = new List<byte>();
            for (int i = 0; i < data.Length; i++)
            {
                s = (byte)((map[s] + data[i]) % 10);
                if (s < 5)
                {
                    result.Add(s);
                }
            }
            return result.ToArray();
        }

        private byte[] prg1(byte[] seed, byte[] map)
        {
            //expand seed:
            List<byte> expandedSeed = new List<byte>();
            expandedSeed.AddRange(seed);
            for (int i = 1; i <= seed.Length / 2; i++)
            {
                expandedSeed.AddRange(skipInsertN(seed, i));
            }
            //MessageBox.Show(string.Join("", expandedSeed.Select(x => x.ToString())));
            return stml_prg(expandedSeed.ToArray(), map, seed.Last());
        }


        private List<byte> convert5to256Random(byte[] data)
        {
            List<byte> list = new List<byte>();
            for (int i = 0; i < data.Length - 3; i = i + 4)
            {

                int d = data[i] * 125 + data[i + 1] * 25 + data[i + 2] * 5 + data[i + 3];
                if (d >= 512)
                {
                    continue;
                }
                list.Add((byte)(d % 256));
            }
            return list;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            /*byte[] result = prg1(new byte[] { 3,1,4,1,5,9,2,6}, new byte[] {0,3,6,9,2,5,8,1,4,7 });
            MessageBox.Show(string.Join("", result.Select(x => x.ToString())));
            result = prg1(getRandomSTMLSeed5(20), getRandomSTMLMap());
            MessageBox.Show(string.Join("", result.Select(x => x.ToString())));
            var r2 = convert5to256Random(result);
            MessageBox.Show(string.Join(",", r2.Select(x => x.ToString())));*/
            //var map = getRandomSTMLMap();
            var map = new byte[] { 0, 3, 6, 9, 2, 5, 8, 1, 4, 7 };
            using (var stream = new FileStream("stml-prg.bin", FileMode.Append))
            {
                for (int i = 0; i < 10000000; i++)
                {
                    var result = prg1(getRandomSTMLSeed5(10), map);
                    var converted = convert5to256Random(result).ToArray();
                    stream.Write(converted, 0, converted.Length);
                }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (var stream = new FileStream("sanity.bin", FileMode.Append))
            {
                for (int i = 0; i < 10000000; i++)
                {
                    var result = getRandomSTMLSeed5(10);
                    var converted = convert5to256Random(result).ToArray();
                    stream.Write(converted, 0, converted.Length);
                }

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (var stream = new FileStream("sanity256.bin", FileMode.Append))
            {
                for (int i = 0; i < 10000000; i++)
                {
                    byte[] data = new byte[4];
                    RandomNumberGenerator rng = RandomNumberGenerator.Create();
                    rng.GetBytes(data);
                    stream.Write(data, 0, data.Length);
                }

            }
            
        }
    }
}
