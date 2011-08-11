using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WowArmory.Core.Managers;

namespace WowArmory.Core.Helper
{
	public static class AsyncImageLoader
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		public static readonly DependencyProperty ImageUrlProperty = DependencyProperty.RegisterAttached("ImageUrl", typeof(string), typeof(AsyncImageLoader), new PropertyMetadata(ImageUrlPropertyChanged));
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the image URL.
		/// </summary>
		/// <param name="depObj">The dependency object.</param>
		/// <returns></returns>
		public static string GetImageUrl(DependencyObject depObj)
		{
			return (string)depObj.GetValue(ImageUrlProperty);
		}

		/// <summary>
		/// Sets the image URL.
		/// </summary>
		/// <param name="depObj">The dependency object.</param>
		/// <param name="imageUrl">The image URL.</param>
		public static void SetImageUrl(DependencyObject depObj, string imageUrl)
		{
			depObj.SetValue(ImageUrlProperty, imageUrl);
		}

		/// <summary>
		/// Called when the image url property is changed.
		/// </summary>
		/// <param name="depObj">The dependency object.</param>
		/// <param name="eventArgs">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void ImageUrlPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs eventArgs)
		{
			if (depObj == null) return;
			if (eventArgs.NewValue == null) return;

			var image = (Image)depObj;
			var imageUri = new Uri((string)eventArgs.NewValue);

			if (String.IsNullOrEmpty(Path.GetFileNameWithoutExtension(imageUri.LocalPath))) return;

			if (!CacheManager.ImageCache.ContainsKey(imageUri.ToString()))
			{
				var webClient = new WebClient();
				webClient.OpenReadCompleted += delegate(object sender, OpenReadCompletedEventArgs e)
				{
				    var webImage = new BitmapImage();
				    webImage.SetSource(e.Result);
				    image.Source = CacheManager.GetImageSourceFromCache(imageUri.ToString(), webImage);
				};
				webClient.OpenReadAsync(imageUri);
			}
			else
			{
				image.Source = CacheManager.ImageCache[imageUri.ToString()];
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
