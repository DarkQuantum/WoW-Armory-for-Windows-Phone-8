using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Tasks;
using WowArmory.Core.Languages;

namespace WowArmory.ViewModels
{
	public class HelpViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Commands ---
		//----------------------------------------------------------------------
		public RelayCommand OpenFacebookPageCommand { get; private set; }
		public RelayCommand OpenTwitterPageCommand { get; private set; }
		public RelayCommand OpenSourcePageCommand { get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="HelpViewModel"/> class.
		/// </summary>
		public HelpViewModel()
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
			OpenFacebookPageCommand = new RelayCommand(OpenFacebookPage);
			OpenTwitterPageCommand = new RelayCommand(OpenTwitterPage);
			OpenSourcePageCommand = new RelayCommand(OpenSourcePage);
		}

		/// <summary>
		/// Opens the facebook page for this application.
		/// </summary>
		private void OpenFacebookPage()
		{
			var webBrowserTask = new WebBrowserTask();
			webBrowserTask.URL = AppResources.About_FacebookURL;
			webBrowserTask.Show();
		}

		/// <summary>
		/// Opens the twitter page for this application.
		/// </summary>
		private void OpenTwitterPage()
		{
			var webBrowserTask = new WebBrowserTask();
			webBrowserTask.URL = AppResources.About_TwitterURL;
			webBrowserTask.Show();
		}

		/// <summary>
		/// Opens the source page for this application.
		/// </summary>
		private void OpenSourcePage()
		{
			var webBrowserTask = new WebBrowserTask();
			webBrowserTask.URL = AppResources.About_SourceURL;
			webBrowserTask.Show();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
