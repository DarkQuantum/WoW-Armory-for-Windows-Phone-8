using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Pages
{
	[DataContract]
	public class CharacterTalentsPage : ArmoryPage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterTalentsPage"/> class.
		/// </summary>
		public CharacterTalentsPage()
		{
		}

		public CharacterTalentsPage( ArmoryPage armoryPage )
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
		public static CharacterTalentsPage FromArmoryPage( ArmoryPage armoryPage )
		{
			if ( armoryPage == null ) return null;
			if ( armoryPage.Document == null ) return null;

			var derivedPage = armoryPage as CharacterTalentsPage;
			if ( derivedPage == null )
			{
				derivedPage = new CharacterTalentsPage( armoryPage );
			}
			return derivedPage;
		}

		private void FetchData()
		{
			
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
