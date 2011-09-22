using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WowArmory.Controllers;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.Enumerations;
using WowArmory.Models;
using WowArmory.ViewModels;
using Page = WowArmory.Enumerations.Page;

namespace WowArmory.Views
{
	public partial class CharacterListPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private ApplicationBarIconButton _deleteAllButton;
		private ApplicationBarMenuItem _sortByNameMenuItem;
		private ApplicationBarMenuItem _sortByLevelMenuItem;
		private ApplicationBarMenuItem _sortByAchievementPointsMenuItem;
		private CharacterListItem _character;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the view model associated to this view.
		/// </summary>
		public CharacterListViewModel ViewModel
		{
			get
			{
				return (CharacterListViewModel)DataContext;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public CharacterListPage()
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

			var searchButton = new ApplicationBarIconButton(new Uri("/Images/ApplicationBar/CharacterList/search.png", UriKind.Relative));
			searchButton.Text = AppResources.UI_CharacterList_ApplicationBar_Search;
			searchButton.Click += ShowCharacterSearchView;

			var groupsButton = new ApplicationBarIconButton(new Uri("/Images/ApplicationBar/CharacterList/groups.png", UriKind.Relative));
			groupsButton.Text = AppResources.UI_CharacterList_ApplicationBar_Groups;
			groupsButton.Click += groupsButton_Click;

			_deleteAllButton = new ApplicationBarIconButton(new Uri("/Images/ApplicationBar/CharacterList/delete.png", UriKind.Relative));
			_deleteAllButton.Text = AppResources.UI_CharacterList_ApplicationBar_DeleteAll;
			_deleteAllButton.Click += DeleteAll;

			_sortByNameMenuItem = new ApplicationBarMenuItem(AppResources.UI_CharacterList_ApplicationBar_SortByName);
			_sortByNameMenuItem.Click += SortByName;

			_sortByLevelMenuItem = new ApplicationBarMenuItem(AppResources.UI_CharacterList_ApplicationBar_SortByLevel);
			_sortByLevelMenuItem.Click += SortByLevel;

			_sortByAchievementPointsMenuItem = new ApplicationBarMenuItem(AppResources.UI_CharacterList_ApplicationBar_SortByAchievementPoints);
			_sortByAchievementPointsMenuItem.Click += SortByAchievementPoints;

			ApplicationBar.Buttons.Add(searchButton);
			ApplicationBar.Buttons.Add(groupsButton);
			ApplicationBar.Buttons.Add(_deleteAllButton);
			ApplicationBar.MenuItems.Add(_sortByNameMenuItem);
			ApplicationBar.MenuItems.Add(_sortByLevelMenuItem);
			ApplicationBar.MenuItems.Add(_sortByAchievementPointsMenuItem);

			UpdateApplicationBarItems();
		}

		/// <summary>
		/// Builds the groups.
		/// </summary>
		private void BuildGroups()
		{
			while (pvGroups.Items.Count > 1)
			{
				pvGroups.Items.RemoveAt(1);
			}

			lbGroups.Items.Clear();

			foreach (var group in IsolatedStorageManager.CharacterListGroups)
			{
				var pivotItem = new PivotItem();
				pivotItem.Header = group.Value;
				pivotItem.Tag = group.Key;
				pvGroups.Items.Add(pivotItem);

				var listbox = new ListBox();
				//listbox.SelectedItem = new Binding { Source = ViewModel, Path = new PropertyPath("SelectedCharacter"), Mode = BindingMode.TwoWay };
				listbox.SelectionChanged += ListBox_SelectionChanged;
				listbox.ItemTemplate = (DataTemplate)Resources["CharacterListItemTemplate"];
				pivotItem.Content = listbox;

				lbGroups.Items.Add(group);
			}

			FillGroups();
		}

		/// <summary>
		/// Fills the groups.
		/// </summary>
		private void FillGroups()
		{
			foreach (var item in pvGroups.Items)
			{
				var pivotItem = (PivotItem)item;
				var tag = pivotItem.Tag;

				if (tag != null &&
					!tag.ToString().Equals("All", StringComparison.CurrentCultureIgnoreCase))
				{
					var listbox = (ListBox)pivotItem.Content;
					listbox.ItemsSource = ViewModel.FavoriteCharacters.Where(c => c.CharacterListGroup == new Guid(tag.ToString()));
				}
			}
		}

		/// <summary>
		/// Updates the application bar items.
		/// </summary>
		private void UpdateApplicationBarItems()
		{
			var enabled = true;
			if (ViewModel.FavoriteCharacters.Count == 0)
			{
				enabled = false;
			}

			_deleteAllButton.IsEnabled = enabled;
			_sortByNameMenuItem.IsEnabled = enabled;
			_sortByLevelMenuItem.IsEnabled = enabled;
			_sortByAchievementPointsMenuItem.IsEnabled = enabled;
		}

		/// <summary>
		/// Inverts the type the list is sorted by.
		/// </summary>
		private void InvertSortByType()
		{
			AppSettingsManager.CharacterListSortByType = AppSettingsManager.CharacterListSortByType == SortBy.Ascending ? SortBy.Descending : SortBy.Ascending;
		}

		/// <summary>
		/// Sorts the character list by name.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SortByName(object sender, EventArgs e)
		{
			if (AppSettingsManager.CharacterListSortBy == CharacterListSortBy.Name)
			{
				InvertSortByType();
			}
			else
			{
				AppSettingsManager.CharacterListSortByType = SortBy.Ascending;
			}
			AppSettingsManager.CharacterListSortBy = CharacterListSortBy.Name;
			SortCharacterList();
		}

		/// <summary>
		/// Sorts the character list by level.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SortByLevel(object sender, EventArgs e)
		{
			if (AppSettingsManager.CharacterListSortBy == CharacterListSortBy.Level)
			{
				InvertSortByType();
			}
			else
			{
				AppSettingsManager.CharacterListSortByType = SortBy.Descending;
			}
			AppSettingsManager.CharacterListSortBy = CharacterListSortBy.Level;
			SortCharacterList();
		}

		/// <summary>
		/// Sorts the character list by achievement points.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SortByAchievementPoints(object sender, EventArgs e)
		{
			if (AppSettingsManager.CharacterListSortBy == CharacterListSortBy.AchievementPoints)
			{
				InvertSortByType();
			}
			else
			{
				AppSettingsManager.CharacterListSortByType = SortBy.Descending;
			}
			AppSettingsManager.CharacterListSortBy = CharacterListSortBy.AchievementPoints;
			SortCharacterList();
		}

		/// <summary>
		/// Sorts the character list.
		/// </summary>
		private void SortCharacterList()
		{
			ViewModel.RefreshView();
			UpdateApplicationBarItems();
			FillGroups();
		}

		/// <summary>
		/// Shows the character search view.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ShowCharacterSearchView(object sender, EventArgs e)
		{
			ApplicationController.Current.NavigateTo(Enumerations.Page.CharacterSearch);
		}

		/// <summary>
		/// Handles the Loaded event of the PhoneApplicationPage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
			ViewModel.RefreshView();
			UpdateApplicationBarItems();
			BuildGroups();
		}

