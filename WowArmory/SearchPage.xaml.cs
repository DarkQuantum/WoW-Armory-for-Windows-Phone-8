// 
// Copyright (c) 2010 Christian Krueger <christian.krueger@krueger-c.com>
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge and for non-commercial usage
// only, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify,
// merge, publish, and/or distribute copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Microsoft.Phone.Controls;
using WowArmory.Core;
using WowArmory.Core.Helpers;
using WowArmory.Core.Languages;
using WowArmory.Core.Models;
using WowArmory.Core.Pages;
using WowArmory.Core.Storage;
using WowArmory.ViewModels;

namespace WowArmory
{
	public partial class SearchPage : PhoneApplicationPage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public SearchViewModel ViewModel
		{
			get { return (SearchViewModel)DataContext; }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public SearchPage()
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
			EnableSearchInput( true );
			pbSearching.Visibility = Visibility.Collapsed;
			pbSearching.IsIndeterminate = false;
		}

		private void btnSearch_Click( object sender, RoutedEventArgs e )
		{
			Search();
		}

		private void GotArmoryCharacter( ArmoryCharacter armoryCharacter )
		{
			EnableSearchInput( true );
			pbSearching.Visibility = Visibility.Collapsed;
			pbSearching.IsIndeterminate = false;

			if ( armoryCharacter == null || armoryCharacter.CharacterSheetPage == null )
			{
				MessageBox.Show( AppResources.UI_Search_CouldNotRetrieveInformation, AppResources.UI_Search_CouldNotRetrieveInformation_Caption, MessageBoxButton.OK );
				return;
			}

			if ( !armoryCharacter.IsValid )
			{
				var searchResult = AppResources.UI_Search_ArmoryError;

				if ( armoryCharacter.CharacterSheetPage.ErrCode.Equals( "noCharacter", StringComparison.CurrentCultureIgnoreCase ) )
				{
					searchResult = AppResources.UI_Search_NoCharacterFound;
				}

				MessageBox.Show( searchResult, AppResources.UI_Search_CouldNotRetrieveInformation_Caption, MessageBoxButton.OK );
				return;
			}

			ViewModelLocator.CharacterDetailsStatic.SelectedCharacter = armoryCharacter;
			NavigationService.Navigate( new Uri( "/CharacterDetailsPage.xaml", UriKind.Relative ) );
		}

		private void GotSearchPage( Core.Pages.SearchPage searchPage )
		{
			EnableSearchInput( true );
			pbSearching.Visibility = Visibility.Collapsed;
			pbSearching.IsIndeterminate = false;

			if ( searchPage == null || searchPage.Document == null || searchPage.Root == null )
			{
				MessageBox.Show( AppResources.UI_Search_CouldNotRetrieveInformation, AppResources.UI_Search_CouldNotRetrieveInformation_Caption, MessageBoxButton.OK );
				return;
			}

			if ( !searchPage.IsValid )
			{
				MessageBox.Show( AppResources.UI_Search_NoCharacterFound, AppResources.UI_Search_NoCharacterFound_Title, MessageBoxButton.OK );
				return;
			}

			ViewModelLocator.SearchResultStatic.Result = searchPage;
			NavigationService.Navigate( new Uri( "/SearchResultPage.xaml", UriKind.Relative ) );
		}

		private void EnableSearchInput( bool enable )
		{
			txtName.IsEnabled = enable;
			txtRealmName.IsEnabled = enable;
			btnSearch.IsEnabled = enable;
			lpSearchType.IsEnabled = enable;
			lpRegion.IsEnabled = enable;
		}

		private void txtName_KeyDown( object sender, KeyEventArgs e )
		{
			if ( e.Key == Key.Enter )
			{
				ViewModel.SearchName = txtName.Text;
				Search();
			}
		}

		private void txtRealmName_KeyDown( object sender, KeyEventArgs e )
		{
			if ( e.Key == Key.Enter )
			{
				ViewModel.SearchRealm = txtRealmName.Text;
				Search();
			}
		}

		private void Search()
		{
			var searchType = ViewModel.SelectedSearchType;
			var searchQuery = ViewModel.SearchName;
			var realmName = ViewModel.SearchRealm;

			Armory.Current.SetRegionByString( ViewModel.SelectedRegion.Key );
			StorageManager.Settings.Upsert( "region", Armory.Current.Region );

			if ( String.IsNullOrEmpty( searchQuery ) )
			{
				MessageBox.Show( AppResources.UI_Search_NoNameSpecified, AppResources.UI_Search_NoNameSpecified_Caption, MessageBoxButton.OK );
				return;
			}

			if ( searchQuery.Length < 2 )
			{
				MessageBox.Show( AppResources.UI_Search_NameLengthInvalid, AppResources.UI_Search_NameLengthInvalid_Caption, MessageBoxButton.OK );
				return;
			}

			EnableSearchInput( false );
			ViewModel.ResetSearchResult();
			pbSearching.Visibility = Visibility.Visible;
			pbSearching.IsIndeterminate = true;
			try
			{
				if ( !String.IsNullOrEmpty( realmName ) )
				{
					Armory.Current.GetCharacterFromArmoryAsync( searchQuery, realmName, GotArmoryCharacter );
				}
				else
				{
					Armory.Current.GetSearchPageAsync( searchType, searchQuery, GotSearchPage );
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( AppResources.UI_Search_CouldNotRetrieveInformation, AppResources.UI_Search_CouldNotRetrieveInformation_Caption, MessageBoxButton.OK );
			}
		}

		protected override void OnBackKeyPress( System.ComponentModel.CancelEventArgs e )
		{
			base.OnBackKeyPress( e );

			if ( lpRegion.ListPickerMode == ListPickerMode.Expanded )
			{
				lpRegion.ListPickerMode = ListPickerMode.Normal;
				e.Cancel = true;
			}

			if ( lpSearchType.ListPickerMode == ListPickerMode.Expanded )
			{
				lpSearchType.ListPickerMode = ListPickerMode.Normal;
				e.Cancel = true;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}