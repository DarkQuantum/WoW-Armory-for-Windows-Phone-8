using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WowArmory.Controllers;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Extensions;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.Enumerations;
using WowArmory.Models;

namespace WowArmory.ViewModels
{
	public class GuildDetailsViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private bool _isProgressBarVisible = false;
		private bool _isProgressBarIndeterminate = false;
		private Guild _guild;
		private ObservableCollection<GuildMemberItem> _members;
		private GuildMemberItem _selectedMember;
		private int _dataToLoad = 0;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Events ---
		//----------------------------------------------------------------------
		public delegate void GuildLoadedEventHandler();
		public event GuildLoadedEventHandler OnGuildLoaded;
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
		/// Gets or sets the guild.
		/// </summary>
		/// <value>
		/// The character.
		/// </value>
		public Guild Guild
		{
			get { return _guild; }
			set
			{
				if (_guild == value) return;

				_guild = value;
				Members = null;
				RaisePropertyChanged("Guild");
			}
		}

		/// <summary>
		/// Gets or sets the members of the guild.
		/// </summary>
		/// <value>
		/// The members of the guild.
		/// </value>
		public ObservableCollection<GuildMemberItem> Members
		{
			get
			{
				return _members;
			}
			set
			{
				if (_members == value)
				{
					return;
				}

				_members = value;
				RaisePropertyChanged("Members");
			}
		}

		/// <summary>
		/// Gets or sets the selected member.
		/// </summary>
		/// <value>
		/// The selected member.
		/// </value>
		public GuildMemberItem SelectedMember
		{
			get
			{
				return _selectedMember;
			}
			set
			{
				if (_selectedMember == value)
				{
					return;
				}

				_selectedMember = value;
				RaisePropertyChanged("SelectedMember");
			}
		}

		/// <summary>
		/// Gets the guild faction.
		/// </summary>
		public int GuildFaction
		{
			get
			{
				return (int)Guild.Side;
			}
		}

		/// <summary>
		/// Gets the favorite image source used in the view.
		/// </summary>
		public ImageSource FavoriteImage
		{
			get
			{
				var isGuildStored = IsolatedStorageManager.IsGuildStored(Guild.Region, Guild.Realm, Guild.Name);
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/GuildDetails/Favorite_{0}.png", isGuildStored ? 1 : 0));
			}
		}

		/// <summary>
		/// Gets the member count.
		/// </summary>
		public string MemberCount
		{
			get
			{
				return String.Format(AppResources.UI_GuildDetails_Header_Members, Guild.Members != null ? Guild.Members.Count : 0);
			}
		}

		/// <summary>
		/// Gets the level.
		/// </summary>
		public string Level
		{
			get
			{
				return String.Format(AppResources.UI_GuildDetails_Header_Level, Guild.Level);
			}
		}

		/// <summary>
		/// Gets the faction.
		/// </summary>
		public string Faction
		{
			get
			{
				return AppResources.ResourceManager.GetString(String.Format("UI_GuildDetails_Header_Faction_{0}", Guild.Side));
			}
		}

		/// <summary>
		/// Gets the faction image.
		/// </summary>
		public ImageSource FactionImage
		{
			get
			{
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/Icons/Factions/{0}.png", (int)Guild.Side));
			}
		}

		/// <summary>
		/// Gets the name of the region string.
		/// </summary>
		public string RegionString
		{
			get
			{
				return AppResources.ResourceManager.GetString(String.Format("BattleNet_Region_{0}", Guild.Region));
			}
		}

		/// <summary>
		/// Gets the emblem default image.
		/// </summary>
		public ImageSource EmblemDefaultImage
		{
			get
			{
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/GuildDetails/Emblem_Default_{0}.png", Guild.Side));
			}
		}

