using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WowArmory.Core.BattleNet.Models;

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

		/// <summary>
		/// Generates the guild emblem image.
		/// </summary>
		/// <param name="guildEmblemData">The guild emblem data.</param>
		/// <param name="guildSide">The guild side.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns></returns>
		public static BitmapSource GenerateGuildEmblemImage(GuildEmblem guildEmblemData, GuildSide guildSide, int width = 216, int height = 240)
		{
			WriteableBitmap guildEmblemImage;

			try
			{
				guildEmblemImage = new WriteableBitmap(216, 240);

				var factionBackgroundImage = new WriteableBitmap(216, 216).FromResource(String.Format("Images/GuildDetails/Emblem_Background_{0}.png", guildSide));
				guildEmblemImage.Blit(new Rect(0, 0, 216, 216), factionBackgroundImage, new Rect(0, 0, 216, 216), WriteableBitmapExtensions.BlendMode.Alpha);

				var shadowImage = new WriteableBitmap(179, 210).FromResource("Images/GuildDetails/Emblem_Shadow.png");
				guildEmblemImage.Blit(new Rect(18, 27, 179, 210), shadowImage, new Rect(0, 0, 179, 210), WriteableBitmapExtensions.BlendMode.Alpha);

				var backgroundColor = GetColorFromHexString(guildEmblemData.BackgroundColor);
				var emblemBackgroundImage = new WriteableBitmap(179, 210).FromResource("Images/GuildDetails/Emblem_Background.png");
				guildEmblemImage.Blit(new Rect(18, 27, 179, 210), emblemBackgroundImage, new Rect(0, 0, 179, 210), backgroundColor, WriteableBitmapExtensions.BlendMode.Alpha);

				var borderColor = GetColorFromHexString(guildEmblemData.BorderColor);
				var borderImage = new WriteableBitmap(147, 159).FromResource(String.Format("Images/GuildDetails/Emblem_Border_{0}.png", guildEmblemData.Border));
				guildEmblemImage.Blit(new Rect(31, 40, 147, 159), borderImage, new Rect(0, 0, 147, 159), borderColor, WriteableBitmapExtensions.BlendMode.Alpha);

				var iconColor = GetColorFromHexString(guildEmblemData.IconColor);
				var iconImage = new WriteableBitmap(125, 125).FromResource(String.Format("Images/GuildDetails/Emblems/Emblem_{0}.png", guildEmblemData.Icon.ToString().PadLeft(3, '0')));
				guildEmblemImage.Blit(new Rect(33, 57, 125, 125), iconImage, new Rect(0, 0, 125, 125), iconColor, WriteableBitmapExtensions.BlendMode.Alpha);

				var hooksImage = new WriteableBitmap(179, 32).FromResource("Images/GuildDetails/Emblem_Hooks.png");
				guildEmblemImage.Blit(new Rect(18, 27, 179, 32), hooksImage, new Rect(0, 0, 179, 32), WriteableBitmapExtensions.BlendMode.Alpha);
			}
			catch (Exception ex)
			{
				guildEmblemImage = new WriteableBitmap(216, 240).FromResource(String.Format("Images/GuildDetails/Emblem_Default_{0}.png", guildSide));
			}

			var resized = guildEmblemImage.Resize(width, height, WriteableBitmapExtensions.Interpolation.Bilinear);
			return resized;
		}

		/// <summary>
		/// Gets the color from specified hex string.
		/// </summary>
		/// <param name="hex">The hex color string.</param>
		/// <returns></returns>
		public static Color GetColorFromHexString(string hex)
		{
			var alpha = 0;
			var red = 0;
			var green = 0;
			var blue = 0;

			try
			{
				hex = hex.Replace("#", "");
				switch (hex.Length)
				{
					case 3:
						{
							hex = String.Format("FF{0}{0}{1}{1}{2}{2}", hex[0], hex[1], hex[2]);
						} break;
					case 4:
						{
							hex = String.Format("{0}{0}{1}{1}{2}{2}{3}{3}", hex[0], hex[1], hex[2], hex[3]);
						} break;
					case 6:
						{
							hex = String.Format("FF{0}", hex);
						} break;
				}

				alpha = Int32.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
				red = Int32.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
				green = Int32.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
				blue = Int32.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
			}
			catch (Exception ex)
			{
				alpha = 255;
				red = 255;
				green = 255;
				blue = 255;
			}

			return GetColor(alpha, red, green, blue);
		}

		/// <summary>
		/// Gets the color for the specified alpha, red, green and blue values.
		/// </summary>
		/// <param name="alpha">The alpha value of the color.</param>
		/// <param name="red">The red value of the color.</param>
		/// <param name="green">The green value of the color.</param>
		/// <param name="blue">The blue value of the color.</param>
		/// <returns></returns>
		public static Color GetColor(int alpha, int red, int green, int blue)
		{
			return Color.FromArgb((byte)alpha, (byte)red, (byte)green, (byte)blue);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}