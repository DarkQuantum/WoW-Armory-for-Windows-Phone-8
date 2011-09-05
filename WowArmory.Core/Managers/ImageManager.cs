using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WowArmory.Core.BattleNet.Models;

namespace WowArmory.Core.Managers
{
	public static class ImageManager
	{
		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Generates the guild emblem image.
		/// </summary>
		/// <param name="guildEmblemData">The guild emblem data.</param>
		/// <param name="guildSide">The guild side.</param>
		/// <returns></returns>
		public static BitmapSource GenerateGuildEmblemImage(GuildEmblem guildEmblemData, GuildSide guildSide)
		{
			var guildEmblemImage = new WriteableBitmap(216, 240);

			var backgroundImageUri = new Uri(String.Format("/WowArmory.Core;Component/Images/GuildDetails/Emblem_Background_{0}.png", guildSide), UriKind.Relative);
			var backgroundImageBitmap = new BitmapImage(backgroundImageUri);
			var backgroundImageWriteableBitmap = new WriteableBitmap(backgroundImageBitmap);

			guildEmblemImage.Blit(new Rect(0, 0, 216, 216), backgroundImageWriteableBitmap, new Rect(0, 0, 216, 216), WriteableBitmapExtensions.BlendMode.None);

			return guildEmblemImage;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}