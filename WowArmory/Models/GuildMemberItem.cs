using System;
using System.Windows.Media;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;

namespace WowArmory.Models
{
	public class GuildMemberItem : GuildMember
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the region.
		/// </summary>
		/// <value>
		/// The region.
		/// </value>
		public Region Region { get; set; }

		/// <summary>
		/// Gets the race image.
		/// </summary>
		public ImageSource RaceImage
		{
			get
			{
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/Icons/Races/{0}_{1}_Border.png", (int)Character.Race, (int)Character.Gender));
			}
		}

		/// <summary>
		/// Gets the class image.
		/// </summary>
		public ImageSource ClassImage
		{
			get
			{
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/Icons/Classes/{0}_Border.png", (int)Character.Class));
			}
		}

		/// <summary>
		/// Gets the class as an integer data type.
		/// </summary>
		public int ClassAsInt
		{
			get
			{
				return (int)Character.Class;
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
				var result = BattleNetClient.Current.GetThumbnailUrl(Character.Thumbnail);
				BattleNetClient.Current.Region = previousRegion;
				return result;
			}
		}

		/// <summary>
		/// Gets the rank string.
		/// </summary>
		public string RankString
		{
			get
			{
				return Rank != 0 ? String.Format(AppResources.UI_GuildDetails_Members_Rank, Rank) : AppResources.UI_GuildDetails_Members_GuildMaster;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this character is the guild master.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this character is the guild master; otherwise, <c>false</c>.
		/// </value>
		public bool IsGuildMaster
		{
			get
			{
				return Rank == 0;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="GuildMemberItem"/> class.
		/// </summary>
		public GuildMemberItem()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GuildMemberItem"/> class.
		/// </summary>
		/// <param name="guildMember">The guild member.</param>
		public GuildMemberItem(GuildMember guildMember)
		{
			base.Character = guildMember.Character;
			base.Rank = guildMember.Rank;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}