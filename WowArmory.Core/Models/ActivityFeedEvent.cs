using System;
using System.Runtime.Serialization;
using System.Windows.Media;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Languages;
using WowArmory.Core.Storage;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ActivityFeedEvent
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public ActivityFeedEventType Type { get; set; }
		[DataMember]
		public string Date { get; set; }
		[DataMember]
		public string Time { get; set; }
		[DataMember]
		public string Timestamp { get; set; }
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public int RootCategoryId { get; set; }
		[DataMember]
		public string IconName { get; set; }
		[DataMember]
		public string AchievementTitle { get; set; }
		[DataMember]
		public string AchievementDescription { get; set; }
		[DataMember]
		public string CriteriaDescription { get; set; }
		[DataMember]
		public int Count { get; set; }
		[DataMember]
		public string Sort { get; set; }
		[DataMember]
		public int Points { get; set; }
		[DataMember]
		public bool FeatOfStrength { get; set; }
		[DataMember]
		public int ItemLevel { get; set; }
		[DataMember]
		public int ItemQuality { get; set; }
		[DataMember]
		public string ItemName { get; set; }
		[DataMember]
		public int ItemSlot { get; set; }
		[DataMember]
		public Region Region { get; set; }

		public ImageSource IconImage
		{
			get
			{
				if ( !String.IsNullOrEmpty( IconName ) )
				{
					return StorageManager.GetImageSourceFromCache( String.Format( "{0}wow-icons/_images/64x64/{1}.jpg", Armory.Current.GetArmoryUriByRegion( Region ), IconName ) );
				}

				return StorageManager.GetImageSourceFromCache( String.Format( "/WowArmory.Core;component/Images/feed_icon_{0}.png", Type.ToString().ToLower() ), UriKind.Relative );
			}
		}

		public string Text
		{
			get
			{
				switch ( Type )
				{
					case ActivityFeedEventType.Achievement:
						{
							if ( FeatOfStrength )
							{
								return String.Format( AppResources.UI_CharacterDetails_ActivityText_FeatOfStrength, AchievementTitle );
							}
							return String.Format( AppResources.UI_CharacterDetails_ActivityText_Achievement, AchievementTitle, Points );
						} break;
					case ActivityFeedEventType.Bosskill:
						{
							return String.Format( AppResources.UI_CharacterDetails_ActivityText_Bosskill, Count, AchievementTitle );
						} break;
					case ActivityFeedEventType.Criteria:
						{
							return String.Format( AppResources.UI_CharacterDetails_ActivityText_Criteria, CriteriaDescription, AchievementTitle );
						} break;
					case ActivityFeedEventType.Loot:
						{
							return String.Format( AppResources.UI_CharacterDetails_ActivityText_Loot, ItemName );
						} break;
				}

				return String.Empty;
			}
		}

		public string Description
		{
			get
			{
				switch ( Type )
				{
					case ActivityFeedEventType.Achievement:
					case ActivityFeedEventType.Criteria:
						{
							return AchievementDescription;
						}
				}

				return String.Empty;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityFeedEvent"/> class.
		/// </summary>
		public ActivityFeedEvent()
		{
		}

		public ActivityFeedEvent( XElement root )
		{
			var type = root.GetAttributeValue( "type" );
			if ( !String.IsNullOrEmpty( type ) )
			{
				switch ( type )
				{
					case "achievement":
						{
							Type = ActivityFeedEventType.Achievement;
						} break;
					case "bosskill":
						{
							Type = ActivityFeedEventType.Bosskill;
						} break;
					case "criteria":
						{
							Type = ActivityFeedEventType.Criteria;
						} break;
					case "loot":
						{
							Type = ActivityFeedEventType.Loot;
						} break;
				}
			}

			Date = root.GetAttributeValue( "date" );
			Time = root.GetAttributeValue( "time" );
			Timestamp = root.GetAttributeValue( "timestamp" );
			Id = (int)root.GetAttributeValue( "id", ConvertToType.Int );
			RootCategoryId = (int)root.GetAttributeValue( "rootCategoryId", ConvertToType.Int );
			IconName = root.GetAttributeValue( "icon" );
			AchievementTitle = root.GetAttributeValue( "achievementTitle" );
			AchievementDescription = root.GetAttributeValue( "achievementDesc" );
			CriteriaDescription = root.GetAttributeValue( "criteriaDesc" );
			Count = (int)root.GetAttributeValue( "count", ConvertToType.Int );
			Sort = root.GetAttributeValue( "sort" );
			Points = (int)root.GetAttributeValue( "points", ConvertToType.Int );

			var featOfStrength = (int)root.GetAttributeValue( "featOfStrength", ConvertToType.Int );
			FeatOfStrength = false;
			if ( featOfStrength == 1 )
			{
				FeatOfStrength = true;
			}

			ItemLevel = (int)root.GetAttributeValue( "itemLevel", ConvertToType.Int );
			ItemQuality = (int)root.GetAttributeValue( "itemQuality", ConvertToType.Int );
			ItemName = root.GetAttributeValue( "itemName" );
			ItemSlot = (int)root.GetAttributeValue( "slot", ConvertToType.Int );
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
