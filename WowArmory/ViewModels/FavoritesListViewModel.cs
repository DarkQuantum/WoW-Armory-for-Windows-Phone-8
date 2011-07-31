// 
// Copyright (c) 2010 Christian Krueger <christian.krueger@krueger-c.com>
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge and for non-commercial usage
// only, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify,
// merge, publish, and/or distribute copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

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