using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WowArmory.Controllers;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Extensions;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.Enumerations;

namespace WowArmory.ViewModels
{
	public class GuildSearchViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private bool _isProgressBarVisible = false;
		private bool _isProgressBarIndeterminate = false;
		private bool _isLoadingProgressBarVisible = false;
		private bool _isLoadingProgressBarIndeterminate = false;
		private ObservableCollection<string> _realms;
		private string _realm;
		private string _name;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets a value indicating whether the progress bar is visible.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the progress bar is visible; otherwise, <c>false</c>.
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
		/// Gets or sets a value indicating whether the progress bar is indeterminate.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the progress bar is indeterminate; otherwise, <c>false</c>.
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
		/// Gets or sets a value indicating whether the loading progress bar is visible.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the loading progress bar is visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsLoadingProgressBarVisible
		{
			get { return _isLoadingProgressBarVisible; }
			set
			{
				if (_isLoadingProgressBarVisible == value) return;

				_isLoadingProgressBarVisible = value;
				RaisePropertyChanged("IsLoadingProgressBarVisible");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the loading progress bar is indeterminate.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the loading progress bar is indeterminate; otherwise, <c>false</c>.
		/// </value>
		public bool IsLoadingProgressBarIndeterminate
		{
			get { return _isLoadingProgressBarIndeterminate; }
			set
			{
				if (_isLoadingProgressBarIndeterminate == value) return;

				_isLoadingProgressBarIndeterminate = value;
				RaisePropertyChanged("IsLoadingProgressBarIndeterminate");
			}
		}

		/// <summary>
		/// Gets or sets the realms.
		/// </summary>
		/// <value>
		/// The realms.
		/// </value>
		public ObservableCollection<string> Realms
		{
			get
			{
				return _realms;
			}
			set
			{
				if (_realms == value)
				{
					return;
				}

				_realms = value;
				RaisePropertyChanged("Realms");
			}
		}

		/// <summary>
		/// Gets or sets the realm used in the search.
		/// </summary>
		/// <value>
		/// The realm used in the search.
		/// </value>
		public string Realm
		{
			get
			{
				return _realm;
			}
			set
			{
				if (_realm == value || value == null)
				{
					return;
				}

				_realm = value.Trim();
				RaisePropertyChanged("Realm");
			}
		}

		/// <summary>
		/// Gets or sets the name used in the search.
		/// </summary>
		/// <value>
		/// The name used in the search.
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

				_name = value.Trim();
				RaisePropertyChanged("Name");
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Commands ---
		//----------------------------------------------------------------------
		public RelayCommand SearchGuildCommand { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="GuildSearchViewModel"/> class.
		/// </summary>
		public GuildSearchViewModel()
		{
			InitializeCommands();

			Realm = IsolatedStorageManager.GetValue("GuildSearch_LastRealm", String.Empty);
			Name = IsolatedStorageManager.GetValue("GuildSearch_LastName", String.Empty);
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
			SearchGuildCommand = new RelayCommand(SearchGuild);
		}

		/// <summary>
		/// Loads the realms.
		/// </summary>
		public void LoadRealms()
		{
			if (Realms != null && Realms.Count != 0)
			{
				return;
			}

			IsLoadingProgressBarVisible = true;
			IsLoadingProgressBarIndeterminate = true;

			var region = AppSettingsManager.Region;
			BattleNetClient.Current.Region = region;

			if (!CacheManager.CachedRealmLists.ContainsKey(region))
			{
				Realms = new ObservableCollection<string>();
				BattleNetClient.Current.GetRealmListAsync(region, realmList =>
				                                                  	{
				                                                  		CacheManager.CachedRealmLists[region] = realmList;
				                                                  		ConstructBindableRealmList(CacheManager.CachedRealmLists[region]);
				                                                  	});
			}
			else
			{
				ConstructBindableRealmList(CacheManager.CachedRealmLists[region]);
			}
		}

		/// <summary>
		/// Constructs the bindable realm list.
		/// </summary>
		/// <param name="realmList">The realm list.</param>
		private void ConstructBindableRealmList(RealmList realmList)
		{
			if (realmList == null)
			{
				MessageBox.Show(AppResources.UI_Common_Error_NoData_Text, AppResources.UI_Common_Error_NoData_Caption, MessageBoxButton.OK);
				ApplicationController.Current.NavigateBack();
				return;
			}

			Realms = realmList.Realms.Select(realm => realm.Name).ToObservableCollection();

			IsLoadingProgressBarVisible = false;
			IsLoadingProgressBarIndeterminate = false;
		}

		/// <summary>
		/// Searches for the specified guild.
		/// </summary>
		public void SearchGuild()
		{
			IsolatedStorageManager.SetValue("GuildSearch_LastRealm", Realm);
			IsolatedStorageManager.SetValue("GuildSearch_LastName", Name);

			BattleNetClient.Current.Region = AppSettingsManager.Region;

			if (String.IsNullOrEmpty(Realm))
			{
				MessageBox.Show(AppResources.UI_Search_MissingRealm_Text, AppResources.UI_Search_Missing_Caption, MessageBoxButton.OK);
				return;
			}

			if (String.IsNullOrEmpty(Name))
			{
				MessageBox.Show(AppResources.UI_Search_MissingGuildName_Text, AppResources.UI_Search_Missing_Caption, MessageBoxButton.OK);
				return;
			}

			IsProgressBarIndeterminate = true;
			IsProgressBarVisible = true;

			BattleNetClient.Current.GetGuildAsync(Realm, Name, GuildFields.All, OnGuildRetrievedFromArmory);
		}

		/// <summary>
		/// Called when the guild was retrieved from the armory.
		/// </summary>
		/// <param name="guild">The guild retrieved from the armory.</param>
		private void OnGuildRetrievedFromArmory(Guild guild)
		{
			IsProgressBarVisible = false;
			IsProgressBarIndeterminate = false;

			if (guild == null)
			{
				MessageBox.Show(AppResources.UI_Common_Error_NoData_Text, AppResources.UI_Common_Error_NoData_Caption, MessageBoxButton.OK);
				return;
			}

			if (!guild.IsValid)
			{
				var reasonCaption = AppResources.ResourceManager.GetString(String.Format("UI_Search_Error_{0}_Caption", guild.ReasonType)) ?? AppResources.UI_Common_Error_NoData_Caption;
				var reasonText = AppResources.ResourceManager.GetString(String.Format("UI_Search_Error_{0}_Text", guild.ReasonType)) ?? AppResources.UI_Common_Error_NoData_Text;
				MessageBox.Show(reasonText, reasonCaption, MessageBoxButton.OK);
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
