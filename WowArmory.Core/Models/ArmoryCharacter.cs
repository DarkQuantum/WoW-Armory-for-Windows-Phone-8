using System;
using System.Runtime.Serialization;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Pages;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ArmoryCharacter
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public CharacterSheetPage CharacterSheetPage { get; set; }
		[DataMember]
		public CharacterReputationPage CharacterReputationPage { get; set; }
		[DataMember]
		public CharacterTalentsPage CharacterTalentsPage { get; set; }
		[DataMember]
		public CharacterActivityFeedPage CharacterActivityFeedPage { get; set; }
		[DataMember]
		public Region Region { get; set; }
		[DataMember]
		public string Locale { get; set; }
		[DataMember]
		public bool IsBookmarked { get; set; }
		[DataMember]
		public bool IsDirty { get; set; }
		[DataMember]
		public DateTime LastUpdate { get; set; }

		public bool IsValid
		{
			get
			{
				if ( CharacterSheetPage == null ) return false;
				if ( !CharacterSheetPage.IsValid ) return false;
				if ( CharacterReputationPage == null) return false;
				if ( CharacterTalentsPage == null ) return false;
				if ( CharacterActivityFeedPage == null ) return false;

				return true;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ArmoryCharacter"/> class.
		/// </summary>
		public ArmoryCharacter()
		{
			IsBookmarked = false;
			IsDirty = false;
			LastUpdate = DateTime.MinValue;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
