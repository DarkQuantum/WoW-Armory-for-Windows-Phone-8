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
		/// Gets or sets the key in which the character list is sorted by.
		/// </summary>
		/// <value>
		/// The key in which the character list is sorted by.
		/// </value>
		public static CharacterListSortBy CharacterListSortBy
		{
			get
			{
				return IsolatedStorageManager.GetValue("Setting_CharacterListSortBy", CharacterListSortBy.Name);
			}
			set
			{
				IsolatedStorageManager.SetValue("Setting_CharacterListSortBy", value);
			}
		}

		/// <summary>
		/// Gets or sets the type the character list is sorted by.
		/// </summary>
		/// <value>
		/// The type the character list is sorted by.
		/// </value>
		public static SortBy CharacterListSortByType
		{
			get
			{
				return IsolatedStorageManager.GetValue("Setting_CharacterListSortByType", SortBy.Ascending);
			}
			set
			{
				IsolatedStorageManager.SetValue("Setting_CharacterListSortByType", value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating if the character background should be shown.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the character background should be shown; otherwise, <c>false</c>.
		/// </value>
		public static bool ShowCharacterBackground
		{
			get
			{
				return IsolatedStorageManager.GetValue("Setting_ShowCharacterBackground", false);
			}
			set
			{
				IsolatedStorageManager.SetValue("Setting_ShowCharacterBackground", value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the gem name should be shown.
		/// </summary>
		/// <value>
		///   <c>true</c> if the gem name should be shown; otherwise, <c>false</c>.
		/// </value>
		public static bool ShowGemName
		{
			get
			{
				return IsolatedStorageManager.GetValue("Setting_ShowGemName", true);
			}
			set
			{
				IsolatedStorageManager.SetValue("Setting_ShowGemName", value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the 3D item viewer is enabled.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the 3D item viewer is enabled; otherwise, <c>false</c>.
		/// </value>
		public static bool Is3DItemViewerEnabled
		{
			get
			{
				return IsolatedStorageManager.GetValue("Setting_Is3DItemViewerEnabled", true);
			}
			set
			{
				IsolatedStorageManager.SetValue("Setting_Is3DItemViewerEnabled", value);
			}
		}

		/// <summary>
		/// Gets or sets the key in which the guild list is sorted by.
		/// </summary>
		/// <value>
		/// The key in which the guild list is sorted by.
		/// </value>
		public static GuildListSortBy GuildListSortBy
		{
			get
			{
				return IsolatedStorageManager.GetValue("Setting_GuildListSortBy", GuildListSortBy.Name);
			}
			set
			{
				IsolatedStorageManager.SetValue("Setting_GuildListSortBy", value);
			}
		}

		/// <summary>
		/// Gets or sets the type the guild list is sorted by.
		/// </summary>
		/// <value>
		/// The type the guild list is sorted by.
		/// </value>
		public static SortBy GuildListSortByType
		{
			get
			{
				return IsolatedStorageManager.GetValue("Setting_GuildListSortByType", SortBy.Ascending);
			}
			set
			{
				IsolatedStorageManager.SetValue("Setting_GuildListSortByType", value);
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
