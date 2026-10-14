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
        public static (Bitmap Image, double Mean, double StdDev) CropImage(Bitmap source, int xMin, int yMin, int xMax, int yMax)
        {
            if (source == null) return (null, 0, 0);

            int rectWidth = xMax - xMin;
            int rectHeight = yMax - yMin;
            Bitmap cropped = new Bitmap(rectWidth, rectHeight);

            long sumR = 0, sumG = 0, sumB = 0;
            int N = rectWidth * rectHeight;

            for (int y = 0; y < rectHeight; y++)
            {
                for (int x = 0; x < rectWidth; x++)
                {
                    Color p = source.GetPixel(xMin + x, yMin + y);
                    cropped.SetPixel(x, y, p);

                    sumR += p.R;
                    sumG += p.G;
                    sumB += p.B;
                }
            }

            double meanR = (double)sumR / N;
            double meanG = (double)sumG / N;
            double meanB = (double)sumB / N;

            double mean = (meanR + meanG + meanB) / 3.0;

            double sumSqDiff = 0;
            for (int y = 0; y < rectHeight; y++)
            {
                for (int x = 0; x < rectWidth; x++)
                {
                    Color p = cropped.GetPixel(x, y);
                    double intensity = (p.R + p.G + p.B) / 3.0;
                    sumSqDiff += Math.Pow(intensity - mean, 2);
                }
            }

            double stdDev = Math.Sqrt(sumSqDiff / N);

            return (cropped, mean, stdDev);
        }
    }
}