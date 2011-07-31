using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Models;

namespace WowArmory.Core.Pages
{
	[DataContract]
	public class CharacterSheetPage : ArmoryPage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public CharacterInfo CharacterInfo { get; set; }

		public bool IsValid
		{
			get
			{
				if ( Document == null ) return false;
				if ( CharacterInfo == null ) return false;
				if ( !CharacterInfo.IsValid ) return false;

				return true;
			}
		}

		public string ErrCode
		{
			get
			{
				if ( CharacterInfo == null ) return String.Empty;
				return CharacterInfo.ErrCode;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public CharacterSheetPage()
		{
		}

		public CharacterSheetPage( ArmoryPage armoryPage )
			: base( armoryPage.Document )
		{
			FetchData();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public static CharacterSheetPage FromArmoryPage( ArmoryPage armoryPage )
		{
			if ( armoryPage == null ) return null;
			if ( armoryPage.Document == null ) return null;

			var derivedPage = armoryPage as CharacterSheetPage;
			if ( derivedPage == null )
			{
				derivedPage = new CharacterSheetPage( armoryPage );
			}
			return derivedPage;
		}

		private void FetchData()
		{
			CharacterInfo = Root.Elements( "characterInfo" ).Select( element => new CharacterInfo( element, Region ) ).FirstOrDefault();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
