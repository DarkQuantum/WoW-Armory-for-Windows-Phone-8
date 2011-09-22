using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Helper;

namespace WowArmory.Core.Managers
{
	public static class CacheManager
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private static readonly Dictionary<string, ImageSource> _imageCache = new Dictionary<string,ImageSource>();
		private static Dictionary<Region, RealmList> _cachedRealmLists = new Dictionary<Region,RealmList>();
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the current image cache.
		/// </summary>
		public static Dictionary<string, ImageSource> ImageCache
		{
			get { return _imageCache; }
		}

		/// <summary>
		/// Gets or sets the cached realm lists.
		/// </summary>
		/// <value>
		/// The cached realm lists.
		/// </value>
		public static Dictionary<Region, RealmList> CachedRealmLists
		{
			get
			{
				return _cachedRealmLists;
			}
			set
			{
				_cachedRealmLists = value;
			}
		}

		/// <summary>
		/// Gets or sets the cached guild perks.
		/// </summary>
		/// <value>
		/// The cached guild perks.
		/// </value>
		public static GuildPerks CachedGuildPerks { get; set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Returns an image source with the specified key from the cache.
		/// If it doesn't exist yet it will be fetched and cached for future access.
		/// </summary>
		/// <param name="key">The key for the image.</param>
		/// <returns>
		/// The image source for the specified key.
		/// </returns>
		public static ImageSource GetImageSourceFromCache(string key)
		{
			var uriKind = UriKind.Absolute;
			if (key.StartsWith("/")) uriKind = UriKind.Relative;
			return GetImageSourceFromCache(key, uriKind);
		}

		/// <summary>
		/// Returns an image source with the specified key from the cache.
		/// If it doesn't exist yet it will be fetched and cached for future access.
		/// </summary>
		/// <param name="key">The key for the image.</param>
		/// <param name="uriKind">The uri kind.</param>
		/// <returns>
		/// The image source for the specified key.
		/// </returns>
		public static ImageSource GetImageSourceFromCache(string key, UriKind uriKind)
		{
			if (String.IsNullOrEmpty(key)) return null;

			if (!ImageCache.ContainsKey(key))
			{
				var imageSource = new BitmapImage(new Uri(key, uriKind));
				ImageCache.Add(key, imageSource);
			}

			return ImageCache[key];
		}

		/// <summary>
		/// Returns an image source with the specified key from the cache.
		/// If it doesn't exist yet it will cache the specified image source for future access.
		/// </summary>
		/// <param name="key">The key for the image.</param>
		/// <param name="imageSource">The image source to cache if the key is not yet cached.</param>
		/// <returns>
		/// The image source for the specified key.
		/// </returns>
		public static ImageSource GetImageSourceFromCache(string key, ImageSource imageSource)
		{
			var uriKind = UriKind.Absolute;
			if (key.StartsWith("/")) uriKind = UriKind.Relative;
			return GetImageSourceFromCache(key, imageSource, uriKind);
		}

		/// <summary>
		/// Returns an image source with the specified key from the cache.
		/// If it doesn't exist yet it will cache the specified image source for future access.
		/// </summary>
		/// <param name="key">The key for the image.</param>
		/// <param name="imageSource">The image source to cache if the key is not yet cached.</param>
		/// <param name="uriKind">The uri kind.</param>
		/// <returns>
		/// The image source for the specified key.
		/// </returns>
		public static ImageSource GetImageSourceFromCache(string key, ImageSource imageSource, UriKind uriKind)
		{
			if (String.IsNullOrEmpty(key)) return null;

			if (!ImageCache.ContainsKey(key))
			{
				ImageCache.Add(key, imageSource);
			}

			return ImageCache[key];
		}

		/// <summary>
		/// Gets the guild emblem image.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="realm">The realm.</param>
		/// <param name="name">The name.</param>
		/// <param name="guildEmblem">The guild emblem.</param>
		/// <param name="guildSide">The guild side.</param>
		/// <returns></returns>
		public static ImageSource GetGuildEmblemImage(Region region, string realm, string name, GuildEmblem guildEmblem, GuildSide guildSide)
		{
			var key = String.Format("{0}_{1}_{2}", region.ToString().ToLower(), realm.Replace(" ", "-").ToLower().Trim(), name.Replace(" ", "-").ToLower().Trim());

			if (!ImageCache.ContainsKey(key))
			{
				var guildEmblemImage = ImageHelper.GenerateGuildEmblemImage(guildEmblem, guildSide);
				ImageCache.Add(key, guildEmblemImage);
			}

			return ImageCache[key];
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}