using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WowArmory.Controllers;
using WowArmory.Core.BattleNet;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class SettingsPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the view model associated to this view.
		/// </summary>
		public SettingsViewModel ViewModel
		{
			get
			{
				return (SettingsViewModel)DataContext;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public SettingsPage()
		{
			InitializeComponent();
			BuildApplicationBar();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Builds the application bar.
		/// </summary>
		private void BuildApplicationBar()
		{
			if (ApplicationBar == null)
			{
				ApplicationBar = new ApplicationBar();
			}

			var saveButton = new ApplicationBarIconButton(new Uri("/Images/ApplicationBar/Settings/save.png", UriKind.Relative));
			saveButton.Text = AppResources.UI_Settings_ApplicationBar_Save;
			saveButton.Click += SaveSettings;

			var cancelButton = new ApplicationBarIconButton(new Uri("/Images/ApplicationBar/Settings/cancel.png", UriKind.Relative));
			cancelButton.Text = AppResources.UI_Settings_ApplicationBar_Cancel;
			cancelButton.Click += CancelSettings;

			ApplicationBar.Buttons.Add(saveButton);
			ApplicationBar.Buttons.Add(cancelButton);
		}

		/// <summary>
		/// Saves the settings.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SaveSettings(object sender, EventArgs e)
		{
			// set settings
			IsolatedStorageManager.Region = ViewModel.SelectedRegion.Key;
			BattleNetClient.Current.Region = IsolatedStorageManager.Region;

			// save settings
			IsolatedStorageManager.Save();

			// navigate back to previous page
			ApplicationController.Current.NavigateBack();
		}

		/// <summary>
		/// Cancel any changes.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void CancelSettings(object sender, EventArgs e)
		{
			// reset changes in view model
			ViewModel.ResetSettings();

			// navigate back to previous page
			ApplicationController.Current.NavigateBack();
		}

		/// <summary>
		/// This method is called when the hardware back key is pressed.
		/// </summary>
		/// <param name="e">Set e.Cancel to true to indicate that the request was handled by the application.</param>
		protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
		{
			base.OnBackKeyPress(e);

			// handle region list picker
			if (lpRegion.ListPickerMode == ListPickerMode.Expanded)
			{
				lpRegion.ListPickerMode = ListPickerMode.Normal;
				e.Cancel = true;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}