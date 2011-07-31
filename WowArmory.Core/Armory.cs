using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Languages;
using WowArmory.Core.Models;
using WowArmory.Core.Pages;

namespace WowArmory.Core
{
	public class Armory
	{
		//---------------------------------------------------------------------------
		#region --- Singleton Implementation ---
		//---------------------------------------------------------------------------
		private static Armory _instance;

		/// <summary>
		/// Gets the current instance of the armory parser.
		/// </summary>
		public static Armory Current
		{
			get { return _instance ?? (_instance = new Armory()); }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private Region _region = Region.Europe;
		private string _locale = "en_us";
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Gets the base URI used to access the armory.
		/// </summary>
		public Uri BaseUri
		{
			get { return GetArmoryUriByRegion( Region ); }
		}

		/// <summary>
		/// Gets or sets the region to retrieve information from.
		/// </summary>
		/// <value>
		/// The region to retrieve information from.
		/// </value>
		public Region Region
		{
			get { return _region; }
			set { _region = value; }
		}

		/// <summary>
		/// Gets or sets the locale to use when retrieving information.
		/// </summary>
		/// <value>
		/// The locale to use when retrieving information.
		/// </value>
		public string Locale
		{
			get { return _locale; }
			set { _locale = value; }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="Armory"/> class.
		/// </summary>
		public Armory()
		{
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public void GetCharacterFromArmoryAsync( string characterName, string realmName, Action<ArmoryCharacter> action )
		{
			var armoryCharacter = new ArmoryCharacter();

			GetCharacterPageAsync( characterName, realmName, ( csp ) =>
			{
				if ( csp == null || !csp.IsPageValid )
				{
					action( armoryCharacter );
					return;
				}

				armoryCharacter.CharacterSheetPage = csp;

				GetCharacterReputationPageAsync( characterName, realmName, ( crp ) =>
				{
					if ( crp == null || !crp.IsPageValid )
					{
						action( armoryCharacter );
						return;
					}

					armoryCharacter.CharacterReputationPage = crp;

					GetCharacterTalentsPageAsync( characterName, realmName, ( ctp ) =>
					{
						if ( ctp == null || !ctp.IsPageValid )
						{
							action( armoryCharacter );
							return;
						}

						armoryCharacter.CharacterTalentsPage = ctp;

						GetCharacterActivityFeedPageAsync( characterName, realmName, ( cafp ) =>
						{
							if ( cafp == null )
							{
								action( armoryCharacter );
								return;
							}

							armoryCharacter.CharacterActivityFeedPage = cafp;
							armoryCharacter.Region = Region;
							armoryCharacter.Locale = Locale;
							armoryCharacter.LastUpdate = DateTime.Now;
							action( armoryCharacter );
						} );
					} );
				} );
			} );
		}

		public void GetCharacterPageAsync( string characterName, string realmName, Action<CharacterSheetPage> action )
		{
			var parameters = new Dictionary<string, string>();
			parameters.Add( "r", realmName );
			parameters.Add( "cn", characterName );

			GetArmoryPageAsync( "character-sheet.xml", parameters,
				armoryPage => action( CharacterSheetPage.FromArmoryPage( armoryPage ) ) );
		}

		public void GetCharacterReputationPageAsync( string characterName, string realmName, Action<CharacterReputationPage> action )
		{
			var parameters = new Dictionary<string, string>();
			parameters.Add( "r", realmName );
			parameters.Add( "cn", characterName );

			GetArmoryPageAsync( "character-reputation.xml", parameters,
				armoryPage => action( CharacterReputationPage.FromArmoryPage( armoryPage ) ) );
		}

		public void GetCharacterTalentsPageAsync( string characterName, string realmName, Action<CharacterTalentsPage> action )
		{
			var parameters = new Dictionary<string, string>();
			parameters.Add( "r", realmName );
			parameters.Add( "cn", characterName );

			GetArmoryPageAsync( "character-talents.xml", parameters,
				armoryPage => action( CharacterTalentsPage.FromArmoryPage( armoryPage ) ) );
		}

		public void GetCharacterActivityFeedPageAsync( string characterName, string realmName, Action<CharacterActivityFeedPage> action )
		{
			var parameters = new Dictionary<string, string>();
			parameters.Add( "r", realmName );
			parameters.Add( "cn", characterName );

			GetArmoryPageAsync( "character-feed-data.xml", parameters,
				armoryPage => action( CharacterActivityFeedPage.FromArmoryPage( armoryPage ) ) );
		}

		public void GetItemTooltipPageAsync( int itemId, Region region, string realmName, string characterName, int slotId, Action<ItemToolTipPage> action )
		{
			var parameters = new Dictionary<string, string>();
			parameters.Add( "i", itemId.ToString() );
			parameters.Add( "r", realmName );
			parameters.Add( "cn", characterName );
			parameters.Add( "s", slotId.ToString() );

			var prevRegion = Region;
			Region = region;

			GetArmoryPageAsync( "item-tooltip.xml", parameters,
				armoryPage => action( ItemToolTipPage.FromArmoryPage( armoryPage ) ) );

			Region = prevRegion;
		}

		public void GetSearchPageAsync( string searchType, string query, Action<SearchPage> action )
		{
			var parameters = new Dictionary<string, string>();
			parameters.Add( "searchQuery", query );
			parameters.Add( "searchType", searchType );

			GetArmoryPageAsync( "search.xml", parameters,
				armoryPage => action( SearchPage.FromArmoryPage( armoryPage ) ) );
		}

		public void GetArmoryPageAsync( string page, Dictionary<string, string> parameters, Action<ArmoryPage> action )
		{
			try
			{
				var uriBuilder = new UriBuilder( String.Format( "{0}{1}", BaseUri, page ) );

				var queryString = parameters.Aggregate( "", ( current, parameter ) =>
					String.Format( "{0}{1}{2}={3}", current, ( !String.IsNullOrEmpty( current ) ? "&" : "" ), parameter.Key, parameter.Value ) );
				uriBuilder.Query = queryString;

				// we can not derive from WebClient because it's constructor is internally marked as SecuritySafeCritical
				// and because WebClient does not accept cookies we have to use HttpWebRequest
				// http://forums.create.msdn.com/forums/p/64432/394235.aspx

				//var webClient = new WebClient();
				//webClient.Headers[ "user-agent" ] = "MSIE 7.0";
				//webClient.DownloadStringCompleted += delegate( object sender, DownloadStringCompletedEventArgs e )
				//{
				//    try
				//    {
				//        var document = XDocument.Parse( e.Result );
				//        Deployment.Current.Dispatcher.BeginInvoke( () =>
				//            action( new ArmoryPage( document ) { Region = Region, Locale = Locale } )
				//        );
				//    }
				//    catch ( Exception ex )
				//    {
				//        Deployment.Current.Dispatcher.BeginInvoke( () =>
				//            action( new ArmoryPage( null ) { Region = Region, Locale = Locale } )
				//        );
				//    }
				//};
				//webClient.DownloadStringAsync( uriBuilder.Uri );

				var request = (HttpWebRequest)WebRequest.Create( uriBuilder.Uri );
				request.Headers[ "user-agent" ] = "MSIE 7.0";
				request.CookieContainer = new CookieContainer();
				request.CookieContainer.Add( uriBuilder.Uri, new Cookie( "cookieLangId", Locale ) );
				request.AllowAutoRedirect = true;

				request.BeginGetResponse( delegate( IAsyncResult result )
				{
					try
					{
						var response = request.EndGetResponse( result );
						using ( var stream = response.GetResponseStream() )
						{
							var document = XDocument.Load( stream );
							Deployment.Current.Dispatcher.BeginInvoke( () =>
								action( new ArmoryPage( document ) { Region = Region, Locale = Locale } ) );
						}
					}
					catch ( Exception ex )
					{
						Deployment.Current.Dispatcher.BeginInvoke( () =>
							action( new ArmoryPage( null ) { Region = Region, Locale = Locale } ) );
					}
				}, null );
			}
			catch ( Exception ex )
			{
				MessageBox.Show( AppResources.UI_Error_Unknown_Text, AppResources.UI_Error_Unknown_Title, MessageBoxButton.OK );
			}
		}

		public void SetRegionByString( string region )
		{
			if ( region.Equals( "Europe", StringComparison.CurrentCultureIgnoreCase ) )
			{
				Region = Region.Europe;
			}
			else if ( region.Equals( "USA", StringComparison.CurrentCultureIgnoreCase ) )
			{
				Region = Region.USA;
			}
			else if ( region.Equals( "China", StringComparison.CurrentCultureIgnoreCase ) )
			{
				Region = Region.China;
			}
			else if ( region.Equals( "Korea", StringComparison.CurrentCultureIgnoreCase ) )
			{
				Region = Region.Korea;
			}
			else if ( region.Equals( "Taiwan", StringComparison.CurrentCultureIgnoreCase ) )
			{
				Region = Region.Taiwan;
			}
		}

		public Uri GetArmoryUriByRegion( Region region )
		{
			var regionString = "";

			switch ( region )
			{
				case Region.Europe:
					{
						regionString = "eu";
					} break;
				case Region.USA:
					{
						regionString = "www";
					} break;
				case Region.Korea:
					{
						regionString = "kr";
					} break;
				case Region.China:
					{
						regionString = "cn";
					} break;
				case Region.Taiwan:
					{
						regionString = "tw";
					} break;
			}

			return new Uri( String.Format( "http://{0}.wowarmory.com", regionString ) );
		}

		public Region GetArmoryRegionByString( string region )
		{
			if ( region.Equals( "Europe", StringComparison.CurrentCultureIgnoreCase ) )
			{
				return Region.Europe;
			}
			if ( region.Equals( "USA", StringComparison.CurrentCultureIgnoreCase ) )
			{
				return Region.USA;
			}
			if ( region.Equals( "China", StringComparison.CurrentCultureIgnoreCase ) )
			{
				return Region.China;
			}
			if ( region.Equals( "Korea", StringComparison.CurrentCultureIgnoreCase ) )
			{
				return Region.Korea;
			}
			if ( region.Equals( "Taiwan", StringComparison.CurrentCultureIgnoreCase ) )
			{
				return Region.Taiwan;
			}

			return Armory.Current.Region;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}