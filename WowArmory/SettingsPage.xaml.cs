using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WowArmory.Core;
using WowArmory.Core.Helpers;
using WowArmory.Core.Languages;
using WowArmory.Core.Storage;
using WowArmory.ViewModels;

namespace WowArmory
{
	public partial class SettingsPage : PhoneApplicationPage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public SettingsViewModel ViewModel
		{
			get { return (SettingsViewModel)DataContext; }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public SettingsPage()
		{
			InitializeComponent();

			BuildApplicationBar();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		private void PhoneApplicationPage_Loaded( object sender, RoutedEventArgs e )
		{
			tsAutoUpdateCharacter_Click( sender, e );
		}
		
		private void BuildApplicationBar()
		{
			if ( ApplicationBar == null )
			{
				ApplicationBar = new ApplicationBar();
			}

			var saveButton = new ApplicationBarIconButton( new Uri( "/Images/ApplicationBar/Settings/save.png", UriKind.Relative ) );
			saveButton.Text = AppResources.UI_Settings_ApplicationBar_Save;
			saveButton.Click += SaveSettings;
			var cancelButton = new ApplicationBarIconButton( new Uri( "/Images/ApplicationBar/Settings/cancel.png", UriKind.Relative ) );
			cancelButton.Text = AppResources.UI_Settings_ApplicationBar_Cancel;
			cancelButton.Click += CancelSettings;

			ApplicationBar.Buttons.Add( saveButton );
			ApplicationBar.Buttons.Add( cancelButton );
		}

		private void SaveSettings( object sender, EventArgs e )
		{
			var favoritesSortBy = ( (KeyValuePair<string, string>)lpFavoritesSortBy.SelectedItem ).Key;
			StorageManager.Settings.Upsert( "favoritesSortBy", favoritesSortBy );

			var autoUpdateCharacter = tsAutoUpdateCharacter.IsChecked;
			StorageManager.Settings.Upsert( "autoUpdateCharacter", autoUpdateCharacter );

			var useAutoUpdateTimeInterval = tsUseAutoUpdateTimeInterval.IsChecked;
			StorageManager.Settings.Upsert( "useAutoUpdateTimeInterval", useAutoUpdateTimeInterval );

			var autoUpdateTimeInterval = ( (KeyValuePair<string, string>)lpAutoUpdateTimeInterval.SelectedItem ).Key;
			StorageManager.Settings.Upsert( "autoUpdateTimeInterval", autoUpdateTimeInterval );

			NavigateBack();
		}

		private void CancelSettings( object sender, EventArgs e )
		{
			NavigateBack();
		}

		private void NavigateBack()
		{
			if ( NavigationService.CanGoBack )
			{
				NavigationService.GoBack();
			}
		}

		private void tsAutoUpdateCharacter_Click( object sender, RoutedEventArgs e )
		{
			if ( tsAutoUpdateCharacter.IsChecked == true )
			{
				tsUseAutoUpdateTimeInterval.Visibility = Visibility.Visible;
				tsUseAutoUpdateTimeInterval_Click( sender, e );
			}
			else
			{
				tsUseAutoUpdateTimeInterval.Visibility = Visibility.Collapsed;
				lpAutoUpdateTimeInterval.Visibility = Visibility.Collapsed;
			}
		}

		private void tsUseAutoUpdateTimeInterval_Click( object sender, RoutedEventArgs e )
		{
			if ( tsUseAutoUpdateTimeInterval.IsChecked == true )
			{
				lpAutoUpdateTimeInterval.Visibility = Visibility.Visible;
			}
			else
			{
				lpAutoUpdateTimeInterval.Visibility = Visibility.Collapsed;
			}
		}

		protected override void OnBackKeyPress( System.ComponentModel.CancelEventArgs e )
		{
			base.OnBackKeyPress( e );

			if ( lpFavoritesSortBy.ListPickerMode == ListPickerMode.Expanded )
			{
				lpFavoritesSortBy.ListPickerMode = ListPickerMode.Normal;
				e.Cancel = true;
			}

			if ( lpAutoUpdateTimeInterval.ListPickerMode == ListPickerMode.Expanded )
			{
				lpAutoUpdateTimeInterval.ListPickerMode = ListPickerMode.Normal;
				e.Cancel = true;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}