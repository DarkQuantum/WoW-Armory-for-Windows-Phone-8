using WowArmory.Core.BattleNet;
using WowArmory.Core.Enumerations;

namespace WowArmory.Core.Managers
{
	public static class AppSettingsManager
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
				return IsolatedStorageManager.GetValue("Setting_Region", Region.Europe);
			}
			set
			{
				IsolatedStorageManager.SetValue("Setting_Region", value);
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
				return IsolatedStorageManager.GetValue("Setting_IsFirstTimeUsage", true);
			}
			set
			{
				IsolatedStorageManager.SetValue("Setting_IsFirstTimeUsage", value);
			}
		}

		/// <summary>
		/// Gets or sets the key in which the character list is ordered by.
		/// </summary>
		/// <value>
		/// The key in which the character list is ordered by.
		/// </value>
		public static CharacterListOrderBy CharacterListOrderBy
		{
			get
			{
				return IsolatedStorageManager.GetValue("Setting_CharacterListOrderBy", CharacterListOrderBy.Name);
			}
			set
			{
				IsolatedStorageManager.SetValue("Setting_CharacterListOrderBy", value);
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
