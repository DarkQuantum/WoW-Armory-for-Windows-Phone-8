using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WowArmory.Core.Managers;
using WowArmory.Enumerations;

namespace WowArmory.ViewModels
{
	public class GroupManagementViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private string _name;
		private Dictionary<Guid, string> _groups;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the type of the group.
		/// </summary>
		/// <value>
		/// The type of the group.
		/// </value>
		public GroupManagementType GroupType { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (_name == value)
				{
					return;
				}

				_name = value;
				RaisePropertyChanged("Name");
			}
		}

		/// <summary>
		/// Gets or sets the groups.
		/// </summary>
		/// <value>
		/// The groups.
		/// </value>
		public Dictionary<Guid, string> Groups
		{
			get
			{
				return _groups;
			}
			set
			{
				if (_groups == value)
				{
					return;
				}

				_groups = value;
				RaisePropertyChanged("Groups");
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Commands ---
		//----------------------------------------------------------------------
		public RelayCommand AddCommand { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="GroupManagementViewModel"/> class.
		/// </summary>
		public GroupManagementViewModel()
		{
			InitializeCommands();
			GroupType = GroupManagementType.CharacterList;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes the commands.
		/// </summary>
		private void InitializeCommands()
		{
			AddCommand = new RelayCommand(Add);
		}

		/// <summary>
		/// Refreshes the view.
		/// </summary>
		public void RefreshView()
		{
			switch (GroupType)
			{
				case GroupManagementType.CharacterList:
					{
						Groups = IsolatedStorageManager.CharacterListGroups;
					} break;
				case GroupManagementType.GuildList:
					{
						Groups = IsolatedStorageManager.GuildListGroups;
					} break;
			}
		}

		/// <summary>
		/// Adds a new group to the list.
		/// </summary>
		public void Add()
		{
			var guid = Guid.NewGuid();

			switch (GroupType)
			{
				case GroupManagementType.CharacterList:
					{
						IsolatedStorageManager.CharacterListGroups.Add(guid, Name);
					} break;
				case GroupManagementType.GuildList:
					{
						IsolatedStorageManager.GuildListGroups.Add(guid, Name);
					} break;
			}

			Name = String.Empty;
			Groups = null;
			RefreshView();
		}

		/// <summary>
		/// Deletes all groups from the list.
		/// </summary>
		public void DeleteAll()
		{
			switch (GroupType)
			{
				case GroupManagementType.CharacterList:
					{
						IsolatedStorageManager.CharacterListGroups = new Dictionary<Guid, string>();
					} break;
				case GroupManagementType.GuildList:
					{
						IsolatedStorageManager.GuildListGroups = new Dictionary<Guid, string>();
					} break;
			}

			Groups = null;
			RefreshView();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}