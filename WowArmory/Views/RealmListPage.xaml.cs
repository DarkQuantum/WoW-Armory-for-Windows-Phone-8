using System.Linq;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using WowArmory.Core.BattleNet.Models;

namespace WowArmory.Views
{
	public partial class RealmListPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public RealmListPage()
		{
			InitializeComponent();
			InitializeRealmsJumpList();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
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

		private void OnJumpList_GroupPickerItemTap(object sender, GroupPickerItemTapEventArgs e)
		{
			foreach (var group in RealmsJumpList.Groups)
			{
				if (!object.Equals(e.DataItem, group.Key)) continue;

				e.DataItemToNavigate = group;
				return;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}