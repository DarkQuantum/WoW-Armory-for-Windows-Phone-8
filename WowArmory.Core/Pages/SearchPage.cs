using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using WowArmory.Core.Helpers;
using WowArmory.Core.Models;

namespace WowArmory.Core.Pages
{
	[DataContract]
	public class SearchPage : ArmoryPage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public ObservableCollection<SearchResultCharacter> Characters { get; set; }
		[DataMember]
		public Dictionary<string, int> Tabs { get; set; }

		public bool HasFoundCharacters
		{
			get { return GetFoundTypeCount( "characters" ) > 0; }
		}

		public bool HasFoundGuilds
		{
			get { return GetFoundTypeCount( "guilds" ) > 0; }
		}

		public bool IsValid
		{
			get
			{
				if ( !HasFoundCharacters && !HasFoundGuilds ) return false;

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
		/// Initializes a new instance of the <see cref="SearchPage"/> class.
		/// </summary>
		public SearchPage()
		{
		}

		public SearchPage( ArmoryPage armoryPage )
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
		public static SearchPage FromArmoryPage( ArmoryPage armoryPage )
		{
			var derivedPage = armoryPage as SearchPage;
			if ( derivedPage == null )
			{
				derivedPage = new SearchPage( armoryPage );
			}
			return derivedPage;
		}

		private void FetchData()
		{
			if ( Root != null )
			{
				var armorySearch = Root.Element( "armorySearch" );
				if ( armorySearch != null )
				{
					var tabs = armorySearch.Element( "tabs" );
					if ( tabs != null )
					{
						Tabs = new Dictionary<string, int>();
						foreach ( var tab in tabs.Elements( "tab" ) )
						{
							Tabs.Add( tab.GetAttributeValue( "type" ), Int32.Parse( tab.GetAttributeValue( "count" ), CultureInfo.InvariantCulture ) );
						}
					}

					var results = armorySearch.Element( "searchResults" );
					if ( results != null )
					{
						var characters = results.Element( "characters" );
						if ( characters != null )
						{
							Characters = characters.Elements( "character" ).Select( e => new SearchResultCharacter( e ) ).OrderByDescending( k => k.Relevance ).ToObservableCollection();
						}
					}
				}
			}
		}

		private int GetFoundTypeCount( string searchType )
		{
			if ( Tabs != null && Tabs.Count > 0 && Tabs.ContainsKey( searchType ) ) return Tabs[ searchType ];
			return 0;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
