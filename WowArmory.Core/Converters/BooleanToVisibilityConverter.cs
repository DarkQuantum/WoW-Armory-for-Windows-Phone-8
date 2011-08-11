using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WowArmory.Core.Converters
{
	public class BooleanToVisibilityConverter : IValueConverter
	{
		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is Boolean)) return Visibility.Collapsed;
			return (bool)value ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}