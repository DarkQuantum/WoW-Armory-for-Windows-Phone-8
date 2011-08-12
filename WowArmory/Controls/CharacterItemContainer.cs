using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WowArmory.Core.BattleNet.Models;

namespace WowArmory.Controls
{
	public class CharacterItemContainer : Control
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

		public static readonly DependencyProperty SelectionVisibilityProperty = DependencyProperty.Register("SelectionVisibility", typeof(Visibility), typeof(CharacterItemContainer), new PropertyMetadata(Visibility.Collapsed));
		/// <summary>
		/// Gets or sets the selection box visibility.
		/// </summary>
		/// <value>
		/// The selection box visibility.
		/// </value>
		public Visibility SelectionVisibility
		{
			get
			{
				return (Visibility)GetValue(SelectionVisibilityProperty);
			}
			set
			{
				SetValue(SelectionVisibilityProperty, value);
			}
		}

		public static readonly DependencyProperty ItemSlotProperty = DependencyProperty.Register("ItemSlot", typeof(ItemSlot), typeof(CharacterItemContainer), null);
		/// <summary>
		/// Gets or sets the item slot.
		/// </summary>
		/// <value>
		/// The item slot.
		/// </value>
		public ItemSlot ItemSlot
		{
			get
			{
				return (ItemSlot)GetValue(ItemSlotProperty);
			}
			set
			{
				SetValue(ItemSlotProperty, value);
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
