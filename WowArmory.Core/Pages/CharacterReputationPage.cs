using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using WowArmory.Core.Helpers;
using WowArmory.Core.Models;

namespace WowArmory.Core.Pages
{
	[DataContract]
	public class CharacterReputationPage : ArmoryPage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public ObservableCollection<ReputationFaction> Factions { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterReputationPage"/> class.
		/// </summary>
		public CharacterReputationPage()
		{
		}

		public CharacterReputationPage( ArmoryPage armoryPage )
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
		public static CharacterReputationPage FromArmoryPage( ArmoryPage armoryPage )
		{
			if ( armoryPage == null ) return null;
			if ( armoryPage.Document == null ) return null;

			var derivedPage = armoryPage as CharacterReputationPage;
			if ( derivedPage == null )
			{
				derivedPage = new CharacterReputationPage( armoryPage );
			}
			return derivedPage;
		}

		private void FetchData()
		{
			var characterInfo = Root.Element( "characterInfo" );
			if ( characterInfo != null )
			{
				var reputationTab = characterInfo.Element( "reputationTab" );
				if ( reputationTab != null )
				{
					Factions = reputationTab.Elements( "faction" ).Select( e => new ReputationFaction( e ) ).ToObservableCollection();
				}
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
