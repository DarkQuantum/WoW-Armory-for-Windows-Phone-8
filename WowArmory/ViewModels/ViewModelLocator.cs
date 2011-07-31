using System;

namespace WowArmory.ViewModels
{
	public class ViewModelLocator
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private static FavoritesListViewModel _favoritesList;
		private static SearchViewModel _search;
		private static SearchResultViewModel _searchResult;
		private static CharacterDetailsViewModel _characterDetails;
		private static SettingsViewModel _settings;
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public static FavoritesListViewModel FavoritesListStatic
		{
			get
			{
				if ( _favoritesList == null )
				{
					CreateFavoritesList();
				}
				return _favoritesList;
			}
		}

		public static SearchViewModel SearchStatic
		{
			get
			{
				if ( _search == null )
				{
					CreateSearch();
				}
				return _search;
			}
		}

		public static SearchResultViewModel SearchResultStatic
		{
			get
			{
				if ( _searchResult == null )
				{
					CreateSearchResult();
				}
				return _searchResult;
			}
		}

		public static CharacterDetailsViewModel CharacterDetailsStatic
		{
			get
			{
				if ( _characterDetails == null )
				{
					CreateCharacterDetails();
				}
				return _characterDetails;
			}
		}

		public static SettingsViewModel SettingsStatic
		{
			get
			{
				if ( _settings == null )
				{
					CreateSettings();
				}
				return _settings;
			}
		}

		public FavoritesListViewModel FavoritesList
		{
			get { return FavoritesListStatic; }
		}

		public SearchViewModel Search
		{
			get { return SearchStatic; }
		}

		public SearchResultViewModel SearchResult
		{
			get { return SearchResultStatic; }
		}

		public CharacterDetailsViewModel CharacterDetails
		{
			get { return CharacterDetailsStatic; }
		}

		public SettingsViewModel Settings
		{
			get { return SettingsStatic; }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
		/// </summary>
		public ViewModelLocator()
		{
			CreateFavoritesList();
			CreateSearch();
			CreateSearchResult();
			CreateCharacterDetails();
			CreateSettings();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public static void CreateFavoritesList()
		{
			if ( _favoritesList == null )
			{
				_favoritesList = new FavoritesListViewModel();
			}
		}

		public static void CreateSearch()
		{
			if ( _search == null )
			{
				_search = new SearchViewModel();
			}
		}

		public static void CreateSearchResult()
		{
			if ( _searchResult == null )
			{
				_searchResult = new SearchResultViewModel();
			}
		}

		public static void CreateCharacterDetails()
		{
			if ( _characterDetails == null )
			{
				_characterDetails = new CharacterDetailsViewModel();
			}
		}

		public static void CreateSettings()
		{
			if ( _settings == null )
			{
				_settings = new SettingsViewModel();
			}
		}

		public static void ClearFavoritesList()
		{
			_favoritesList = null;
		}

		public static void ClearSearch()
		{
			_search = null;
		}

		public static void ClearSearchResult()
		{
			_searchResult = null;
		}

		public static void ClearCharacterDetails()
		{
			_characterDetails = null;
		}

		public static void ClearSettings()
		{
			_settings = null;
		}

		public static void Cleanup()
		{
			ClearFavoritesList();
			ClearSearch();
			ClearSearchResult();
			ClearCharacterDetails();
			ClearSettings();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}