using System;
using System.Globalization;
using System.Windows.Data;

namespace WowArmory.Core.Converters
{
	public class StringToUppercaseConverter : IValueConverter
	{
		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Converts the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="targetType">Type of the target.</param>
		/// <param name="parameter">The parameter.</param>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null)
			{
				var text = value.ToString();
				if (!String.IsNullOrEmpty(text))
				{
					return text.ToUpper();
				}
			}

			return String.Empty;
		}

		/// <summary>
		/// Converts the back.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="targetType">Type of the target.</param>
		/// <param name="parameter">The parameter.</param>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
