using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WowArmory.Controllers;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Languages;
using WowArmory.Enumerations;

namespace WowArmory.ViewModels
{
	public class CharacterListViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private bool _isProgressBarVisible = false;
		private bool _isProgressBarIndeterminate = false;
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
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Commands ---
		//----------------------------------------------------------------------
		public RelayCommand ShowTestCharacterCommand { get; private set; }
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
			ShowTestCharacterCommand = new RelayCommand(ShowTestCharacter);
		}

		/// <summary>
		/// Shows the test character.
		/// </summary>
		private void ShowTestCharacter()
		{
			RetrieveCharacterFromArmory("Lordaeron", "Timothy");
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
			IsProgressBarVisible = false;
			IsProgressBarIndeterminate = false;

			if (!character.IsValid)
			{
				var reasonCaption = AppResources.ResourceManager.GetString(String.Format("UI_CharacterList_Search_Error_{0}_Caption", character.ReasonType)) ?? AppResources.UI_CharacterList_Search_Error_Unknown_Caption;
				var reasonText = AppResources.ResourceManager.GetString(String.Format("UI_CharacterList_Search_Error_{0}_Text", character.ReasonType)) ?? AppResources.UI_CharacterList_Search_Error_Unknown_Text;
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