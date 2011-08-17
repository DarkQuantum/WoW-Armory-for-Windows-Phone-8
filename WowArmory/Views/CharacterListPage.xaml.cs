using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WowArmory.Controllers;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class CharacterListPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private ApplicationBarMenuItem _sortByNameMenuItem;
		private ApplicationBarMenuItem _sortByLevelMenuItem;
		private ApplicationBarMenuItem _sortByAchievementPointsMenuItem;
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

			_sortByNameMenuItem = new ApplicationBarMenuItem(AppResources.UI_CharacterList_ApplicationBar_SortByName);
			_sortByNameMenuItem.Click += SortByName;

			_sortByLevelMenuItem = new ApplicationBarMenuItem(AppResources.UI_CharacterList_ApplicationBar_SortByLevel);
			_sortByLevelMenuItem.Click += SortByLevel;

			_sortByAchievementPointsMenuItem = new ApplicationBarMenuItem(AppResources.UI_CharacterList_ApplicationBar_SortByAchievementPoints);
			_sortByAchievementPointsMenuItem.Click += SortByAchievementPoints;

			ApplicationBar.Buttons.Add(searchButton);
			ApplicationBar.MenuItems.Add(_sortByNameMenuItem);
			ApplicationBar.MenuItems.Add(_sortByLevelMenuItem);
			ApplicationBar.MenuItems.Add(_sortByAchievementPointsMenuItem);

			UpdateApplicationBarItems();
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
		}

		/// <summary>
		/// Handles the SelectionChanged event of the ListBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (ViewModel.SelectedCharacter == null)
			{
				return;
			}

			ViewModel.ShowSelectedCharacter();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}