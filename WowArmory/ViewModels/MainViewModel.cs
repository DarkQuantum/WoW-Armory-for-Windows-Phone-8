using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WowArmory.Controllers;
using WowArmory.Enumerations;

namespace WowArmory.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Commands ---
		//----------------------------------------------------------------------
		public RelayCommand ShowSettingsPageCommand { get; private set; }
		public RelayCommand ShowHelpPageCommand { get; private set; }
		public RelayCommand ShowNewsPageCommand { get; private set; }
		public RelayCommand ShowRealmListPageCommand { get; private set; }
		public RelayCommand ShowCharacterListPageCommand { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="MainViewModel"/> class.
		/// </summary>
		public MainViewModel()
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
			ShowSettingsPageCommand = new RelayCommand(ShowSettingsPage);
			ShowHelpPageCommand = new RelayCommand(ShowHelpPage);
			ShowNewsPageCommand = new RelayCommand(ShowNewsPage);
			ShowRealmListPageCommand = new RelayCommand(ShowRealmListPage);
			ShowCharacterListPageCommand = new RelayCommand(ShowCharacterListPage);
		}

		/// <summary>
		/// Shows the settings page.
		/// </summary>
		private void ShowSettingsPage()
		{
			ApplicationController.Current.NavigateTo(Page.Settings);
		}

		/// <summary>
		/// Shows the help page.
		/// </summary>
		private void ShowHelpPage()
		{
			ApplicationController.Current.NavigateTo(Page.Help);
		}

		/// <summary>
		/// Shows the news page.
		/// </summary>
		private void ShowNewsPage()
		{
			ApplicationController.Current.NavigateTo(Page.News);
		}

		/// <summary>
		/// Shows the realm list page.
		/// </summary>
		private void ShowRealmListPage()
		{
			ApplicationController.Current.NavigateTo(Page.RealmList);
		}

		/// <summary>
		/// Shows the character list page.
		/// </summary>
		private void ShowCharacterListPage()
		{
			ApplicationController.Current.NavigateTo(Page.CharacterList);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}