		/// <summary>
		/// Handles the SelectionChanged event of the ListBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (e.AddedItems != null && e.AddedItems.Count == 1)
			{
				ViewModel.SelectedCharacter = (CharacterListItem)e.AddedItems[0];
			}
			else
			{
				ViewModel.SelectedCharacter = null;
			}

			if (ViewModel.SelectedCharacter == null)
			{
				return;
			}

			ViewModel.ShowSelectedCharacter();
		}

		/// <summary>
		/// Deletes all stored characters.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void DeleteAll(object sender, EventArgs e)
		{
			if (MessageBox.Show(AppResources.UI_CharacterList_DeleteAll_Text, AppResources.UI_CharacterList_DeleteAll_Caption, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
			{
				return;
			}

			while (IsolatedStorageManager.StoredCharacters.Count > 0)
			{
				IsolatedStorageManager.StoredCharacters.Remove(IsolatedStorageManager.StoredCharacters[0]);
			}

			ViewModel.RefreshView();
			UpdateApplicationBarItems();
			FillGroups();
		}

		/// <summary>
		/// Handles the Click event of the CharacterContextMenuItemUpdate control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void CharacterContextMenuItemUpdate_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.PreventNextNavigation = true;
			var character = (CharacterListItem)((MenuItem)sender).DataContext;
			ViewModel.UpdateCharacter(character.Region, character.Realm, character.Character);
			FillGroups();
		}

		/// <summary>
		/// Handles the Click event of the CharacterContextMenuItemRemove control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void CharacterContextMenuItemRemove_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(AppResources.UI_CharacterList_Delete_Text, AppResources.UI_CharacterList_Delete_Caption, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
			{
				return;
			}

			var character = (CharacterListItem)((MenuItem)sender).DataContext;
			var storageData = IsolatedStorageManager.GetStoredCharacter(character.Region, character.Realm, character.Character);
			IsolatedStorageManager.StoredCharacters.Remove(storageData);

			ViewModel.RefreshView();
			UpdateApplicationBarItems();
			FillGroups();
		}

		/// <summary>
		/// Handles the Click event of the groupsButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void groupsButton_Click(object sender, EventArgs e)
		{
			ViewModelLocator.GroupManagementStatic.GroupType = GroupManagementType.CharacterList;
			ApplicationController.Current.NavigateTo(Page.GroupManagement);
		}

		/// <summary>
		/// Handles the Click event of the CharacterContextMenuItemRemoveFromGroup control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void CharacterContextMenuItemRemoveFromGroup_Click(object sender, RoutedEventArgs e)
		{
			var character = (CharacterListItem)((MenuItem)sender).DataContext;
			character.CharacterListGroup = Guid.Empty;
			FillGroups();
		}

		/// <summary>
		/// Handles the SelectionChanged event of the pvGroups control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void pvGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems != null &&
				e.AddedItems[0] != null &&
				((PivotItem)e.AddedItems[0]).Tag != null &&
				!((PivotItem)e.AddedItems[0]).Tag.ToString().Equals("All", StringComparison.CurrentCultureIgnoreCase))
			{
				foreach (var character in ViewModel.FavoriteCharacters)
				{
					character.IsRemoveFromGroupVisible = true;
				}
			}
			else
			{
				foreach (var character in ViewModel.FavoriteCharacters)
				{
					character.IsRemoveFromGroupVisible = false;
				}
			}
		}

		/// <summary>
		/// Handles the SelectionChanged event of the lbGroups control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void lbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (lbGroups.SelectedItem == null)
			{
				return;
			}

			var guid = ((KeyValuePair<Guid, string>)e.AddedItems[0]).Key;
			IsolatedStorageManager.StoredCharacters.Where(c => c.Guid == _character.Guid).First().CharacterListGroup = guid;
			_character = null;
			lbGroups.SelectedItem = null;
			gdGroups.Visibility = Visibility.Collapsed;
			ViewModel.RefreshView();
			BuildApplicationBar();
			FillGroups();
		}

		/// <summary>
		/// Handles the Click event of the CharacterContextMenuItemMoveToGroup control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void CharacterContextMenuItemMoveToGroup_Click(object sender, RoutedEventArgs e)
		{
			var character = (CharacterListItem)((FrameworkElement)sender).DataContext;
			_character = character;
			gdGroups.Visibility = Visibility.Visible;
			ApplicationBar = null;
		}

		/// <summary>
		/// This method is called when the hardware back key is pressed.
		/// </summary>
		/// <param name="e">Set e.Cancel to true to indicate that the request was handled by the application.</param>
		protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
		{
			base.OnBackKeyPress(e);

			if (gdGroups.Visibility == Visibility.Visible)
			{
				gdGroups.Visibility = Visibility.Collapsed;
				_character = null;
				BuildApplicationBar();
				FillGroups();
				e.Cancel = true;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}