using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class CharacterInfo
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public Character Character { get; set; }
		[DataMember]
		public CharacterTab CharacterTab { get; set; }
		[DataMember]
		public string ErrCode { get; set; }

		public bool IsValid
		{
			get
			{
				if ( !String.IsNullOrEmpty( ErrCode ) ) return false;
				if ( Character == null ) return false;
				if ( CharacterTab == null ) return false;

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
		/// Initializes a new instance of the <see cref="CharacterInfo"/> class.
		/// </summary>
		public CharacterInfo()
		{
		}
		
		public CharacterInfo( XElement root, Region region )
		{
			Character = root.Elements( "character" ).Select( element => new Character( element, region ) ).FirstOrDefault();
			CharacterTab = root.Elements( "characterTab" ).Select( element => new CharacterTab( element, region ) ).FirstOrDefault();

			ErrCode = root.GetAttributeValue( "errCode" );
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
