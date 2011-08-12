using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WowArmory.Controls
{
	public class MainPageButton : Button
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(MainPageButton), null);
		public string Label
		{
			get { return (string)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}

		public static readonly DependencyProperty IconSourceProperty = DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(MainPageButton), null);
		public ImageSource IconSource
		{
			get { return (ImageSource)GetValue(IconSourceProperty); }
			set { SetValue(IconSourceProperty, value); }
		}

		public static readonly DependencyProperty BadgeCounterProperty = DependencyProperty.Register("BadgeCounter", typeof(string), typeof(MainPageButton), new PropertyMetadata("0"));
		public string BadgeCounter
		{
			get { return (string)GetValue(BadgeCounterProperty); }
			set { SetValue(BadgeCounterProperty, value); }
		}

		public static readonly DependencyProperty BadgeVisibilityProperty = DependencyProperty.Register("BadgeVisibility", typeof(Visibility), typeof(MainPageButton), new PropertyMetadata(Visibility.Collapsed));
		public Visibility BadgeVisibility
		{
			get { return (Visibility)GetValue(BadgeVisibilityProperty); }
			set { SetValue(BadgeVisibilityProperty, value); }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="MainPageButton"/> class.
		/// </summary>
		public MainPageButton()
		{
			DefaultStyleKey = typeof(MainPageButton);
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Builds the visual tree for the <see cref="T:System.Windows.Controls.Button"/> when a new template is applied.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}