using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WowArmory.Controllers;
using WowArmory.Core.Languages;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class CharacterListPage : PhoneApplicationPage
	{
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

			ApplicationBar.Buttons.Add(searchButton);
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