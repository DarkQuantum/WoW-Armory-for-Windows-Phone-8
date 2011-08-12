using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WowArmory.Controls
{
	public class CharacterItemContainer : Button
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		public static readonly DependencyProperty BackgroundImageSourceProperty = DependencyProperty.Register("BackgroundImageSource", typeof(ImageSource), typeof(CharacterItemContainer), new PropertyMetadata(null));
		/// <summary>
		/// Gets or sets the background image source.
		/// </summary>
		/// <value>
		/// The background image source.
		/// </value>
		public ImageSource BackgroundImageSource
		{
			get
			{
				return (ImageSource)GetValue(BackgroundImageSourceProperty);
			}
			set
			{
				SetValue(BackgroundImageSourceProperty, value);
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterItemContainer"/> class.
		/// </summary>
		public CharacterItemContainer()
		{
			DefaultStyleKey = typeof(CharacterItemContainer);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Builds the visual tree for the <see cref="T:System.Windows.Controls.Button"/> when a new template is applied.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
