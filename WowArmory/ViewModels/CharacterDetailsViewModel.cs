using System;
using System.Globalization;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Converters;
using WowArmory.Core.Languages;
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


		/// <summary>
		/// Gets the average equipped item level string.
		/// </summary>
		public string AverageItemLevelEquipped
		{
			get
			{
				return String.Format(AppResources.UI_CharacterDetails_Character_AverageItemLevelEquipped, Character.Items.AverageItemLevelEquipped);
			}
		}

		/// <summary>
		/// Gets the melee damage string with minimum and maximum value.
		/// </summary>
		public string MeleeDamage
		{
			get
			{
				return Character.Stats.MainHandDmgMin <= 0 || Character.Stats.MainHandDmgMax <= 0 ? "--" : String.Format("{0}-{1}", Character.Stats.MainHandDmgMin, Character.Stats.MainHandDmgMax);
			}
		}

		/// <summary>
		/// Gets the melee DPS string.
		/// </summary>
		public string MeleeDps
		{
			get
			{
				if (Character.Stats.MainHandDps <= 0)
				{
					return "--";
				}

				var meleeDps = String.Format("{0:0.0}", Character.Stats.MainHandDps);
				if (Character.Items.OffHand != null && Character.Stats.OffHandDmgMin > 0)
				{
					meleeDps = String.Format("{0}/{1:0.0}", meleeDps, Character.Stats.OffHandDps);
				}

				return meleeDps;
			}
		}

		/// <summary>
		/// Gets the melee speed string.
		/// </summary>
		public string MeleeSpeed
		{
			get
			{
				if (Character.Stats.MainHandSpeed <= 0)
				{
					return "--";
				}

				var meleeSpeed = String.Format("{0:0.00}", Character.Stats.MainHandSpeed);
				if (Character.Items.OffHand != null && Character.Stats.OffHandDmgMin > 0)
				{
					meleeSpeed = String.Format("{0}/{1:0.00}", meleeSpeed, Character.Stats.OffHandSpeed);
				}

				return meleeSpeed;
			}
		}

		/// <summary>
		/// Gets the melee haste string.
		/// </summary>
		public string MeleeHaste
		{
			get
			{
				return Character.Stats.HasteRating <= 0 ? "--" : String.Format("{0}", Character.Stats.HasteRating);
			}
		}

		/// <summary>
		/// Gets the melee hit string.
		/// </summary>
		public string MeleeHit
		{
			get
			{
				return Character.Stats.HitPercent <= 0 ? "--" : String.Format("+{0:0.00}%", Character.Stats.HitPercent);
			}
		}

		/// <summary>
		/// Gets the melee crit string.
		/// </summary>
		public string MeleeCrit
		{
			get
			{
				return Character.Stats.Crit <= 0 ? "--" : String.Format("{0:0.00}%", Character.Stats.Crit);
			}
		}

		/// <summary>
		/// Gets the melee expertise string.
		/// </summary>
		public string MeleeExpertise
		{
			get
			{
				return Character.Stats.MainHandExpertise <= 0 ? "--" : String.Format("{0}", Character.Stats.MainHandExpertise);
			}
		}

		/// <summary>
		/// Gets the ranged damage string with minimum and maximum value.
		/// </summary>
		public string RangedDamage
		{
			get
			{
				if (Character.Stats.RangedDmgMin <= 0 || Character.Stats.RangedDmgMax <= 0)
				{
					return "--";
				}

				return String.Format("{0}-{1}", Character.Stats.RangedDmgMin, Character.Stats.RangedDmgMax);
			}
		}

		/// <summary>
		/// Gets the ranged DPS string.
		/// </summary>
		public string RangedDps
		{
			get
			{
				return Character.Stats.RangedDps <= 0 ? "--" : String.Format("{0:0.0}", Character.Stats.RangedDps);
			}
		}

		/// <summary>
		/// Gets the ranged speed string.
		/// </summary>
		public string RangedSpeed
		{
			get
			{
				return Character.Stats.RangedSpeed <= 0 ? "--" : String.Format("{0:0.00}", Character.Stats.RangedSpeed);
			}
		}

		/// <summary>
		/// Gets the ranged hit string.
		/// </summary>
		public string RangedHit
		{
			get
			{
				return Character.Stats.RangedHitPercent <= 0 ? "--" : String.Format("+{0:0.00}%", Character.Stats.RangedHitPercent);
			}
		}

		/// <summary>
		/// Gets the ranged crit string.
		/// </summary>
		public string RangedCrit
		{
			get
			{
				return Character.Stats.RangedCrit <= 0 ? "--" : String.Format("{0:0.00}%", Character.Stats.RangedCrit);
			}
		}

		/// <summary>
		/// Gets the spell power string.
		/// </summary>
		public string SpellPower
		{
			get
			{
				return Character.Stats.SpellPower <= 0 ? "--" : String.Format("{0}", Character.Stats.SpellPower);
			}
		}

		/// <summary>
		/// Gets the spell hit string.
		/// </summary>
		public string SpellHit
		{
			get
			{
				return Character.Stats.SpellHitPercent <= 0 ? "--" : String.Format("+{0:0.00}%", Character.Stats.SpellHitPercent);
			}
		}

		/// <summary>
		/// Gets the spell crit string.
		/// </summary>
		public string SpellCrit
		{
			get
			{
				return Character.Stats.SpellCrit <= 0 ? "--" : String.Format("{0:0.00}%", Character.Stats.SpellCrit);
			}
		}

		/// <summary>
		/// Gets the mana regen string.
		/// </summary>
		public string ManaRegen
		{
			get
			{
				return Character.Stats.Mana5 <= 0 ? "--" : String.Format("{0}", Character.Stats.Mana5);
			}
		}

		/// <summary>
		/// Gets the combat regen string.
		/// </summary>
		public string CombatRegen
		{
			get
			{
				return Character.Stats.Mana5Combat <= 0 ? "--" : String.Format("{0}", Character.Stats.Mana5Combat);
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
		public void ToggleCharacterFavorite()
		{
			RaisePropertyChanged("FavoriteImage");
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
