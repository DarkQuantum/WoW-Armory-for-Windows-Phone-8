using System;
using System.Globalization;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Converters;
using WowArmory.Core.Managers;
using WowArmory.Core.Storage;

namespace WowArmory.ViewModels
{
	public class CharacterDetailsViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private Character _character;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Events ---
		//----------------------------------------------------------------------
		public delegate void CharacterLoadedEventHandler(Character character);
		public event CharacterLoadedEventHandler OnCharacterLoaded;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the character.
		/// </summary>
		/// <value>
		/// The character.
		/// </value>
		public Character Character
		{
			get { return _character; }
			set
			{
				if (_character == value) return;

				_character = value;
				RaisePropertyChanged("Character");
				RaisePropertyChanged("CharacterFaction");

				if (OnCharacterLoaded != null)
				{
					OnCharacterLoaded(value);
				}
			}
		}

		/// <summary>
		/// Gets the character faction.
		/// </summary>
		public int CharacterFaction
		{
			get
			{
				if (Character == null) return -1;
				
				var converter = new RaceToFactionConverter();
				return (int)converter.Convert(Character.Race, typeof(Int32), null, CultureInfo.CurrentCulture);
			}
		}

		/// <summary>
		/// Gets the favorite image source used in the view.
		/// </summary>
		public ImageSource FavoriteImage
		{
			get
			{
				var isCharacterStored = IsolatedStorageManager.IsCharacterStored(Character.Region, Character.Realm, Character.Name);
				return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/CharacterDetails/Favorite_{0}.png", isCharacterStored ? 1 : 0));
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		
		//----------------------------------------------------------------------
		#region --- Commands ---
		//----------------------------------------------------------------------
		public RelayCommand ToggleCharacterFavoriteCommand { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterDetailsViewModel"/> class.
		/// </summary>
		public CharacterDetailsViewModel()
		{
			InitializeCommands();

			Character = IsolatedStorageManager.GetValue<Character>("MainPage_Character", null);
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
			ToggleCharacterFavoriteCommand = new RelayCommand(ToggleCharacterFavorite);
		}

		/// <summary>
		/// Addes or removes the current character from the favorites.
		/// </summary>
		private void ToggleCharacterFavorite()
		{
			if (IsolatedStorageManager.IsCharacterStored(Character.Region, Character.Realm, Character.Name))
			{
				IsolatedStorageManager.UnstoreCharacter(Character);
			}
			else
			{
				IsolatedStorageManager.StoreCharacter(Character);
			}

			RaisePropertyChanged("FavoriteImage");
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
