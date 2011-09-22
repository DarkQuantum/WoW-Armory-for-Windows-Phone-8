using System;
using System.Windows.Media;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.Core.Storage;

namespace WowArmory.Models
{
	public class GuildListItem : GuildStorageData
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the name of the region string.
		/// </summary>
		public string RegionString
		{
			get
			{
				return AppResources.ResourceManager.GetString(String.Format("BattleNet_Region_{0}", Region));
			}
		}

		/// <summary>
		/// Gets the member count string.
		/// </summary>
		public string MemberCountString
		{
			get
			{
				return String.Format(AppResources.UI_GuildDetails_Header_Members, Members != null ? Members.Count : 0);
			}
		}

		/// <summary>
		/// Gets the level string.
		/// </summary>
		public string LevelString
		{
			get
			{
				return String.Format(AppResources.UI_GuildDetails_Header_Level, Level);
			}
		}

		/// <summary>
		/// Gets the faction string.
		/// </summary>
		public string FactionString
		{
			get
			{
				return AppResources.ResourceManager.GetString(String.Format("UI_GuildDetails_Header_Faction_{0}", Side));
			}
		}

		/// <summary>
		/// Gets the faction image.
		/// </summary>
		public ImageSource FactionImage
		{
			get
			{
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/Icons/Factions/{0}.png", (int)Side));
			}
		}

		/// <summary>
		/// Gets the emblem default image.
		/// </summary>
		public ImageSource EmblemDefaultImage
		{
			get
			{
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/GuildDetails/Emblem_Default_{0}.png", Side));
			}
		}

		/// <summary>
		/// Gets the guild emblem image.
		/// </summary>
		public ImageSource GuildEmblemImage
		{
			get
			{
				return CacheManager.GetGuildEmblemImage(Region, Realm, Name, Emblem, Side);
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="GuildListItem"/> class.
		/// </summary>
		public GuildListItem()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GuildListItem"/> class.
		/// </summary>
		/// <param name="storageData">The storage data.</param>
		public GuildListItem(GuildStorageData storageData)
		{
			base.AchievementPoints = storageData.AchievementPoints;
			base.Guid = storageData.Guid;
			base.Level = storageData.Level;
			base.Members = storageData.Members;
			base.Name = storageData.Name;
			base.Realm = storageData.Realm;
			base.Region = storageData.Region;
			base.Side = storageData.Side;
			base.Emblem = storageData.Emblem;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}