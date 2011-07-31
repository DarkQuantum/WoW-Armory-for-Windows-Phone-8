using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using WowArmory.Core;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Languages;
using WowArmory.Core.Storage;

namespace WowArmory.ViewModels
{
	public class SettingsViewModel : ViewModelBase
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private ObservableCollection<KeyValuePair<string, string>> _regions;
		private ObservableCollection<KeyValuePair<string, string>> _favoritesSortBy;
		private ObservableCollection<KeyValuePair<string, string>> _autoUpdateTimeIntervals;
		private string _selectedRegion = Armory.Current.Region.ToString();
		private string _selectedFavoritsSortBy;
		private string _selectedAutoUpdateTimeInterval;
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public ObservableCollection<KeyValuePair<string, string>> Regions
		{
			get
			{
				if ( _regions == null )
				{
					_regions = new ObservableCollection<KeyValuePair<string,string>>();
					var availableRegions = AppResources.Regions;
					foreach ( var availableRegion in availableRegions.Split( new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries ) )
					{
						var kvp = new KeyValuePair<string, string>( availableRegion, AppResources.ResourceManager.GetString( String.Format( "Region_{0}", availableRegion ) ) );
						_regions.Add( kvp );
					}
				}

				return _regions;
			}
		}

		public ObservableCollection<KeyValuePair<string, string>> FavoritesSortBy
		{
			get
			{
				if ( _favoritesSortBy == null )
				{
					_favoritesSortBy = new ObservableCollection<KeyValuePair<string, string>>();
					var favoritesSortBy = AppResources.Settings_FavoritesSortBy;
					foreach ( var sortBy in favoritesSortBy.Split( new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries ) )
					{
						var kvp = new KeyValuePair<string, string>( sortBy, AppResources.ResourceManager.GetString( String.Format( "Settings_FavoritesSortBy_{0}", sortBy ) ) );
						_favoritesSortBy.Add( kvp );
					}
				}

				return _favoritesSortBy;
			}
		}

		public ObservableCollection<KeyValuePair<string, string>> AutoUpdateTimeIntervals
		{
			get
			{
				if ( _autoUpdateTimeIntervals == null )
				{
					_autoUpdateTimeIntervals = new ObservableCollection<KeyValuePair<string, string>>();
					var timeIntervals = AppResources.UI_Settings_AutoUpdateSinceLastUpdate;
					foreach ( var timeInterval in timeIntervals.Split( new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries ) )
					{
						var kvp = new KeyValuePair<string, string>( timeInterval, AppResources.ResourceManager.GetString( String.Format( "UI_Settings_AutoUpdateSinceLastUpdate_{0}", timeInterval ) ) );
						_autoUpdateTimeIntervals.Add( kvp );
					}
				}

				return _autoUpdateTimeIntervals;
			}
		}

		public KeyValuePair<string, string> SelectedRegion
		{
			get
			{
				if ( !IsInDesignMode )
				{
					return Regions.Where( kvp => kvp.Key.Equals( Armory.Current.Region.ToString(), StringComparison.CurrentCultureIgnoreCase ) ).FirstOrDefault();
				}
				else
				{
					return Regions[0];
				}
			}
			set
			{
				_selectedRegion = value.Value;
			}
		}

		public KeyValuePair<string, string> SelectedFavoritesSortBy
		{
			get
			{
				var selectedFavoritesSortBy = FavoritesSortBy[ 0 ].Key;
				if ( StorageManager.Settings.ContainsKey( "favoritesSortBy" ) )
				{
					selectedFavoritesSortBy = StorageManager.Settings[ "favoritesSortBy" ].ToString();
				}
				return FavoritesSortBy.Where( kvp => kvp.Key.Equals( selectedFavoritesSortBy, StringComparison.CurrentCultureIgnoreCase ) ).FirstOrDefault();
			}
			set
			{
				_selectedFavoritsSortBy = value.Value;
			}
		}

		public KeyValuePair<string, string> SelectedAutoUpdateTimeInterval
		{
			get
			{
				var selectedAutoUpdateTimeInterval = AutoUpdateTimeIntervals[ 0 ].Key;
				if ( StorageManager.Settings.ContainsKey( "autoUpdateTimeInterval" ) )
				{
					selectedAutoUpdateTimeInterval = StorageManager.Settings[ "autoUpdateTimeInterval" ].ToString();
				}
				return AutoUpdateTimeIntervals.Where( kvp => kvp.Key.Equals( selectedAutoUpdateTimeInterval, StringComparison.CurrentCultureIgnoreCase ) ).FirstOrDefault();
			}
			set
			{
				_selectedAutoUpdateTimeInterval = value.Value;
			}
		}

		public bool AutoUpdateCharacter
		{
			get { return StorageManager.Settings.ContainsKey( "autoUpdateCharacter" ) ? (bool)StorageManager.Settings[ "autoUpdateCharacter" ] : false; }
		}

		public bool UseAutoUpdateTimeInterval
		{
			get { return StorageManager.Settings.ContainsKey( "useAutoUpdateTimeInterval" ) ? (bool)StorageManager.Settings[ "useAutoUpdateTimeInterval" ] : false; }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
		/// </summary>
		public SettingsViewModel()
		{
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
