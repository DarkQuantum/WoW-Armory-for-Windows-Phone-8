using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class GroupManagementPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private ApplicationBarIconButton _deleteButton;
		private ApplicationBarMenuItem _deleteAllMenuItem;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the view model associated to this page.
		/// </summary>
		public GroupManagementViewModel ViewModel
		{
			get
			{
				return (GroupManagementViewModel)DataContext;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public GroupManagementPage()
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

			var addButton = new ApplicationBarIconButton(new Uri("/Images/ApplicationBar/GroupManagement/add.png", UriKind.Relative));
			addButton.Text = AppResources.UI_GroupManagement_ApplicationBar_Add;
			addButton.Click += Add;

			_deleteButton = new ApplicationBarIconButton(new Uri("/Images/ApplicationBar/GroupManagement/delete.png", UriKind.Relative));
			_deleteButton.Text = AppResources.UI_GroupManagement_ApplicationBar_Delete;
			_deleteButton.Click += Delete;

			_deleteAllMenuItem = new ApplicationBarMenuItem();
			_deleteAllMenuItem.Text = AppResources.UI_GroupManagement_ApplicationBar_DeleteAll;
			_deleteAllMenuItem.Click += DeleteAll;

			ApplicationBar.Buttons.Add(addButton);
			ApplicationBar.Buttons.Add(_deleteButton);
			ApplicationBar.MenuItems.Add(_deleteAllMenuItem);

			UpdateApplicationBarItems();
		}

		/// <summary>
		/// Removes the application bar.
		/// </summary>
		private void RemoveApplicationBar()
		{
			ApplicationBar = null;
		}

		/// <summary>
		/// Updates the application bar items.
		/// </summary>
		private void UpdateApplicationBarItems()
		{
			if (ViewModel.Groups != null && ViewModel.Groups.Count > 0)
			{
				_deleteAllMenuItem.IsEnabled = true;
			}
			else
			{
				_deleteAllMenuItem.IsEnabled = false;
			}

			if (lbGroups.SelectedItems != null && lbGroups.SelectedItems.Count > 0)
			{
				_deleteButton.IsEnabled = true;
			}
			else
			{
				_deleteButton.IsEnabled = false;
			}
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
		/// Handles the TextChanged event of the txtName control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs"/> instance containing the event data.</param>
		private void txtName_TextChanged(object sender, TextChangedEventArgs e)
		{
			var text = txtName.Text.Trim().ToLower();

			if (!String.IsNullOrEmpty(text) && text.Length >= 3 && text.Length <= 20 &&
				IsolatedStorageManager.CharacterListGroups.Where(g => g.Value.Equals(text, StringComparison.CurrentCultureIgnoreCase)).Count() == 0)
			{
				btnAdd.IsEnabled = true;
			}
			else
			{
				btnAdd.IsEnabled = false;
			}
		}

		/// <summary>
		/// Handles the KeyUp event of the txtName control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
		private void txtName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Enter && btnAdd.IsEnabled)
			{
				btnAdd_Click(sender, new RoutedEventArgs());
			}
		}

		/// <summary>
		/// Adds the specified name to the group list
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void Add(object sender, EventArgs eventArgs)
		{
			RemoveApplicationBar();
			gdAdd.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Deletes the selected groups.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void Delete(object sender, EventArgs e)
		{
			if (MessageBox.Show(AppResources.UI_GroupManagement_Delete_Text, AppResources.UI_GroupManagement_Delete_Caption, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
			{
				return;
			}

			foreach (var item in lbGroups.SelectedItems)
			{
				var group = (KeyValuePair<Guid, string>)item;
				ViewModel.Groups.Remove(group.Key);
			}

			ViewModel.Groups = null;
			ViewModel.RefreshView();
			UpdateApplicationBarItems();
		}

		/// <summary>
		/// Deletes all groups.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void DeleteAll(object sender, EventArgs e)
		{
			if (MessageBox.Show(AppResources.UI_GroupManagement_DeleteAll_Text, AppResources.UI_GroupManagement_DeleteAll_Caption, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
			{
				return;
			}

			ViewModel.DeleteAll();
			UpdateApplicationBarItems();
		}

		/// <summary>
		/// Handles the SelectionChanged event of the lbGroups control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void lbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateApplicationBarItems();
		}

		/// <summary>
		/// Handles the Click event of the btnAdd control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.Name = txtName.Text.Trim().ToLower();
			ViewModel.Add();
			BuildApplicationBar();
			gdAdd.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// This method is called when the hardware back key is pressed.
		/// </summary>
		/// <param name="e">Set e.Cancel to true to indicate that the request was handled by the application.</param>
		protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
		{
			base.OnBackKeyPress(e);

			if (gdAdd.Visibility == Visibility.Visible)
			{
				BuildApplicationBar();
				gdAdd.Visibility = Visibility.Collapsed;
				e.Cancel = true;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}