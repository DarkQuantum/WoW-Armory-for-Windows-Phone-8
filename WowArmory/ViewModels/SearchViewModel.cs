﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using WowArmory.Core;
using WowArmory.Core.Languages;

namespace WowArmory.ViewModels
{
	public class SearchViewModel : ViewModelBase
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private ObservableCollection<KeyValuePair<string, string>> _searchTypes;
		private ObservableCollection<KeyValuePair<string, string>> _regions;
		private int _selectedSearchTypeIndex = 0;
		private KeyValuePair<string, string> _selectedRegion;

		private string _searchName = "";
		private string _searchRealm = "";
		private string _searchResult = String.Empty;
		private int _searchResultExists = 0;
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public ObservableCollection<KeyValuePair<string,string>> SearchTypes
		{
			get
			{
				if ( _searchTypes == null )
				{
					_searchTypes = new ObservableCollection<KeyValuePair<string, string>>();
					//_searchTypes.Add( new KeyValuePair<string, string>( "all", AppResources.SearchType_All ) );
					_searchTypes.Add( new KeyValuePair<string, string>( "characters", AppResources.SearchType_Characters ) );
					//_searchTypes.Add( new KeyValuePair<string, string>( "guilds", AppResources.SearchType_Guilds ) );
					//_searchTypes.Add( new KeyValuePair<string, string>( "arenateams", AppResources.SearchType_ArenaTeams ) );
					//_searchTypes.Add( new KeyValuePair<string, string>( "items", AppResources.SearchType_Items ) );
				}

				return _searchTypes;
			}
		}

		public ObservableCollection<KeyValuePair<string, string>> Regions
		{
			get
			{
				if ( _regions == null )
				{
					_regions = new ObservableCollection<KeyValuePair<string, string>>();
					var availableRegions = AppResources.Regions;
					foreach ( var availableRegion in availableRegions.Split( new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries ) )
					{
						var kvp = new KeyValuePair<string, string>( availableRegion, AppResources.ResourceManager.GetString( String.Format( "Region_{0}", availableRegion ) ) );
						_regions.Add( kvp );
					}

					_selectedRegion = _regions.Where( kvp => kvp.Key.Equals( Armory.Current.Region.ToString(), StringComparison.CurrentCultureIgnoreCase ) ).FirstOrDefault();
				}

				return _regions;
			}
		}

		public string SelectedSearchType
		{
			get { return SearchTypes[ SelectedSearchTypeIndex ].Value; }
		}

		public int SelectedSearchTypeIndex
		{
			get { return _selectedSearchTypeIndex; }
			set
			{
				if ( _selectedSearchTypeIndex == value ) return;

				_selectedSearchTypeIndex = value;
				RaisePropertyChanged( "SelectedSearchType" );
				RaisePropertyChanged( "SelectedSearchTypeIndex" );
			}
		}

		public KeyValuePair<string, string> SelectedRegion
		{
			get
			{
				return _selectedRegion;
			}
			set
			{
				_selectedRegion = value;
			}
		}

		public string SearchName
		{
			get { return _searchName; }
			set
			{
				if ( _searchName == value ) return;

				_searchName = value;
				RaisePropertyChanged( "SearchName" );
			}
		}

		public string SearchRealm
		{
			get { return _searchRealm; }
			set
			{
				if ( _searchRealm == value ) return;

				_searchRealm = value;
				RaisePropertyChanged( "SearchRealm" );
			}
		}

		public string SearchResult
		{
			get { return _searchResult; }
			set
			{
				if ( _searchResult == value ) return;

				_searchResult = value;
				RaisePropertyChanged( "SearchResult" );
			}
		}

		public int SearchResultExists
		{
			get { return _searchResultExists; }
			set
			{
				if ( _searchResultExists == value ) return;

				_searchResultExists = value;
				RaisePropertyChanged( "SearchResultExists" );
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="SearchViewModel"/> class.
		/// </summary>
		public SearchViewModel()
		{
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public void ResetSearchResult()
		{
			SearchResult = String.Empty;
			SearchResultExists = 0;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
