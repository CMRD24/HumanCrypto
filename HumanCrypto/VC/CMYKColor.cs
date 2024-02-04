using System.Drawing.Imaging;
using System.Drawing;
using System;


namespace SecHCI
{
    public class CMYKColor
    {
        public float C { get; set; }
        public float M { get; set; }
        public float Y { get; set; }
        public float K { get; set; }

        public CMYKColor(float c, float m, float y, float k)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
        }

        public void Superimpose(CMYKColor color)
        {
            C += color.C;
            M += color.M;
            Y += color.Y;
            K += color.K;
        }

        public void RemoveSuperimp(CMYKColor color)
        {
            C -= color.C;
            M -= color.M;
            Y -= color.Y;
            K -= color.K;
        }


        public Color ConvertToRgb()
        {
            int r;
            int g;
            int b;

            r = Convert.ToInt32(255 * (1 - C) * (1 - K));
            g = Convert.ToInt32(255 * (1 - M) * (1 - K));
            b = Convert.ToInt32(255 * (1 - Y) * (1 - K));
            if (r < 0) { r = 0; }
            if (g < 0) { g = 0; }
            if (b < 0) { b = 0; }
            if (r > 255) { r = 255; }
            if (g > 255) { g = 255; }
            if (b > 255) { b = 255; }
            return Color.FromArgb(r, g, b);
        }

        public static CMYKColor FromRGB(Color rgb)
        {
            float c;
            float m;
            float y;
            float k;
            float rf;
            float gf;
            float bf;

            rf = rgb.R / 255F;
            gf = rgb.G / 255F;
            bf = rgb.B / 255F;

            k = ClampCmyk(1 - Math.Max(Math.Max(rf, gf), bf));
            c = ClampCmyk((1 - rf - k) / (1 - k));
            m = ClampCmyk((1 - gf - k) / (1 - k));
            y = ClampCmyk((1 - bf - k) / (1 - k));

            return new CMYKColor(c, m, y, k);
        }

        private static float ClampCmyk(float value)
        {
            if (value < 0 || float.IsNaN(value))
            {
                value = 0;
            }

            return value;
        }
    }
}