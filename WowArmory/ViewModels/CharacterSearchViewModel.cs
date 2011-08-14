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
using WowArmory.Models;

namespace WowArmory.ViewModels
{
	public class CharacterSearchViewModel : ViewModelBase
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

				_realm = value;
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

				_name = value;
				RaisePropertyChanged("Name");
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Commands ---
		//----------------------------------------------------------------------
		public RelayCommand SearchCharacterCommand { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterSearchViewModel"/> class.
		/// </summary>
		public CharacterSearchViewModel()
		{
			InitializeCommands();

			Realm = IsolatedStorageManager.GetValue("CharacterSearch_LastRealm", String.Empty);
			Name = IsolatedStorageManager.GetValue("CharacterSearch_LastName", String.Empty);
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
			SearchCharacterCommand = new RelayCommand(SearchCharacter);
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
		/// Searches for the specified character.
		/// </summary>
		public void SearchCharacter()
		{
			IsolatedStorageManager.SetValue("CharacterSearch_LastRealm", Realm);
			IsolatedStorageManager.SetValue("CharacterSearch_LastName", Name);

			BattleNetClient.Current.Region = AppSettingsManager.Region;

			if (String.IsNullOrEmpty(Realm))
			{
				MessageBox.Show(AppResources.UI_CharacterSearch_MissingRealm_Text, AppResources.UI_CharacterSearch_Missing_Caption, MessageBoxButton.OK);
				return;
			}

			if (String.IsNullOrEmpty(Name))
			{
				MessageBox.Show(AppResources.UI_CharacterSearch_MissingName_Text, AppResources.UI_CharacterSearch_Missing_Caption, MessageBoxButton.OK);
				return;
			}

			IsProgressBarIndeterminate = true;
			IsProgressBarVisible = true;

			BattleNetClient.Current.GetCharacterAsync(Realm, Name, CharacterFields.All, OnCharacterRetrievedFromArmory);
		}

		/// <summary>
		/// Called when the character was retrieved from the armory.
		/// </summary>
		/// <param name="character">The character retrieved from the armory.</param>
		private void OnCharacterRetrievedFromArmory(Character character)
		{
			IsProgressBarVisible = false;
			IsProgressBarIndeterminate = false;

			if (character == null)
			{
				MessageBox.Show(AppResources.UI_Common_Error_NoData_Text, AppResources.UI_Common_Error_NoData_Caption, MessageBoxButton.OK);
				return;
			}

			if (!character.IsValid)
			{
				var reasonCaption = AppResources.ResourceManager.GetString(String.Format("UI_CharacterSearch_Error_{0}_Caption", character.ReasonType)) ?? AppResources.UI_CharacterSearch_Error_Unknown_Caption;
				var reasonText = AppResources.ResourceManager.GetString(String.Format("UI_CharacterSearch_Error_{0}_Text", character.ReasonType)) ?? AppResources.UI_CharacterSearch_Error_Unknown_Text;
				MessageBox.Show(reasonText, reasonCaption, MessageBoxButton.OK);
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