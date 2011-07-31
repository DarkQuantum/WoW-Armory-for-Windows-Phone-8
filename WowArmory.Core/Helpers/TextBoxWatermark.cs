using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace WowArmory.Core.Helpers
{
	public class TextBoxWatermark : Behavior<TextBox>
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private bool _hasWatermark;
		private Brush _foreground = new SolidColorBrush( Colors.LightGray );
		private Brush _textBoxForeground;
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public string Text { get; set; }
		public Brush Foreground { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		protected override void OnAttached()
		{
			_textBoxForeground = AssociatedObject.Foreground;

			base.OnAttached();

			if ( !String.IsNullOrEmpty( Text ) )
			{
				SetWatermarkText();
			}

			AssociatedObject.GotFocus += GotFocus;
			AssociatedObject.LostFocus += LostFocus;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.GotFocus -= GotFocus;
			AssociatedObject.LostFocus -= LostFocus;
		}

		private void SetWatermarkText()
		{
			AssociatedObject.Foreground = _foreground;
			AssociatedObject.Text = Text;
			_hasWatermark = true;
		}

		private void RemoveWatermarkText()
		{
			AssociatedObject.Foreground = _textBoxForeground;
			AssociatedObject.Text = String.Empty;
			_hasWatermark = false;
		}

		private void GotFocus( object sender, RoutedEventArgs e )
		{
			if ( _hasWatermark )
			{
				RemoveWatermarkText();
			}
		}

		private void LostFocus( object sender, RoutedEventArgs e )
		{
			if ( String.IsNullOrEmpty( AssociatedObject.Text ) )
			{
				SetWatermarkText();
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
