using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;
using WowArmory.Controllers;
using WowArmory.Core.Managers;
using WowArmory.Enumerations;

namespace WowArmory
{
	public partial class App : Application
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Provides easy access to the root frame of the Phone Application.
		/// </summary>
		/// <returns>The root frame of the Phone Application.</returns>
		public PhoneApplicationFrame RootFrame { get; private set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Constructor for the Application object.
		/// </summary>
		public App()
		{
			// Global handler for uncaught exceptions. 
			UnhandledException += Application_UnhandledException;

			// Show graphics profiling information while debugging.
			if ( System.Diagnostics.Debugger.IsAttached )
			{
				// Display the current frame rate counters.
				Application.Current.Host.Settings.EnableFrameRateCounter = true;

				// Show the areas of the app that are being redrawn in each frame.
				//Application.Current.Host.Settings.EnableRedrawRegions = true;

				// Enable non-production analysis visualization mode, 
				// which shows areas of a page that are being GPU accelerated with a colored overlay.
				//Application.Current.Host.Settings.EnableCacheVisualization = true;
			}

			// Standard Silverlight initialization
			InitializeComponent();

			// Phone-specific initialization
			InitializePhoneApplication();
		}

		// Code to execute when the application is launching (eg, from Start)
		// This code will not execute when the application is reactivated
		private void Application_Launching( object sender, LaunchingEventArgs e )
		{
		}

		// Code to execute when the application is activated (brought to foreground)
		// This code will not execute when the application is first launched
		private void Application_Activated( object sender, ActivatedEventArgs e )
		{
		}

		// Code to execute when the application is deactivated (sent to background)
		// This code will not execute when the application is closing
		private void Application_Deactivated( object sender, DeactivatedEventArgs e )
		{
			IsolatedStorageManager.Save();
		}

		// Code to execute when the application is closing (eg, user hit Back)
		// This code will not execute when the application is deactivated
		private void Application_Closing( object sender, ClosingEventArgs e )
		{
			IsolatedStorageManager.Save();
		}

		// Code to execute if a navigation fails
		private void RootFrame_NavigationFailed( object sender, NavigationFailedEventArgs e )
		{
			if ( System.Diagnostics.Debugger.IsAttached )
			{
				// A navigation has failed; break into the debugger
				System.Diagnostics.Debugger.Break();
			}
		}

		// Code to execute on Unhandled Exceptions
		private void Application_UnhandledException( object sender, ApplicationUnhandledExceptionEventArgs e )
		{
			if ( System.Diagnostics.Debugger.IsAttached )
			{
				// An unhandled exception has occurred; break into the debugger
				System.Diagnostics.Debugger.Break();
			}
		}

		#region Phone application initialization

		// Avoid double-initialization
		private bool phoneApplicationInitialized = false;

		// Do not add any additional code to this method
		private void InitializePhoneApplication()
		{
			if ( phoneApplicationInitialized )
				return;

			// Create the frame but don't set it as RootVisual yet; this allows the splash
			// screen to remain active until the application is ready to render.
			//RootFrame = new PhoneApplicationFrame();
			RootFrame = new RadPhoneApplicationFrame();
			RootFrame.Navigated += CompleteInitializePhoneApplication;

			// Handle navigation failures
			RootFrame.NavigationFailed += RootFrame_NavigationFailed;

			// Ensure we don't initialize again
			phoneApplicationInitialized = true;

			// register all available pages
			ApplicationController.Current.Register(Page.Main, new Uri("/Views/MainPage.xaml", UriKind.Relative));
			ApplicationController.Current.Register(Page.Settings, new Uri("/Views/SettingsPage.xaml", UriKind.Relative));
			ApplicationController.Current.Register(Page.Help, new Uri("/Views/HelpPage.xaml", UriKind.Relative));
			ApplicationController.Current.Register(Page.News, new Uri("/Views/NewsPage.xaml", UriKind.Relative));
			ApplicationController.Current.Register(Page.RealmList, new Uri("/Views/RealmListPage.xaml", UriKind.Relative));
			ApplicationController.Current.Register(Page.CharacterList, new Uri("/Views/CharacterListPage.xaml", UriKind.Relative));
			ApplicationController.Current.Register(Page.CharacterDetails, new Uri("/Views/CharacterDetailsPage.xaml", UriKind.Relative));
		}

		// Do not add any additional code to this method
		private void CompleteInitializePhoneApplication( object sender, NavigationEventArgs e )
		{
			// Set the root visual to allow the application to render
			if ( RootVisual != RootFrame )
				RootVisual = RootFrame;

			// Remove this handler since it is no longer needed
			RootFrame.Navigated -= CompleteInitializePhoneApplication;
		}

		#endregion
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}