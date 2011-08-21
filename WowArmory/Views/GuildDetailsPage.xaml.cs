using System.Windows;
using Microsoft.Phone.Controls;
using WowArmory.Core.Managers;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class GuildDetailsPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the view model associated to this page.
		/// </summary>
		public GuildDetailsViewModel ViewModel
		{
			get
			{
				return (GuildDetailsViewModel)DataContext;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public GuildDetailsPage()
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
		/// Handles the Loaded event of the PhoneApplicationPage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
			Dispatcher.BeginInvoke(() => ViewModel.RefreshView());
		}

		/// <summary>
		/// Handles the MouseLeftButtonDown event of the FavoriteToggle control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
		private void FavoriteToggle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (IsolatedStorageManager.IsGuildStored(ViewModel.Guild.Region, ViewModel.Guild.Realm, ViewModel.Guild.Name))
			{
				IsolatedStorageManager.Unstore(ViewModel.Guild);
				ViewModel.ToggleGuildFavorite();
			}
			else
			{
				pbStoreGuild.IsIndeterminate = true;
				gdStoreGuildOverlay.Visibility = Visibility.Visible;
				Dispatcher.BeginInvoke(() =>
				{
					IsolatedStorageManager.Store(ViewModel.Guild);
					gdStoreGuildOverlay.Visibility = Visibility.Collapsed;
					pbStoreGuild.IsIndeterminate = false;
					ViewModel.ToggleGuildFavorite();
				});
			}
		}

		/// <summary>
		/// Handles the SelectionChanged event of the GuildPivot control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void GuildPivot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

		}

		/// <summary>
		/// Handles the SelectionChanged event of the ListBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (ViewModel.SelectedMember == null)
			{
				return;
			}

			ViewModel.ShowSelectedMember();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}