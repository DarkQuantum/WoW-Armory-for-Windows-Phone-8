using System;
using System.IO.IsolatedStorage;
using WowArmory.Core.BattleNet;

namespace WowArmory.Core.Managers
{
	public static class IsolatedStorageManager
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the region used by the <see cref="BattleNetClient"/> object.
		/// </summary>
		/// <value>
		/// The region used by the <see cref="BattleNetClient"/> object.
		/// </value>
		public static Region Region
		{
			get
			{
				return GetValue("Setting_Region", Region.Europe);
			}
			set
			{
				SetValue("Setting_Region", value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the application is used for the first time.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the application is used for the first time; otherwise, <c>false</c>.
		/// </value>
		public static bool IsFirstTimeUsage
		{
			get
			{
				return GetValue("Setting_IsFirstTimeUsage", true);
			}
			set
			{
				SetValue("Setting_IsFirstTimeUsage", value);
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the value of the application setting for the specified key from the isolated storage settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The application setting key.</param>
		/// <param name="defaultValue">The default value to use.</param>
		/// <returns>
		/// The value of the application setting for the specified key from the isolated storage settings.
		/// </returns>
		public static T GetValue<T>(string key, T defaultValue)
		{
			try
			{
				T value;
				if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out value))
				{
					value = defaultValue;
					SetValue(key, value);
					Save();
				}

				return value;
			}
			catch
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// Sets the value of the application setting for the specified key in the isolated storage settings.
		/// </summary>
		/// <param name="key">The application setting key.</param>
		/// <param name="value">The application setting value to set.</param>
		public static void SetValue(string key, object value)
		{
			if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
			{
				IsolatedStorageSettings.ApplicationSettings[key] = value;
			}
			else
			{
				IsolatedStorageSettings.ApplicationSettings.Add(key, value);
			}
		}

		/// <summary>
		/// Saves the current state of the isolated storage settings.
		/// </summary>
		public static void Save()
		{
			IsolatedStorageSettings.ApplicationSettings.Save();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}