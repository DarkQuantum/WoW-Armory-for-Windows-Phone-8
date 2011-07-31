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
