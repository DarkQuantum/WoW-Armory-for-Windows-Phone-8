using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WowArmory.Controllers;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Extensions;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.Core.Storage;
using WowArmory.Enumerations;
using WowArmory.Models;

namespace WowArmory.ViewModels
{
	public class CharacterListViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private bool _isProgressBarVisible = false;
		private bool _isProgressBarIndeterminate = false;
		private ObservableCollection<CharacterListItem> _favoriteCharacters;
		private CharacterListItem _selectedCharacter;
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
		/// Gets the favorite characters.
		/// </summary>
		public ObservableCollection<CharacterListItem> FavoriteCharacters
		{
			get
			{
				return _favoriteCharacters;
			}
			set
			{
				if (_favoriteCharacters == value)
				{
					return;
				}

				_favoriteCharacters = value;
				RaisePropertyChanged("FavoriteCharacters");
			}
		}

		/// <summary>
		/// Gets or sets the selected character.
		/// </summary>
		/// <value>
		/// The selected character.
		/// </value>
		public CharacterListItem SelectedCharacter
		{
			get
			{
				return _selectedCharacter;
			}
			set
			{
				if (_selectedCharacter == value)
				{
					return;
				}

				_selectedCharacter = value;
				RaisePropertyChanged("SelectedCharacter");
			}
		}
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
		/// Initializes a new instance of the <see cref="CharacterListViewModel"/> class.
		/// </summary>
		public CharacterListViewModel()
		{
			InitializeCommands();
			RefreshView();
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
			var characters = IsolatedStorageManager.StoredCharacters;

			switch (AppSettingsManager.CharacterListOrderBy)
			{
				case CharacterListOrderBy.Level:
					{
						FavoriteCharacters = characters.OrderBy(c => c.Level).Select(c => new CharacterListItem(c)).ToObservableCollection();
					} break;
				default:
					{
						FavoriteCharacters = characters.OrderBy(c => c.Character).Select(c => new CharacterListItem(c)).ToObservableCollection();
					} break;
			}

			SelectedCharacter = null;
		}

		public void ShowSelectedCharacter()
		{
			BattleNetClient.Current.Region = SelectedCharacter.Region;
			RetrieveCharacterFromArmory(SelectedCharacter.Realm, SelectedCharacter.Character);
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
			SelectedCharacter = null;

			IsProgressBarVisible = false;
			IsProgressBarIndeterminate = false;

			if (character == null)
			{
				MessageBox.Show(AppResources.UI_Common_Error_NoData_Text, AppResources.UI_Common_Error_NoData_Caption, MessageBoxButton.OK);
				return;
			}

			if (!character.IsValid)
			{
				var reasonCaption = AppResources.ResourceManager.GetString(String.Format("UI_Search_Error_{0}_Caption", character.ReasonType)) ?? AppResources.UI_Common_Error_NoData_Caption;
				var reasonText = AppResources.ResourceManager.GetString(String.Format("UI_Search_Error_{0}_Text", character.ReasonType)) ?? AppResources.UI_Common_Error_NoData_Text;
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