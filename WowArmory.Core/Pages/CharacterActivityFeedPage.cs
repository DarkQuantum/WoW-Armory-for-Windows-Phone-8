using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using WowArmory.Core.Helpers;
using WowArmory.Core.Models;

namespace WowArmory.Core.Pages
{
	[DataContract]
	public class CharacterActivityFeedPage : ArmoryPage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public string AuthorName { get; set; }
		[DataMember]
		public string Id { get; set; }
		[DataMember]
		public string Title { get; set; }
		[DataMember]
		public DateTime Updated { get; set; }
		[DataMember]
		public ObservableCollection<ActivityFeedEvent> Events { get; set; }

		public bool IsValid
		{
			get
			{
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
		/// Initializes a new instance of the <see cref="CharacterActivityFeedPage"/> class.
		/// </summary>
		public CharacterActivityFeedPage()
		{
		}

		public CharacterActivityFeedPage( ArmoryPage armoryPage )
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
		public static CharacterActivityFeedPage FromArmoryPage( ArmoryPage armoryPage )
		{
			if ( armoryPage == null ) return null;
			if ( armoryPage.Document == null ) return null;

			var derivedPage = armoryPage as CharacterActivityFeedPage;
			if ( derivedPage == null )
			{
				derivedPage = new CharacterActivityFeedPage( armoryPage );
			}
			return derivedPage;
		}

		private void FetchData()
		{
			Events = Root.Elements( "event" ).Select( e => new ActivityFeedEvent( e ) { Region = Region } ).OrderByDescending( e => e.Timestamp ).ToObservableCollection();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
