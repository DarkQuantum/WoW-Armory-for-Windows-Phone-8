using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WowArmory.Core;
using WowArmory.Core.Languages;
using WowArmory.Core.Models;
using WowArmory.Core.Pages;
using WowArmory.ViewModels;

namespace WowArmory
{
	public partial class SearchResultPage : PhoneApplicationPage
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private FrameworkElement _currentListBox;
		private static bool _isLoading = false;
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public SearchResultViewModel ViewModel
		{
			get { return (SearchResultViewModel)DataContext; }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public SearchResultPage()
		{
			InitializeComponent();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		private void PhoneApplicationPage_Loaded( object sender, RoutedEventArgs e )
		{
			if ( ViewModel.Result == null )
			{
				if ( NavigationService.CanGoBack ) NavigationService.GoBack();

				return;
			}

			if ( ViewModel.Result.HasFoundCharacters )
			{
				lbFoundCharacters.Visibility = Visibility.Visible;
				_currentListBox = lbFoundCharacters;
			}
			else if ( ViewModel.Result.HasFoundGuilds )
			{
				
			}
			else
			{
				
			}

			( (ListBox)_currentListBox ).IsEnabled = true;
			( (ListBox)_currentListBox ).SelectedIndex = -1;
			//_currentListBox.Opacity = 1;
			spLoadingIndicator.Visibility = Visibility.Collapsed;
			pbLoading.IsIndeterminate = false;
			_isLoading = false;
		}
		
		private void lbFoundCharacters_SelectionChanged( object sender, SelectionChangedEventArgs e )
		{
			if ( _isLoading ) return;
			_isLoading = true;
			if ( lbFoundCharacters.SelectedIndex == -1 ) return;

			( (ListBox)_currentListBox ).IsEnabled = false;
			pbLoading.IsIndeterminate = true;
			spLoadingIndicator.Visibility = Visibility.Visible;
			Armory.Current.GetCharacterFromArmoryAsync( ViewModel.SelectedCharacter.Name, ViewModel.SelectedCharacter.Realm, GotArmoryCharacter );
		}

		private void GotArmoryCharacter( ArmoryCharacter armoryCharacter )
		{
			if ( armoryCharacter != null )
			{
				if ( !armoryCharacter.IsValid )
				{
					( (ListBox)_currentListBox ).IsEnabled = true;
					lbFoundCharacters.SelectedIndex = -1;
					spLoadingIndicator.Visibility = Visibility.Collapsed;
					pbLoading.IsIndeterminate = false;
					_isLoading = false;

					MessageBox.Show( AppResources.UI_Search_ArmoryError, AppResources.UI_Search_CouldNotRetrieveInformation_Caption, MessageBoxButton.OK );
					return;
				}

				ViewModelLocator.CharacterDetailsStatic.SelectedCharacter = armoryCharacter;
				NavigationService.Navigate( new Uri( "/CharacterDetailsPage.xaml", UriKind.Relative ) );
				_isLoading = false;
				return;
			}

			MessageBox.Show( AppResources.UI_Search_CouldNotRetrieveInformation, AppResources.UI_Search_CouldNotRetrieveInformation_Caption, MessageBoxButton.OK );
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}