using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;

namespace WowArmory.Core.Managers
{
	public static class CacheManager
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private static readonly Dictionary<string, ImageSource> _imageCache = new Dictionary<string,ImageSource>();
		private static RealmList _realmList = null;
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
		/// Gets or sets the cached realm list.
		/// </summary>
		/// <value>
		/// The cached realm list.
		/// </value>
		public static RealmList RealmList
		{
			get { return _realmList; }
			set { _realmList = value; }
		}
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
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}