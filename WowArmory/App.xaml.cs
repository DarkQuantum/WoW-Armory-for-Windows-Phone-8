using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using Microsoft.Phone.Shell;
using Microsoft.Unsupported;
using WowArmory.Core;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Languages;
using WowArmory.Core.Models;
using WowArmory.Core.Pages;
using WowArmory.Core.Storage;
using WowArmory.ViewModels;

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

		public string PhoneID { get; set; }
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

			try
			{
				PhoneID = Convert.ToBase64String( (byte[])DeviceExtendedProperties.GetValue( "DeviceUniqueId" ) );
			}
			catch ( Exception ex )
			{
			}

			// Phone-specific initialization
			InitializePhoneApplication();
		}

		// Code to execute when the application is launching (eg, from Start)
		// This code will not execute when the application is reactivated
		private void Application_Launching( object sender, LaunchingEventArgs e )
		{
			TiltEffect.SetIsTiltEnabled( RootFrame, false );
		}

		// Code to execute when the application is activated (brought to foreground)
		// This code will not execute when the application is first launched
		private void Application_Activated( object sender, ActivatedEventArgs e )
		{
			// TODO: Load state from PhoneApplicationService.Current.State
			var state = this.RetrieveFromPhoneState();

			if ( state != null && state.Count > 0 )
			{
				if ( state.ContainsKey( "IsTiltEnabled" ) ) { TiltEffect.SetIsTiltEnabled( RootFrame, (bool)state[ "IsTiltEnabled" ] ); }
				if ( state.ContainsKey( "Search_TypeIndex" ) ) { ViewModelLocator.SearchStatic.SelectedSearchTypeIndex = (int)state[ "Search_TypeIndex" ]; }
				if ( state.ContainsKey( "Search_Name" ) ) { ViewModelLocator.SearchStatic.SearchName = (string)state[ "Search_Name" ]; }
				if ( state.ContainsKey( "Search_Realm" ) ) { ViewModelLocator.SearchStatic.SearchRealm = (string)state[ "Search_Realm" ]; }
				if ( state.ContainsKey( "SearchResult_Result" ) ) { ViewModelLocator.SearchResultStatic.Result = (Core.Pages.SearchPage)state[ "SearchResult_Result" ]; }
				if ( state.ContainsKey( "CharacterDetails_SelectedCharacter" ) ) { ViewModelLocator.CharacterDetailsStatic.SelectedCharacter = (ArmoryCharacter)state[ "CharacterDetails_SelectedCharacter" ]; }
			}
		}

		// Code to execute when the application is deactivated (sent to background)
		// This code will not execute when the application is closing
		private void Application_Deactivated( object sender, DeactivatedEventArgs e )
		{
			//state.Add( "SearchResult_Result", ViewModelLocator.SearchResultStatic.Result );

			this.SaveToPhoneState( "IsTiltEnabled", TiltEffect.GetIsTiltEnabled( RootFrame ) );
			this.SaveToPhoneState( "Search_TypeIndex", ViewModelLocator.SearchStatic.SelectedSearchTypeIndex );
			this.SaveToPhoneState( "Search_Name", ViewModelLocator.SearchStatic.SearchName );
			this.SaveToPhoneState( "Search_Realm", ViewModelLocator.SearchStatic.SearchRealm );
			if ( ViewModelLocator.SearchResultStatic.Result != null )
			{
				this.SaveToPhoneState( "SearchResult_Result", ViewModelLocator.SearchResultStatic.Result );
			}
			if ( ViewModelLocator.CharacterDetailsStatic.SelectedCharacter != null )
			{
				this.SaveToPhoneState( "CharacterDetails_SelectedCharacter", ViewModelLocator.CharacterDetailsStatic.SelectedCharacter );
			}
		}

		// Code to execute when the application is closing (eg, user hit Back)
		// This code will not execute when the application is deactivated
		private void Application_Closing( object sender, ClosingEventArgs e )
		{
			StorageManager.SaveSettings();
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

			var cultureInfo = Thread.CurrentThread.CurrentCulture;
			var regionInfo = RegionInfo.CurrentRegion;

			if ( regionInfo.EnglishName.Equals( "United States", StringComparison.CurrentCultureIgnoreCase ) )
			{
				Armory.Current.Region = Region.USA;
			}
			else
			{
				Armory.Current.Region = Region.Europe;
			}

			Armory.Current.Locale = AppResources.Armory_Locale;

			StorageManager.LoadSettings();

			if ( StorageManager.Settings.ContainsKey( "region" ) )
			{
				Armory.Current.Region = (Region)StorageManager.Settings[ "region" ];
			}

			if ( !StorageManager.Settings.ContainsKey( "autoUpdateCharacter" ) )
			{
				StorageManager.Settings.Upsert( "autoUpdateCharacter", true );
			}

			// Create the frame but don't set it as RootVisual yet; this allows the splash
			// screen to remain active until the application is ready to render.
			//RootFrame = new PhoneApplicationFrame();
			RootFrame = new TransitionFrame();
			RootFrame.Navigated += CompleteInitializePhoneApplication;

			// Handle navigation failures
			RootFrame.NavigationFailed += RootFrame_NavigationFailed;

			// Ensure we don't initialize again
			phoneApplicationInitialized = true;
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