using System;
using System.Windows.Media;
using WowArmory.Core.BattleNet;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.Core.Storage;

namespace WowArmory.Models
{
	public class CharacterListItem : CharacterStorageData
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the race image.
		/// </summary>
		public ImageSource RaceImage
		{
			get
			{
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/Icons/Races/{0}_{1}_Border.png", (int)Race, Gender));
			}
		}

		/// <summary>
		/// Gets the class image.
		/// </summary>
		public ImageSource ClassImage
		{
			get
			{
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/Icons/Classes/{0}_Border.png", (int)Class));
			}
		}

		/// <summary>
		/// Gets the badge image.
		/// </summary>
		public ImageSource BadgeImage
		{
			get
			{
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/Badge/Badge_Class_{0}.png", (int)Class));
			}
		}

		/// <summary>
		/// Gets the class as an integer data type.
		/// </summary>
		public int ClassAsInt
		{
			get
			{
				return (int)Class;
			}
		}

		/// <summary>
		/// Gets the faction as an integer data type.
		/// </summary>
		public int FactionAsInt
		{
			get
			{
				return (int)Faction;
			}
		}

		/// <summary>
		/// Gets the name of the faction.
		/// </summary>
		public string FactionName
		{
			get
			{
				return AppResources.ResourceManager.GetString(String.Format("UI_CharacterDetails_Faction_{0}", Faction));
			}
		}

		/// <summary>
		/// Gets the faction image.
		/// </summary>
		public ImageSource FactionImage
		{
			get
			{
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/Icons/Factions/{0}.png", (int)Faction));
			}
		}

		/// <summary>
		/// Gets the thumbnail image URL.
		/// </summary>
		public string ThumbnailImageUrl
		{
			get
			{
				var previousRegion = BattleNetClient.Current.Region;
				BattleNetClient.Current.Region = Region;
				var result = BattleNetClient.Current.GetThumbnailUrl(Thumbnail);
				BattleNetClient.Current.Region = previousRegion;
				return result;
			}
		}

		/// <summary>
		/// Gets the name of the region.
		/// </summary>
		public string RegionName
		{
			get
			{
				return AppResources.ResourceManager.GetString(String.Format("BattleNet_Region_{0}", Region));
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterListItem"/> class.
		/// </summary>
		public CharacterListItem()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterListItem"/> class.
		/// </summary>
		/// <param name="storageData">The storage data.</param>
		public CharacterListItem(CharacterStorageData storageData)
		{
			base.AchievementPoints = storageData.AchievementPoints;
			base.Character = storageData.Character;
			base.Class = storageData.Class;
			base.Faction = storageData.Faction;
			base.Gender = storageData.Gender;
			base.Guid = storageData.Guid;
			base.Guild = storageData.Guild;
			base.Level = storageData.Level;
			base.Race = storageData.Race;
			base.Realm = storageData.Realm;
			base.Region = storageData.Region;
			base.Thumbnail = storageData.Thumbnail;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}