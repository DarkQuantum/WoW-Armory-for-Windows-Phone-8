using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;

namespace WowArmory.Core.Converters
{
	public class RaceToFactionConverter : IValueConverter
	{
		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Modifies the source data before passing it to the target for display in the UI.
		/// </summary>
		/// <param name="value">The source data being passed to the target.</param>
		/// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
		/// <param name="parameter">An optional parameter to be used in the converter logic.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>
		/// The value to be passed to the target dependency property.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((value is int))
			{
				var race = (CharacterRace)(int)value;
				var faction = BattleNetClient.Current.FactionRaces.Where(f => f.Value.Contains(race)).Select(f => (int)f.Key).FirstOrDefault();
				return faction;
			}

			return -1;
		}

		/// <summary>
		/// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
		/// </summary>
		/// <param name="value">The target data being passed to the source.</param>
		/// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
		/// <param name="parameter">An optional parameter to be used in the converter logic.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>
		/// The value to be passed to the source object.
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
