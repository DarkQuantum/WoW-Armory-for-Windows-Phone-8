using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WowArmory.Core.Converter
{
	public class IntToVisibilityConverter : IValueConverter
	{
		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			if ( parameter != null )
			{
				var intString = parameter.ToString();
				var valueString = value.ToString();

				if ( !String.IsNullOrEmpty( intString ) && !String.IsNullOrEmpty( valueString ) )
				{
					if ( intString.Equals( valueString ) )
					{
						return Visibility.Visible;
					}
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
