// 
// Copyright (c) 2010 Christian Krueger <christian.krueger@krueger-c.com>
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge and for non-commercial usage
// only, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify,
// merge, publish, and/or distribute copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using WowArmory.Core.Helpers;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Storage;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class Character
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int AchievementPoints { get; set; }
		[DataMember]
		public string BattleGroup { get; set; }
		[DataMember]
		public string CharacterUrl { get; set; }
		[DataMember]
		public CharacterClass Class { get; set; }
		[DataMember]
		public CharacterFaction Faction { get; set; }
		[DataMember]
		public CharacterGender Gender { get; set; }
		[DataMember]
		public string GuildName { get; set; }
		[DataMember]
		public string GuildUrl { get; set; }
		[DataMember]
		public DateTime LastModified { get; set; }
		[DataMember]
		public int Level { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public CharacterRace Race { get; set; }
		[DataMember]
		public string Realm { get; set; }
		[DataMember]
		public Region Region { get; set; }
		[DataMember]
		public int TitleId { get; set; }
		[DataMember]
		public string TitlePrefix { get; set; }
		[DataMember]
		public string TitleSuffix { get; set; }
		
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

		public ImageSource DetailsBackgroundImage
		{
			get
			{
				var key = String.Format( "/WowArmory.Core;component/Images/Backgrounds/CharacterDetails/{0}.png", (int)Race );
				return StorageManager.GetImageSourceFromCache( key, UriKind.Relative );
			}
		}

		public string PortraitUrl
		{
			get
			{
				string levelString;
				if ( Level < 60 ) levelString = "wow-default";
				else if ( Level < 70 ) levelString = "wow";
				else if ( Level < 80 ) levelString = "wow-70";
				else levelString = "wow-80";
				return String.Format( "{0}_images/portraits/{1}/{2}-{3}-{4}.gif", Armory.Current.GetArmoryUriByRegion( Region ), levelString, (int)Gender, (int)Race, (int)Class );
			}
		}

		public SolidColorBrush NameColorBrush
		{
			get
			{
				SolidColorBrush result = new SolidColorBrush( (Color)Application.Current.Resources[ "PhoneForegroundColor" ] );
				
				//switch ( Class )
				//{
				//    case CharacterClass.Warrior: result = new SolidColorBrush( Tools.ColorFromHex( "#ff977b4d" ) ); break;
				//    case CharacterClass.Paladin: result = new SolidColorBrush( Tools.ColorFromHex( "#fff48cba" ) ); break;
				//    case CharacterClass.Hunter: result = new SolidColorBrush( Tools.ColorFromHex( "#ffaad373" ) ); break;
				//    case CharacterClass.Rogue: result = new SolidColorBrush( Tools.ColorFromHex( "#fffff468" ) ); break;
				//    case CharacterClass.Priest: result = new SolidColorBrush( Tools.ColorFromHex( "#ffffffff" ) ); break;
				//    case CharacterClass.DeathKnight: result = new SolidColorBrush( Tools.ColorFromHex( "#ffc51e3a" ) ); break;
				//    case CharacterClass.Shaman: result = new SolidColorBrush( Tools.ColorFromHex( "#ff2359ff" ) ); break;
				//    case CharacterClass.Mage: result = new SolidColorBrush( Tools.ColorFromHex( "#ff68ccef" ) ); break;
				//    case CharacterClass.Warlock: result = new SolidColorBrush( Tools.ColorFromHex( "#ff8962d4" ) ); break;
				//    default: result = new SolidColorBrush( Tools.ColorFromHex( "#ffff7c0b" ) ); break;
				//}

				return result;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public Character()
		{
		}
		
		public Character( XElement root, Region region )
		{
			AchievementPoints = (int)root.GetAttributeValue( "points", ConvertToType.Int );
			BattleGroup = root.GetAttributeValue( "battleGroup" );
			CharacterUrl = root.GetAttributeValue( "charUrl" );
			Class = (CharacterClass)root.GetAttributeValue( "classId", ConvertToType.Int );
			Faction = (CharacterFaction)root.GetAttributeValue( "factionId", ConvertToType.Int );
			Gender = (CharacterGender)root.GetAttributeValue( "genderId", ConvertToType.Int );
			GuildName = root.GetAttributeValue( "guildName" );
			GuildUrl = root.GetAttributeValue( "guildUrl" );
			Level = (int)root.GetAttributeValue( "level", ConvertToType.Int );
			Name = root.GetAttributeValue( "name" );
			Realm = root.GetAttributeValue( "realm" );
			Region = Armory.Current.Region;
			TitleId = (int)root.GetAttributeValue( "titleId", ConvertToType.Int );
			TitlePrefix = root.GetAttributeValue( "prefix" );
			TitleSuffix = root.GetAttributeValue( "suffix" );
			Race = (CharacterRace)root.GetAttributeValue( "raceId", ConvertToType.Int );
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
