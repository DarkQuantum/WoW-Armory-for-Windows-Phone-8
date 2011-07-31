using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Media;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Storage;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class SearchResultCharacter
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public string BattleGroup { get; set; }
		[DataMember]
		public CharacterClass Class { get; set; }
		[DataMember]
		public CharacterFaction Faction { get; set; }
		[DataMember]
		public CharacterGender Gender { get; set; }
		[DataMember]
		public string GuildName { get; set; }
		[DataMember]
		public int Level { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public CharacterRace Race { get; set; }
		[DataMember]
		public string Realm { get; set; }
		[DataMember]
		public int Relevance { get; set; }
		[DataMember]
		public int SearchRank { get; set; }
		[DataMember]
		public string Url { get; set; }

		public ImageSource FactionImage
		{
			get
			{
				var key = String.Format( "/WowArmory.Core;component/Images/faction_{0}.png", (int)Faction );
				return StorageManager.GetImageSourceFromCache( key, UriKind.Relative );
			}
		}

		public ImageSource RaceImage
		{
			get
			{
				var key = String.Format( "/WowArmory.Core;component/Images/Characters/race_{0}_{1}.jpg", Race.ToString().Replace( " ", "" ).ToLower(), Gender.ToString().ToLower() );
				return StorageManager.GetImageSourceFromCache( key, UriKind.Relative );
			}
		}

		public ImageSource ClassImage
		{
			get
			{
				var key = String.Format( "/WowArmory.Core;component/Images/Characters/class_{0}.jpg", Class.ToString().Replace( " ", "" ).ToLower() );
				return StorageManager.GetImageSourceFromCache( key, UriKind.Relative );
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="SearchResultCharacter"/> class.
		/// </summary>
		public SearchResultCharacter()
		{
		}

		public SearchResultCharacter( XElement root )
		{
			BattleGroup = root.GetAttributeValue( "battleGroup" );
			Class = (CharacterClass)Int32.Parse( root.GetAttributeValue( "classId" ), CultureInfo.InvariantCulture );
			Faction = (CharacterFaction)Int32.Parse( root.GetAttributeValue( "factionId" ), CultureInfo.InvariantCulture );
			Gender = (CharacterGender)Int32.Parse( root.GetAttributeValue( "genderId" ), CultureInfo.InvariantCulture );
			GuildName = root.GetAttributeValue( "guild" );
			Level = Int32.Parse( root.GetAttributeValue( "level" ), CultureInfo.InvariantCulture );
			Name = root.GetAttributeValue( "name" );
			Race = (CharacterRace)Int32.Parse( root.GetAttributeValue( "raceId" ), CultureInfo.InvariantCulture );
			Realm = root.GetAttributeValue( "realm" );
			Relevance = Int32.Parse( root.GetAttributeValue( "relevance" ), CultureInfo.InvariantCulture );
			SearchRank = Int32.Parse( root.GetAttributeValue( "searchRank" ), CultureInfo.InvariantCulture );
			Url = root.GetAttributeValue( "url" );
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
