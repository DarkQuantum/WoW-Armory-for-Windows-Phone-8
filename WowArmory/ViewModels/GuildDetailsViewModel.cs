using System;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Managers;

namespace WowArmory.ViewModels
{
	public class GuildDetailsViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private Guild _guild;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Events ---
		//----------------------------------------------------------------------
		public delegate void GuildLoadedEventHandler(Guild guild);
		public event GuildLoadedEventHandler OnGuildLoaded;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
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
				RaisePropertyChanged("Guild");

				if (OnGuildLoaded != null)
				{
					OnGuildLoaded(value);
				}
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

		/// <summary>
		/// Addes or removes the current guild from the favorites.
		/// </summary>
		public void ToggleGuildFavorite()
		{
			RaisePropertyChanged("FavoriteImage");
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
