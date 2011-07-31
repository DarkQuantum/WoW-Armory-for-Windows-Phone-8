using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageTools;
using ImageTools.IO.Gif;
using WowArmory.Core.Storage;

namespace WowArmory.Core.Helpers
{
	public static class AsyncImageLoader
	{
		//---------------------------------------------------------------------------
		#region --- Dependency Properties ---
		//---------------------------------------------------------------------------
		public static readonly DependencyProperty ImageUrlProperty = DependencyProperty.RegisterAttached( "ImageUrl", typeof( string ), typeof( AsyncImageLoader ), new PropertyMetadata( ImageUrlPropertyChanged ) );
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------

		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public static string GetImageUrl( DependencyObject depObj )
		{
			return (string)depObj.GetValue( ImageUrlProperty );
		}

		public static void SetImageUrl( DependencyObject depObj, string imageUrl )
		{
			depObj.SetValue( ImageUrlProperty, imageUrl );
		}

		private static void ImageUrlPropertyChanged( DependencyObject depObj, DependencyPropertyChangedEventArgs eventArgs )
		{
			if ( depObj == null ) return;
			if ( eventArgs.NewValue == null ) return;

			var image = (Image)depObj;
			var imageUri = new Uri( (string)eventArgs.NewValue );

			if ( !String.IsNullOrEmpty( Path.GetFileNameWithoutExtension( imageUri.LocalPath ) ) )
			{
				if ( !StorageManager.ImageCache.ContainsKey( imageUri.ToString() ) )
				{
					var webClient = new WebClient();
					webClient.OpenReadCompleted += delegate( object sender, OpenReadCompletedEventArgs e )
					{
						ProcessImage( sender, e, image, imageUri );
					};
					webClient.OpenReadAsync( imageUri );
				}
				else
				{
					image.Source = StorageManager.ImageCache[ imageUri.ToString() ];
				}
			}
		}

		private static void ProcessImage( object sender, OpenReadCompletedEventArgs e, Image image, Uri imageUri )
		{
			try
			{
				if ( imageUri.ToString().EndsWith( ".gif", StringComparison.CurrentCultureIgnoreCase ) )
				{
					var b = new byte[ e.Result.Length ];
					e.Result.Read( b, 0, b.Length );
					if ( b[ 0 ] == 71 && b[ 1 ] == 73 && b[ 2 ] == 70 )
					{
						var gifImage = new ExtendedImage();
						var gifDecoder = new GifDecoder();
						gifDecoder.Decode( gifImage, new MemoryStream( b ) );
						var imageSource = gifImage.ToBitmap();
						if ( imageSource.PixelWidth > 0 && imageSource.PixelHeight > 0 )
						{
							image.Source = StorageManager.GetImageSourceFromCache( imageUri.ToString(), imageSource );
						}
					}
					else
					{
						e.Result.Close();
					}
				}
				else
				{
					var webImage = new BitmapImage();
					webImage.SetSource( e.Result );
					image.Source = StorageManager.GetImageSourceFromCache( imageUri.ToString(), webImage );
				}
			}
			catch ( Exception ex )
			{
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
