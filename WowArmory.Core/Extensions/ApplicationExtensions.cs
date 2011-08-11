using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Shell;

namespace WowArmory.Core.Extensions
{
	public static class ApplicationExtensions
	{
		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Retrieves the current application phone state.
		/// </summary>
		/// <param name="app">The application to retrieve the phone state from.</param>
		/// <returns>
		/// The current application phone state.
		/// </returns>
		public static Dictionary<string, object> RetrieveFromPhoneState(this Application app)
		{
			var state = new Dictionary<string, object>();

			if (PhoneApplicationService.Current.State != null)
			{
				state = (Dictionary<string, object>)PhoneApplicationService.Current.State;
			}

			return state;
		}

		/// <summary>
		/// Saves the specified key and value to the current application phone state.
		/// </summary>
		/// <param name="app">The application for which the key and value will be saved.</param>
		/// <param name="key">The key to add to the current phone state.</param>
		/// <param name="value">The value to add to the current phone state.</param>
		public static void SaveToPhoneState(this Application app, string key, object value)
		{
			if (PhoneApplicationService.Current.State == null) return;

			if (PhoneApplicationService.Current.State.ContainsKey(key))
			{
				PhoneApplicationService.Current.State.Remove(key);
			}

			PhoneApplicationService.Current.State.Add(key, value);
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}