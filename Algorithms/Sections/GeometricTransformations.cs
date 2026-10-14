using System;
using System.Drawing;

namespace Algorithms.Sections
{
    public class GeometricTransformations
    {
        public static Bitmap MirrorImage(Bitmap source)
        {
            int width = source.Width;
            int height = source.Height;
            Bitmap result = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width / 2; x++)
                {
                    Color pixelLeft = source.GetPixel(x, y);
                    Color pixelRight = source.GetPixel(width - 1 - x, y);

                    result.SetPixel(x, y, pixelRight);
                    result.SetPixel(width - 1 - x, y, pixelLeft);
                }
            }
            return result;
        }

        public static Bitmap RotateClockwise(Bitmap source)
        {
            int w = source.Width;
            int h = source.Height;
            Bitmap resultCW = new Bitmap(h, w);
            Bitmap resultCCW = new Bitmap(h, w);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Color p = source.GetPixel(x, y);

                    resultCW.SetPixel(h - 1 - y, x, p);
                }
            }
            return resultCW;
        }

        public static Bitmap RotateAntiClockwise(Bitmap source)
        {
            int w = source.Width;
            int h = source.Height;
            Bitmap resultCW = new Bitmap(h, w);
            Bitmap resultCCW = new Bitmap(h, w);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Color p = source.GetPixel(x, y);
                    resultCCW.SetPixel(y, w - 1 - x, p);
                }
            }
            return resultCCW;
        }
    }
}