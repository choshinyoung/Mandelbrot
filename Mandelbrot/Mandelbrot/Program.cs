using System;
using System.Drawing;
using System.Numerics;

namespace Mandelbrot
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 1000, height = 1000;
            int max = 1000, InfinitValue = 35;

            Bitmap bmp = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                //Console.WriteLine($"Processing.. {x} / {width}");
                for (int y = 0; y < height; y++)
                {
                    Complex z = Complex.Zero;
                    Complex c = new Complex((x - (width / 5) - width / 2) * 2.5 / width, (y - height / 2) * 2.5 / width);

                    int i = 0;
                    while(i < max)
                    {
                        z = (z * z) + c;
                        if (Complex.Abs(z) > InfinitValue) break;

                        i++;
                    }

                    if (i >= max)
                        bmp.SetPixel(x, y, Color.Black);
                    else
                    {
                        double color = (i * (360 / 5) / InfinitValue) + 200;
                        if (color > 360) color -= 360;
                        if (color < 0) color += 360;

                        bmp.SetPixel(x, y, ColorFromHSV(color, 1, 1));
                    }
                }
            }

            bmp.Save($"{max}-{width}.png");
        }

        /* https://stackoverflow.com/questions/1335426/is-there-a-built-in-c-net-system-api-for-hsv-to-rgb */
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}
 
