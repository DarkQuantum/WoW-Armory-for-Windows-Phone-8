using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using WowArmory.Controllers;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Extensions;
using WowArmory.Core.Managers;
using WowArmory.Enumerations;
using WowArmory.Models;

namespace WowArmory.ViewModels
{
	public class GuildListViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private bool _isProgressBarVisible = false;
		private bool _isProgressBarIndeterminate = false;
		private ObservableCollection<GuildListItem> _favoriteGuilds;
		private GuildListItem _selectedGuild;
		private Region _previousRegion = BattleNetClient.Current.Region;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets a value indicating whether the progress bar visible.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the progress bar visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsProgressBarVisible
		{
			get { return _isProgressBarVisible; }
			set
			{
				if (_isProgressBarVisible == value) return;

				_isProgressBarVisible = value;
				RaisePropertyChanged("IsProgressBarVisible");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the progress bar indeterminate.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the progress bar indeterminate; otherwise, <c>false</c>.
		/// </value>
		public bool IsProgressBarIndeterminate
		{
			get { return _isProgressBarIndeterminate; }
			set
			{
				if (_isProgressBarIndeterminate == value) return;

				_isProgressBarIndeterminate = value;
				RaisePropertyChanged("IsProgressBarIndeterminate");
			}
		}

		/// <summary>
		/// Gets the favorite guilds.
		/// </summary>
		public ObservableCollection<GuildListItem> FavoriteGuilds
		{
			get
			{
				return _favoriteGuilds;
			}
			set
			{
				if (_favoriteGuilds == value)
				{
					return;
				}

				_favoriteGuilds = value;
				RaisePropertyChanged("FavoriteGuilds");
			}
		}

		/// <summary>
		/// Gets or sets the selected guild.
		/// </summary>
		/// <value>
		/// The selected guild.
		/// </value>
		public GuildListItem SelectedGuild
		{
			get
			{
				return _selectedGuild;
			}
			set
			{
				if (_selectedGuild == value)
				{
					return;
				}

				_selectedGuild = value;
				RaisePropertyChanged("SelectedGuild");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the next application controller navigation should be prevented.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the next application controller navigation should be prevented; otherwise, <c>false</c>.
		/// </value>
		public bool PreventNextNavigation { get; set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Commands ---
		//----------------------------------------------------------------------
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="GuildListViewModel"/> class.
		/// </summary>
		public GuildListViewModel()
		{
			InitializeCommands();
			RefreshView();
			PreventNextNavigation = false;
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
		}

		/// <summary>
		/// Refreshes the view.
		/// </summary>
		public void RefreshView()
		{
			var guilds = IsolatedStorageManager.StoredGuilds;

			switch (AppSettingsManager.GuildListSortBy)
			{
				case GuildListSortBy.Level:
					{
						if (AppSettingsManager.GuildListSortByType == SortBy.Ascending)
						{
							FavoriteGuilds = guilds.OrderBy(g => g.Level).Select(g => new GuildListItem(g)).ToObservableCollection();
						}
						else
						{
							FavoriteGuilds = guilds.OrderByDescending(g => g.Level).Select(g => new GuildListItem(g)).ToObservableCollection();
						}
					} break;
				case GuildListSortBy.Members:
					{
						if (AppSettingsManager.GuildListSortByType == SortBy.Ascending)
						{
							FavoriteGuilds = guilds.OrderBy(g => g.Members.Count).Select(g => new GuildListItem(g)).ToObservableCollection();
						}
						else
						{
							FavoriteGuilds = guilds.OrderByDescending(g => g.Members.Count).Select(g => new GuildListItem(g)).ToObservableCollection();
						}
					} break;
				default:
					{
						if (AppSettingsManager.GuildListSortByType == SortBy.Ascending)
						{
							FavoriteGuilds = guilds.OrderBy(g => g.Name).Select(g => new GuildListItem(g)).ToObservableCollection();
						}
						else
						{
							FavoriteGuilds = guilds.OrderByDescending(g => g.Name).Select(g => new GuildListItem(g)).ToObservableCollection();
						}
					} break;
			}

			SelectedGuild = null;
		}

		/// <summary>
		/// Shows the selected guild.
		/// </summary>
		public void ShowSelectedGuild()
		{
			BattleNetClient.Current.Region = SelectedGuild.Region;
			RetrieveGuildFromArmory(SelectedGuild.Realm, SelectedGuild.Name);
		}

		/// <summary>
		/// Updates the guild.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="realmName">Name of the realm.</param>
		/// <param name="guildName">Name of the guild.</param>
		public void UpdateGuild(Region region, string realmName, string guildName)
		{
			if (!IsolatedStorageManager.IsGuildStored(region, realmName, guildName))
			{
				return;
			}

			PreventNextNavigation = true;
			BattleNetClient.Current.Region = region;
			RetrieveGuildFromArmory(realmName, guildName);
		}

		/// <summary>
		/// Retrieves the guild from armory.
		/// </summary>
		/// <param name="realmName">Name of the realm.</param>
		/// <param name="guildName">Name of the guild.</param>
		private void RetrieveGuildFromArmory(string realmName, string guildName)
		{
			IsProgressBarIndeterminate = true;
			IsProgressBarVisible = true;

			BattleNetClient.Current.GetGuildAsync(realmName, guildName, GuildFields.All, OnGuildRetrievedFromArmory);
		}

		/// <summary>
		/// Called when the guild was retrieved from the armory.
		/// </summary>
		/// <param name="guild">The guild retrieved from the armory.</param>
		private void OnGuildRetrievedFromArmory(Guild guild)
		{
			SelectedGuild = null;

			IsProgressBarIndeterminate = false;
			IsProgressBarVisible = false;

			if (guild == null)
			{
				return;
			}

			if (PreventNextNavigation)
			{
				PreventNextNavigation = false;
				return;
			}

			ViewModelLocator.GuildDetailsStatic.Guild = guild;
			ApplicationController.Current.NavigateTo(Page.GuildDetails);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
