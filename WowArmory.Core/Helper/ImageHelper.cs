using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WowArmory.Core.Helper
{
	public static class ImageHelper
	{
		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Converts the specified image to grayscale.
		/// </summary>
		/// <param name="image">The image to convert.</param>
		/// <returns></returns>
		public static WriteableBitmap ToGrayscale(BitmapImage image)
		{
			var bitmap = new WriteableBitmap(image);

			for (var y = 0; y < bitmap.PixelHeight; y++)
			{
				for (var x = 0; x < bitmap.PixelWidth; x++)
				{
					var pixelLocation = bitmap.PixelWidth * y + x;
					var pixel = bitmap.Pixels[pixelLocation];
					var pixelBytes = BitConverter.GetBytes(pixel);
					var grayPixel = (byte)(0.3 * pixelBytes[2] + 0.59 * pixelBytes[1] + 0.11 * pixelBytes[0]);
					pixelBytes[0] = pixelBytes[1] = pixelBytes[2] = grayPixel;
					bitmap.Pixels[pixelLocation] = BitConverter.ToInt32(pixelBytes, 0);
				}
			}

			return bitmap;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}