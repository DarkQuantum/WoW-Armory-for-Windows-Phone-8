using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WowArmory.Core.Converter
{
	public class StringNotNullOrEmptyToVisibilityConverter : IValueConverter
	{
		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			if ( value != null )
			{
				var text = value.ToString();

				if ( !String.IsNullOrEmpty( text ) )
				{
					return Visibility.Visible;
				}
			}

			return Visibility.Collapsed;
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotImplementedException();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
