using System;
using System.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Languages;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class RealmListPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the view model associated to this page.
		/// </summary>
		public RealmListViewModel ViewModel
		{
			get
			{
				return (RealmListViewModel)DataContext;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="RealmListPage"/> class.
		/// </summary>
		public RealmListPage()
		{
			BuildApplicationBar();
			InitializeComponent();
			InitializeRealmsJumpList();
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

			var refreshButton = new ApplicationBarIconButton(new Uri("/Images/ApplicationBar/RealmList/refresh.png", UriKind.Relative));
			refreshButton.Text = AppResources.UI_RealmList_ApplicationBar_Refresh;
			refreshButton.Click += RefreshRealmList;
			ApplicationBar.Buttons.Add(refreshButton);
		}

		/// <summary>
		/// Refreshes the realm list.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void RefreshRealmList(object sender, EventArgs e)
		{
			ViewModel.LoadRealms(true);
		}

		/// <summary>
		/// Initializes the realms jump list.
		/// </summary>
		private void InitializeRealmsJumpList()
		{
			var alphabet = "abcdefghijklmnopqrstuvwxyz";

			var groupPickerItems = alphabet.Select(character => character.ToString()).ToList();
			RealmsJumpList.GroupPickerItemsSource = groupPickerItems;

			RealmsJumpList.GroupPickerItemTap += OnJumpList_GroupPickerItemTap;

			var groupDescriptor = new GenericGroupDescriptor<Realm, string>(r => r.Name.Substring(0, 1).ToLower());
			RealmsJumpList.GroupDescriptors.Add(groupDescriptor);

			var sortDescriptor = new GenericSortDescriptor<Realm, string>(r => r.Name);
			RealmsJumpList.SortDescriptors.Add(sortDescriptor);
		}

		/// <summary>
		/// Called when the jump list group picker item is tapped.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="Telerik.Windows.Controls.GroupPickerItemTapEventArgs"/> instance containing the event data.</param>
		private void OnJumpList_GroupPickerItemTap(object sender, GroupPickerItemTapEventArgs e)
		{
			foreach (var group in RealmsJumpList.Groups)
			{
				if (!object.Equals(e.DataItem, group.Key)) continue;

				e.DataItemToNavigate = group;
				return;
			}
		}

		/// <summary>
		/// Handles the Loaded event of the PhoneApplicationPage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void PhoneApplicationPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel.LoadRealms();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}