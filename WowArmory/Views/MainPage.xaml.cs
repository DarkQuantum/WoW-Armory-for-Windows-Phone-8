using System.Windows;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;
using WowArmory.Controllers;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.Enumerations;

namespace WowArmory.Views
{
	public partial class MainPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public MainPage()
		{
			InitializeComponent();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Called when one of the buttons was clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void MainPageButtonClicked(object sender, RoutedEventArgs e)
		{
			SetValue(RadTileAnimation.ContainerToAnimateProperty, TileContainer);
		}

		/// <summary>
		/// Handles the Loaded event of the PhoneApplicationPage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
			if (IsolatedStorageManager.IsFirstTimeUsage)
			{
				IsolatedStorageManager.SetValue("Temp_Setting_IsFirstTimeUsage", false);
				IsolatedStorageManager.IsFirstTimeUsage = false;
				IsolatedStorageManager.Save();

				if (MessageBox.Show(AppResources.UI_Main_FirstTimeUsage_Text, AppResources.UI_Main_FirstTimeUsage_Caption, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
				{
					ApplicationController.Current.NavigateTo(Page.Settings);
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}