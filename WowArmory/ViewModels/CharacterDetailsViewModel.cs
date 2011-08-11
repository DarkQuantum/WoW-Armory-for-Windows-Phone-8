using System;
using System.Globalization;
using GalaSoft.MvvmLight;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Converters;
using WowArmory.Core.Managers;

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

		public int CharacterFaction
		{
			get
			{
				if (Character == null) return -1;
				
				var converter = new RaceToFactionConverter();
				return (int)converter.Convert(Character.Race, typeof(Int32), null, CultureInfo.CurrentCulture);
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public CharacterDetailsViewModel()
		{
			Character = IsolatedStorageManager.GetValue<Character>("MainPage_Character", null);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
