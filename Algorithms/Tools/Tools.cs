using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Algorithms.Tools
{
    public class Tools
    {
        #region Copy
        public static Image<Gray, byte> Copy(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = inputImage.Clone();
            return result;
        }

        public static Image<Bgr, byte> Copy(Image<Bgr, byte> inputImage)
        {
            Image<Bgr, byte> result = inputImage.Clone();
            return result;
        }
        #endregion

        #region Invert
        public static Image<Gray, byte> Invert(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    result.Data[y, x, 0] = (byte)(255 - inputImage.Data[y, x, 0]);
                }
            }
            return result;
        }

        public static Image<Bgr, byte> Invert(Image<Bgr, byte> inputImage)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Size);

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    result.Data[y, x, 0] = (byte)(255 - inputImage.Data[y, x, 0]);
                    result.Data[y, x, 1] = (byte)(255 - inputImage.Data[y, x, 1]);
                    result.Data[y, x, 2] = (byte)(255 - inputImage.Data[y, x, 2]);
                }
            }
            return result;
        }
        #endregion

        #region Convert color image to grayscale image
        public static Image<Gray, byte> Convert(Image<Bgr, byte> inputImage)
        {
            return inputImage.Convert<Gray, byte>();
        }

        #endregion
        
        #region Binary
        public static Image<Gray, byte> Binary(Image<Gray, byte> inputImage, byte threshhold = 128)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);
            for (int y = 0; y < result.Height; ++y)
            {
                for (int x = 0; x < result.Width; ++x)
                {
                    if (inputImage.Data[y, x, 0] >= threshhold)
                        result.Data[y, x, 0] = 255;
                    //else
                    //    result.Data[y,x,0] = 0;
                }
            }
            return result;
        }
        #endregion

        #region Crop Image
        public static (Bitmap cropped, double mean, double stdDev) CropAndCalculate(Bitmap source, int xMin, int yMin, int xMax, int yMax)
        {
            int rectWidth = xMax - xMin;
            int rectHeight = yMax - yMin;
            Bitmap croppedImage = new Bitmap(rectWidth, rectHeight);

            long sum = 0;
            int N = rectWidth * rectHeight;

            for (int y = 0; y < rectHeight; y++)
            {
                for (int x = 0; x < rectWidth; x++)
                {
                    Color p = source.GetPixel(xMin + x, yMin + y);
                    croppedImage.SetPixel(x, y, p);
                    sum += p.R;
                }
            }

            double mean = (double)sum / N;
            double sumSqDiff = 0;

            for (int y = 0; y < rectHeight; y++)
            {
                for (int x = 0; x < rectWidth; x++)
                {
                    Color p = croppedImage.GetPixel(x, y);
                    sumSqDiff += Math.Pow(p.R - mean, 2);
                }
            }

            double stdDev = Math.Sqrt(sumSqDiff / N);
            return (croppedImage, mean, stdDev);
        }
        #endregion 
    }
}