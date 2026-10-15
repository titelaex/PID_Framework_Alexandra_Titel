using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Algorithms.Sections
{
    public class PointwiseOperations
    {

        #region Contrast and Brightness
        public static byte[] ContrastBrightness(double a, double b)
        {
            byte[] LUT = new byte[256];
            for(int r = 0; r < 256; r++)
            {
                double s = a * r + b;
                s = Math.Min(255, Math.Max(0, s));
                LUT[r] = (byte)Math.Round(s);
            }
            return LUT;
        }
        public static Image<Gray, byte> ApplyLUT(Image<Gray,byte>inputImage, byte[] LUT)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);
            for(int y = 0; y < inputImage.Height; y++)
            {
                for(int x = 0; x < inputImage.Width; x++)
                {
                    result.Data[y,x,0]= (byte)LUT[inputImage.Data[y,x,0]];
                }
            }
            return result;
        }
        #endregion

        #region Gamma operator
        public static byte[] GammaOperator(double gamma)
        {
            byte[] LUT = new byte[256];
            for (int r = 0; r < 256; r++)
            {
                double newValue = 255 * Math.Pow(r / 255.0, gamma);
                LUT[r] = (byte)Math.Round(newValue);

            }
            return LUT;
        }
        #endregion
    }
}