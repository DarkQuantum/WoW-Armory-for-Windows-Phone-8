using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WowArmory.Controllers;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.Models;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class GuildListPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private ApplicationBarIconButton _deleteAllButton;
		private ApplicationBarMenuItem _sortByNameMenuItem;
		private ApplicationBarMenuItem _sortByLevelMenuItem;
		private ApplicationBarMenuItem _sortByMembersMenuItem;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the view model associated to this view.
		/// </summary>
		public GuildListViewModel ViewModel
		{
			get
			{
				return (GuildListViewModel)DataContext;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="GuildListPage"/> class.
		/// </summary>
		public GuildListPage()
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
		/// Handles the Loaded event of the PhoneApplicationPage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
			ViewModel.RefreshView();
			UpdateApplicationBarItems();
		}

		/// <summary>
		/// Builds the application bar.
		/// </summary>
		private void BuildApplicationBar()
		{
			if (ApplicationBar == null)
			{
				ApplicationBar = new ApplicationBar();
			}

			var searchButton = new ApplicationBarIconButton(new Uri("/Images/ApplicationBar/GuildList/search.png", UriKind.Relative));
			searchButton.Text = AppResources.UI_GuildList_ApplicationBar_Search;
			searchButton.Click += ShowGuildSearchView;

			_deleteAllButton = new ApplicationBarIconButton(new Uri("/Images/ApplicationBar/CharacterList/delete.png", UriKind.Relative));
			_deleteAllButton.Text = AppResources.UI_CharacterList_ApplicationBar_DeleteAll;
			_deleteAllButton.Click += DeleteAll;

			_sortByNameMenuItem = new ApplicationBarMenuItem(AppResources.UI_GuildList_ApplicationBar_SortByName);
			_sortByNameMenuItem.Click += SortByName;

			_sortByLevelMenuItem = new ApplicationBarMenuItem(AppResources.UI_GuildList_ApplicationBar_SortByLevel);
			_sortByLevelMenuItem.Click += SortByLevel;

			_sortByMembersMenuItem = new ApplicationBarMenuItem(AppResources.UI_GuildList_ApplicationBar_SortByMembers);
			_sortByMembersMenuItem.Click += SortByMembers;

			ApplicationBar.Buttons.Add(searchButton);
			ApplicationBar.Buttons.Add(_deleteAllButton);
			ApplicationBar.MenuItems.Add(_sortByNameMenuItem);
			ApplicationBar.MenuItems.Add(_sortByLevelMenuItem);
			ApplicationBar.MenuItems.Add(_sortByMembersMenuItem);

			UpdateApplicationBarItems();
		}

		/// <summary>
		/// Updates the application bar items.
		/// </summary>
		private void UpdateApplicationBarItems()
		{
			var enabled = true;
			if (ViewModel.FavoriteGuilds.Count == 0)
			{
				enabled = false;
			}

			_deleteAllButton.IsEnabled = enabled;
			_sortByNameMenuItem.IsEnabled = enabled;
			_sortByLevelMenuItem.IsEnabled = enabled;
			_sortByMembersMenuItem.IsEnabled = enabled;
		}

		/// <summary>
		/// Inverts the type the list is sorted by.
		/// </summary>
		private void InvertSortByType()
		{
			AppSettingsManager.GuildListSortByType = AppSettingsManager.GuildListSortByType == SortBy.Ascending ? SortBy.Descending : SortBy.Ascending;
		}

		/// <summary>
		/// Sorts the character list by name.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SortByName(object sender, EventArgs e)
		{
			if (AppSettingsManager.GuildListSortBy == GuildListSortBy.Name)
			{
				InvertSortByType();
			}
			else
			{
				AppSettingsManager.GuildListSortByType = SortBy.Ascending;
			}
			AppSettingsManager.GuildListSortBy = GuildListSortBy.Name;
			SortGuildList();
		}

		/// <summary>
		/// Sorts the character list by level.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SortByLevel(object sender, EventArgs e)
		{
			if (AppSettingsManager.GuildListSortBy == GuildListSortBy.Level)
			{
				InvertSortByType();
			}
			else
			{
				AppSettingsManager.GuildListSortByType = SortBy.Descending;
			}
			AppSettingsManager.GuildListSortBy = GuildListSortBy.Level;
			SortGuildList();
		}

		/// <summary>
		/// Sorts the character list by member count.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SortByMembers(object sender, EventArgs e)
		{
			if (AppSettingsManager.GuildListSortBy == GuildListSortBy.Members)
			{
				InvertSortByType();
			}
			else
			{
				AppSettingsManager.GuildListSortByType = SortBy.Descending;
			}
			AppSettingsManager.GuildListSortBy = GuildListSortBy.Members;
			SortGuildList();
		}

		/// <summary>
		/// Sorts the guild list.
		/// </summary>
		private void SortGuildList()
		{
			ViewModel.RefreshView();
			UpdateApplicationBarItems();
		}

		/// <summary>
		/// Shows the guild search view.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ShowGuildSearchView(object sender, EventArgs e)
		{
			ApplicationController.Current.NavigateTo(Enumerations.Page.GuildSearch);
		}

		/// <summary>
		/// Handles the SelectionChanged event of the ListBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (ViewModel.SelectedGuild == null)
			{
				return;
			}

			ViewModel.ShowSelectedGuild();
		}

		/// <summary>
		/// Deletes all stored guilds.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void DeleteAll(object sender, EventArgs e)
		{
			if (MessageBox.Show(AppResources.UI_GuildList_DeleteAll_Text, AppResources.UI_GuildList_DeleteAll_Caption, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
			{
				return;
			}

			while (IsolatedStorageManager.StoredGuilds.Count > 0)
			{
				IsolatedStorageManager.StoredGuilds.Remove(IsolatedStorageManager.StoredGuilds[0]);
			}

			ViewModel.RefreshView();
			UpdateApplicationBarItems();
		}

		/// <summary>
		/// Handles the Click event of the GuildContextMenuItemUpdate control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void GuildContextMenuItemUpdate_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.PreventNextNavigation = true;
			var guild = (GuildListItem)((MenuItem)sender).DataContext;
			ViewModel.UpdateGuild(guild.Region, guild.Realm, guild.Name);
		}

		/// <summary>
		/// Handles the Click event of the GuildContextMenuItemRemove control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void GuildContextMenuItemRemove_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(AppResources.UI_GuildList_Delete_Text, AppResources.UI_GuildList_Delete_Caption, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
			{
				return;
			}

			var guild = (GuildListItem)((MenuItem)sender).DataContext;
			var storageData = IsolatedStorageManager.GetStoredGuild(guild.Region, guild.Realm, guild.Name);
			IsolatedStorageManager.StoredGuilds.Remove(storageData);

			ViewModel.RefreshView();
			UpdateApplicationBarItems();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}