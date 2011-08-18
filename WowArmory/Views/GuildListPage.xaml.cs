using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WowArmory.Controllers;
using WowArmory.Core.Languages;

namespace WowArmory.Views
{
	public partial class GuildListPage : PhoneApplicationPage
	{
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

			ApplicationBar.Buttons.Add(searchButton);
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
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}