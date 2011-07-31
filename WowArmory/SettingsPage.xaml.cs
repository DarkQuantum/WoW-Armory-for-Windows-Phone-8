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