		/// <summary>
		/// Gets the guild emblem image.
		/// </summary>
		public ImageSource GuildEmblemImage
		{
			get
			{
				return CacheManager.GetGuildEmblemImage(Guild.Region, Guild.Realm, Guild.Name, Guild.Emblem, Guild.Side);
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Commands ---
		//----------------------------------------------------------------------
		public RelayCommand ToggleGuildFavoriteCommand { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="GuildDetailsViewModel"/> class.
		/// </summary>
		public GuildDetailsViewModel()
		{
			InitializeCommands();

			Guild = null;
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
			ToggleGuildFavoriteCommand = new RelayCommand(ToggleGuildFavorite);
		}

		public void RefreshView()
		{
			//Deployment.Current.Dispatcher.BeginInvoke(BuildMembersList);

			_dataToLoad = 2;
			BuildMembersList();
			BuildGuildPerks();
		}

		/// <summary>
		/// Addes or removes the current guild from the favorites.
		/// </summary>
		public void ToggleGuildFavorite()
		{
			RaisePropertyChanged("FavoriteImage");
		}

		/// <summary>
		/// Builds the members list.
		/// </summary>
		private void BuildMembersList()
		{
			IsProgressBarIndeterminate = true;
			IsProgressBarVisible = true;
			Deployment.Current.Dispatcher.BeginInvoke(() =>
			{
				Members = Guild.Members.Select(m => new GuildMemberItem(m) { Region = Guild.Region }).ToObservableCollection();
				_dataToLoad--;
				if (_dataToLoad > 0)
				{
					return;
				}
				IsProgressBarIndeterminate = false;
				IsProgressBarVisible = false;
				if (OnGuildLoaded != null)
				{
					OnGuildLoaded();
				}
			});
		}

		private void BuildGuildPerks()
		{
			if (CacheManager.CachedGuildPerks == null)
			{
				IsProgressBarIndeterminate = true;
				IsProgressBarVisible = true;

				BattleNetClient.Current.GetGuildPerksAsync(OnGuildPerksRetrievedFromArmory);
			}
			else
			{
				_dataToLoad--;
				if (_dataToLoad > 0)
				{
					return;
				}
				IsProgressBarIndeterminate = false;
				IsProgressBarVisible = false;
				if (OnGuildLoaded != null)
				{
					OnGuildLoaded();
				}
			}
		}

		/// <summary>
		/// Called when the guild perks list was retrieved from the armory.
		/// </summary>
		/// <param name="perks">The guild perks list retrieved from the armory.</param>
		private void OnGuildPerksRetrievedFromArmory(GuildPerks perks)
		{
			CacheManager.CachedGuildPerks = perks;
			_dataToLoad--;
			if (_dataToLoad > 0)
			{
				return;
			}
			IsProgressBarIndeterminate = false;
			IsProgressBarVisible = false;
			if (OnGuildLoaded != null)
			{
				OnGuildLoaded();
			}
		}

		/// <summary>
		/// Shows the selected member.
		/// </summary>
		public void ShowSelectedMember()
		{
			BattleNetClient.Current.Region = Guild.Region;
			RetrieveCharacterFromArmory(Guild.Realm, SelectedMember.Character.Name);
		}

		/// <summary>
		/// Retrieves the character from armory.
		/// </summary>
		/// <param name="realmName">Name of the realm.</param>
		/// <param name="characterName">Name of the character.</param>
		private void RetrieveCharacterFromArmory(string realmName, string characterName)
		{
			IsProgressBarIndeterminate = true;
			IsProgressBarVisible = true;

			BattleNetClient.Current.GetCharacterAsync(realmName, characterName, CharacterFields.All, OnCharacterRetrievedFromArmory);
		}

		/// <summary>
		/// Called when the character was retrieved from the armory.
		/// </summary>
		/// <param name="character">The character retrieved from the armory.</param>
		private void OnCharacterRetrievedFromArmory(Character character)
		{
			// reset selected character so we can click it again in case the result wasn't valid
			SelectedMember = null;

			IsProgressBarVisible = false;
			IsProgressBarIndeterminate = false;

			if (character == null)
			{
				return;
			}

			ViewModelLocator.CharacterDetailsStatic.Character = character;
			ApplicationController.Current.NavigateTo(Page.CharacterDetails);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
