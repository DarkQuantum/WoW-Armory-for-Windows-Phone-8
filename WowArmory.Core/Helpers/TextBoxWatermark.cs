// 
// Copyright (c) 2010 Christian Krueger <christian.krueger@krueger-c.com>
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge and for non-commercial usage
// only, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify,
// merge, publish, and/or distribute copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

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
