using System.IO.IsolatedStorage;

namespace WowArmory.Core.Managers
{
	public static class IsolatedStorageManager
	{
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