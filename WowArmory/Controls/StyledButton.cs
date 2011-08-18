using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WowArmory.Controls
{
	public class StyledButton : Button
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(StyledButton), new PropertyMetadata(String.Empty));
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text
		{
			get
			{
				return (string)GetValue(TextProperty);
			}
			set
			{
				SetValue(TextProperty, value);
			}
		}

		public static readonly DependencyProperty ExternalLinkIconVisibilityProperty = DependencyProperty.Register("ExternalLinkIconVisibility", typeof(Visibility), typeof(StyledButton), new PropertyMetadata(Visibility.Collapsed));
		/// <summary>
		/// Gets or sets the external link icon visibility.
		/// </summary>
		/// <value>
		/// The external link icon visibility.
		/// </value>
		public Visibility ExternalLinkIconVisibility
		{
			get
			{
				return (Visibility)GetValue(ExternalLinkIconVisibilityProperty);
			}
			set
			{
				SetValue(ExternalLinkIconVisibilityProperty, value);
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="StyledButton"/> class.
		/// </summary>
		public StyledButton()
		{
			DefaultStyleKey = typeof(StyledButton);
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