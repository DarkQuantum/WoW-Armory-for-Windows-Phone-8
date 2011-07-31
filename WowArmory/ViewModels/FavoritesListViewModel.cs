using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using GalaSoft.MvvmLight;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Models;
using WowArmory.Core.Pages;
using WowArmory.Core.Storage;


namespace WowArmory.ViewModels
{
	public class FavoritesListViewModel : ViewModelBase
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private ArmoryCharacter _selectedCharacter;
		private ObservableCollection<ArmoryCharacter> _characters;

		private bool _isDataLoaded = false;
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public ArmoryCharacter SelectedCharacter
		{
			get { return _selectedCharacter; }
			set
			{
				if ( _selectedCharacter == value ) return;

				_selectedCharacter = value;
				RaisePropertyChanged( "SelectedCharacter" );
			}
		}

		public ObservableCollection<ArmoryCharacter> Characters
		{
			get { return _characters; }
			set
			{
				if ( _characters == value ) return;

				_characters = value;
				RaisePropertyChanged( "Characters" );
			}
		}

		public bool IsDataLoaded
		{
			get { return _isDataLoaded; }
			set
			{
				if ( _isDataLoaded == value ) return;

				_isDataLoaded = value;
				RaisePropertyChanged( "IsDataLoaded" );
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public FavoritesListViewModel()
		{
			_characters = new ObservableCollection<ArmoryCharacter>();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Creates and adds a few ItemViewModel objects into the Items collection.
		/// </summary>
		public void LoadData()
		{
			_characters = StorageManager.GetStoredCharacters();
			OrderCharacters();
			IsDataLoaded = true;
		}

		private void OrderCharacters()
		{
			if ( StorageManager.Settings != null && StorageManager.Settings.Count > 0 && StorageManager.Settings.ContainsKey( "favoritesSortBy" ) )
			{
				switch ( StorageManager.Settings[ "favoritesSortBy" ].ToString() )
				{
					case "Level":
						{
							Characters = Characters.OrderByDescending( c => ( c.CharacterSheetPage != null && c.CharacterSheetPage.CharacterInfo != null ) ? c.CharacterSheetPage.CharacterInfo.Character.Level : 0 ).ToObservableCollection();
							return;
						} break;
					case "Achievements":
						{
							Characters = Characters.OrderByDescending( c => ( c.CharacterSheetPage != null && c.CharacterSheetPage.CharacterInfo != null ) ? c.CharacterSheetPage.CharacterInfo.Character.AchievementPoints : 0 ).ToObservableCollection();
							return;
						} break;
				}
			}

			Characters = Characters.OrderBy( c => ( c.CharacterSheetPage != null && c.CharacterSheetPage.CharacterInfo != null ) ? c.CharacterSheetPage.CharacterInfo.Character.Name : String.Empty ).ToObservableCollection();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}