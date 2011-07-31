using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WowArmory.Core;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Languages;
using WowArmory.Core.Models;
using WowArmory.Core.Storage;
using WowArmory.ViewModels;

namespace WowArmory
{
	public partial class FavoritesListPage : PhoneApplicationPage
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private Region _prevRegion;
		private bool _showAfterRefresh;
		private ApplicationBarMenuItem _removeAllMenu;
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public FavoritesListViewModel ViewModel
		{
			get { return (FavoritesListViewModel)DataContext; }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public FavoritesListPage()
		{
			InitializeComponent();

			BuildApplicationBar();

			// Set the data context of the listbox control to the sample data
			Loaded += FavoritesListPage_Loaded;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		// Load data for the ViewModel Items
		private void FavoritesListPage_Loaded( object sender, RoutedEventArgs e )
		{
			ViewModel.LoadData();
			if ( ViewModel.Characters == null || ViewModel.Characters.Count == 0 )
			{
				_removeAllMenu.IsEnabled = false;
			}
			else
			{
				_removeAllMenu.IsEnabled = true;
			}

			PivotFavorites.IsEnabled = true;
			CharacterListBox.IsEnabled = true;
			CharacterListBox.SelectedIndex = -1;
			CharacterListBox.Opacity = 1;
			spLoadingIndicator.Visibility = Visibility.Collapsed;
			pbLoading.IsIndeterminate = false;
		}

		private void BuildApplicationBar()
		{
			if ( ApplicationBar == null )
			{
				ApplicationBar = new ApplicationBar();
			}

			var searchButton = new ApplicationBarIconButton( new Uri( "/Images/ApplicationBar/CharacterList/search.png", UriKind.Relative ) );
			searchButton.Text = AppResources.UI_ApplicationBar_Search;
			searchButton.Click += ApplicationBarButtonSearch_Click;
			ApplicationBar.Buttons.Add( searchButton );

			var removeAllMenuItem = new ApplicationBarMenuItem( AppResources.UI_ApplicationBar_RemoveAll );
			var settingsMenuItem = new ApplicationBarMenuItem( AppResources.UI_ApplicationBar_Settings );
			removeAllMenuItem.Click += ApplicationBarMenuItemRemoveAll_Click;
			settingsMenuItem.Click += ApplicationBarMenuItemSettings_Click;
			ApplicationBar.MenuItems.Add( removeAllMenuItem );
			ApplicationBar.MenuItems.Add( settingsMenuItem );

			_removeAllMenu = removeAllMenuItem;

			ApplicationBar.IsMenuEnabled = true;
			ApplicationBar.IsVisible = true;
		}

		// Handle selection changed on ListBox
		private void CharacterListBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
		{
			// If selected index is -1 (no selection) do nothing
			if ( CharacterListBox.SelectedIndex == -1 )
				return;

			var autoUpdateCharacter = StorageManager.Settings.ContainsKey( "autoUpdateCharacter" ) ? (bool)StorageManager.Settings[ "autoUpdateCharacter" ] : false;
			var useAutoUpdateTimeInterval = StorageManager.Settings.ContainsKey( "useAutoUpdateTimeInterval" ) ? (bool)StorageManager.Settings[ "useAutoUpdateTimeInterval" ] : false;
			var autoUpdateTimeInterval = StorageManager.Settings.ContainsKey( "autoUpdateTimeInterval" ) ? Convert.ToDouble( Convert.ToInt32( StorageManager.Settings[ "autoUpdateTimeInterval" ] ) ) : 0.00;
			var nextUpdateDate = ViewModel.SelectedCharacter.LastUpdate.AddDays( autoUpdateTimeInterval );

			if ( ViewModel.SelectedCharacter.IsDirty || ( autoUpdateCharacter && ( !useAutoUpdateTimeInterval || ( DateTime.Now.CompareTo( nextUpdateDate ) >= 0 ) ) ) )
			{
				_showAfterRefresh = true;
				RefreshCharacter( ViewModel.SelectedCharacter );
			}
			else
			{
				PivotFavorites.IsEnabled = false;
				spLoadingIndicator.Visibility = Visibility.Visible;
				pbLoading.IsIndeterminate = true;

				ViewModelLocator.CharacterDetailsStatic.SelectedCharacter = ViewModel.SelectedCharacter;
				NavigationService.Navigate( new Uri( "/CharacterDetailsPage.xaml", UriKind.Relative ) );

				PivotFavorites.IsEnabled = true;
				spLoadingIndicator.Visibility = Visibility.Collapsed;
				pbLoading.IsIndeterminate = false;
			}
		}

		private void ApplicationBarButtonSearch_Click( object sender, EventArgs e )
		{
			NavigationService.Navigate( new Uri( "/SearchPage.xaml", UriKind.Relative ) );
		}

		private void ApplicationBarMenuItemRemoveAll_Click( object sender, EventArgs e )
		{
			if ( StorageManager.SavedCharacters == null || StorageManager.SavedCharacters.Count == 0 )
			{
				MessageBox.Show( AppResources.UI_Favorites_MessageBox_NothingToRemove_Body, AppResources.UI_Favorites_MessageBox_NothingToRemove_Title, MessageBoxButton.OK );
				return;
			}

			if ( PivotFavorites.SelectedItem == PivotItemCharacters )
			{
				if ( MessageBox.Show( AppResources.UI_Favorites_MessageBox_RemoveAll_Body, AppResources.UI_Favorites_MessageBox_RemoveAll_Title, MessageBoxButton.OKCancel ) == MessageBoxResult.OK )
				{
					RemoveSavedCharacters();
				}
			}
		}

		private void ApplicationBarMenuItemSettings_Click( object sender, EventArgs e )
		{
			NavigationService.Navigate( new Uri( "/SettingsPage.xaml", UriKind.Relative ) );
		}

		private void RemoveSavedCharacters()
		{
			ViewModel.SelectedCharacter = null;
			StorageManager.DeleteAllSavedCharacters();
			ViewModel.LoadData();
			if ( ViewModel.Characters == null || ViewModel.Characters.Count == 0 )
			{
				_removeAllMenu.IsEnabled = false;
			}
			else
			{
				_removeAllMenu.IsEnabled = true;
			}
		}

		private void CharacterRefresh_Click( object sender, RoutedEventArgs e )
		{
			_showAfterRefresh = false;
			RefreshCharacter( (ArmoryCharacter)( (MenuItem)sender ).DataContext );
		}

		private void CharacterRemove_Click( object sender, RoutedEventArgs e )
		{
			var armoryCharacter = (ArmoryCharacter)( (MenuItem)sender ).DataContext;

			if ( MessageBox.Show( String.Format( AppResources.UI_Favorites_MessageBox_RemoveSelected_Body, armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Name ), AppResources.UI_Favorites_MessageBox_RemoveSelected_Title, MessageBoxButton.OKCancel ) == MessageBoxResult.OK )
			{
				StorageManager.DeleteCharacter( armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Realm, armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Name );
				ViewModel.LoadData();
				if ( ViewModel.Characters == null || ViewModel.Characters.Count == 0 )
				{
					_removeAllMenu.IsEnabled = false;
				}
				else
				{
					_removeAllMenu.IsEnabled = true;
				}
			}
		}

		private void RefreshCharacter( ArmoryCharacter armoryCharacter )
		{
			PivotFavorites.IsEnabled = false;
			spLoadingIndicator.Visibility = Visibility.Visible;
			pbLoading.IsIndeterminate = true;

			_prevRegion = Armory.Current.Region;
			Armory.Current.Region = armoryCharacter.CharacterSheetPage.Region;
			Armory.Current.GetCharacterFromArmoryAsync( armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Name,
				armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Realm,
				GotArmoryCharacter );
		}

		private void GotArmoryCharacter( ArmoryCharacter armoryCharacter )
		{
			Armory.Current.Region = _prevRegion;
			PivotFavorites.IsEnabled = true;
			spLoadingIndicator.Visibility = Visibility.Collapsed;
			pbLoading.IsIndeterminate = false;

			if ( armoryCharacter == null || ( armoryCharacter.CharacterSheetPage == null && armoryCharacter.CharacterReputationPage == null && armoryCharacter.CharacterTalentsPage == null ) )
			{
				MessageBox.Show( AppResources.UI_Search_CouldNotRetrieveInformation, AppResources.UI_Search_CouldNotRetrieveInformation_Caption, MessageBoxButton.OK );
				return;
			}

			if ( !armoryCharacter.IsValid )
			{
				MessageBox.Show( AppResources.UI_Search_ArmoryError, AppResources.UI_Search_CouldNotRetrieveInformation_Caption, MessageBoxButton.OK );
				return;
			}

			StorageManager.DeleteCharacter( armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Realm, armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Name );
			StorageManager.StoreCharacter( armoryCharacter );

			if ( _showAfterRefresh )
			{
				ViewModel.SelectedCharacter = armoryCharacter;
				ViewModelLocator.CharacterDetailsStatic.SelectedCharacter = ViewModel.SelectedCharacter;
				NavigationService.Navigate( new Uri( "/CharacterDetailsPage.xaml", UriKind.Relative ) );
				return;
			}

			ViewModel.LoadData();
			CharacterListBox.SelectedIndex = -1;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}