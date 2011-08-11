using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.JumpList;

namespace WowArmory.Selectors
{
	public class RealmJumpListTemplateSelector : DataTemplateSelector
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private DataTemplate _linkedItemTemplate;
		private DataTemplate _emptyItemTemplate;
		private RadJumpList _jumpList;
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public DataTemplate LinkedItemTemplate
		{
			get { return _linkedItemTemplate; }
			set { _linkedItemTemplate = value; }
		}

		public DataTemplate EmptyItemTemplate
		{
			get { return _emptyItemTemplate; }
			set { _emptyItemTemplate = value; }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (_jumpList == null)
			{
				var picker = ElementTreeHelper.FindVisualAncestor<JumpListGroupPicker>(container);
				if (picker != null)
				{
					_jumpList = picker.Owner;
				}
			}

			if (_jumpList == null)
			{
				return base.SelectTemplate(item, container);
			}

			return IsLinkedItem(item) ? _linkedItemTemplate : _emptyItemTemplate;
		}

		private bool IsLinkedItem(object item)
		{
			return _jumpList.Groups.Any(group => object.Equals(group.Key, item));
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}