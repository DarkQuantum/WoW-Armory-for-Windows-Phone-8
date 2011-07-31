using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Models;
using WowArmory.Core.Pages;

namespace WowArmory.Core.Storage
{
	public class StorageManager
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private static Dictionary<string, ImageSource> _imageCache = new Dictionary<string, ImageSource>();
		private static ObservableCollection<CharacterStorageData> _savedCharacters = null;
		private static bool _isDirty = false;
		private static bool _isStorageDirty = true;
		private static Dictionary<string, object> _settings;
		private static ObservableCollection<ArmoryCharacter> _storedCharacters = new ObservableCollection<ArmoryCharacter>();
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public static ObservableCollection<CharacterStorageData> SavedCharacters
		{
			get
			{
				if ( _isDirty || _savedCharacters == null )
				{
					FillSavedCharacters();
				}

				return _savedCharacters;
			}
		}

		public static Dictionary<string, ImageSource> ImageCache
		{
			get { return _imageCache; }
			set { _imageCache = value; }
		}

		public static Dictionary<string, object> Settings
		{
			get
			{
				if ( _settings == null || _settings.Count == 0 )
				{
					LoadSettings();
				}

				return _settings;
			}
			set
			{
				if ( _settings == value ) return;

				_settings = value;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public static void FillSavedCharacters()
		{
			var serializer = new XmlSerializer( typeof( ObservableCollection<CharacterStorageData> ) );
			var savedCharacters = new ObservableCollection<CharacterStorageData>();

			using ( var store = IsolatedStorageFile.GetUserStoreForApplication() )
			{
				if ( store.DirectoryExists( "CharacterData\\" ) && store.FileExists( "CharacterData\\characterstoragedata.xml" ) )
				{
					using ( var stream = new IsolatedStorageFileStream( "CharacterData\\characterstoragedata.xml", FileMode.Open, store ) )
					{
						if ( stream.Length > 0 )
						{
							savedCharacters = (ObservableCollection<CharacterStorageData>)serializer.Deserialize( stream );
						}
					}
				}
			}

			_savedCharacters = savedCharacters;
			_isDirty = false;
		}

		public static void StoreCharacterStorageData()
		{
			if ( SavedCharacters != null )
			{
				using ( var store = IsolatedStorageFile.GetUserStoreForApplication() )
				{
					var stream = new MemoryStream();
					var serializer = new XmlSerializer( typeof( ObservableCollection<CharacterStorageData> ) );
					serializer.Serialize( stream, SavedCharacters );

					stream.Seek( 0, SeekOrigin.Begin );
					using ( stream )
					{
						if ( !store.DirectoryExists( "CharacterData\\" ) ) store.CreateDirectory( "CharacterData\\" );

						var file = store.CreateFile( "CharacterData\\characterstoragedata.xml" );
						int readChunk = 1024 * 1024;
						int writeChunk = 1024 * 1024;
						var buffer = new byte[ readChunk ];
						while ( true )
						{
							var read = stream.Read( buffer, 0, readChunk );
							if ( read <= 0 ) break;

							var write = read;
							while ( write > 0 )
							{
								file.Write( buffer, 0, Math.Min( write, writeChunk ) );
								write -= Math.Min( write, writeChunk );
							}
						}

						file.Close();
					}
				}
			}
		}

		public static CharacterStorageData GetCharacterStorageData( string realm, string character )
		{
			CharacterStorageData characterStorageData = SavedCharacters.Where( ( csd, b ) =>
				csd.RealmName.Equals( realm, StringComparison.CurrentCultureIgnoreCase ) &&
				csd.CharacterName.Equals( character, StringComparison.CurrentCultureIgnoreCase ) ).FirstOrDefault();

			return characterStorageData;
		}

		public static void StoreCharacter( ArmoryCharacter armoryCharacter )
		{
			var storageGuid = Guid.NewGuid();

			var characterStorageData = GetCharacterStorageData( armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Realm, armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Name );
			if ( characterStorageData != null )
			{
				storageGuid = characterStorageData.StorageGuid;
			}

			var directoryPath = String.Format( "CharacterData\\{0}", storageGuid );
			var fileName = String.Empty;
			using ( var store = IsolatedStorageFile.GetUserStoreForApplication() )
			{
				if ( !store.DirectoryExists( directoryPath ) )
				{
					store.CreateDirectory( directoryPath );
				}

				using ( var file = store.OpenFile( String.Format( "{0}\\character-sheet.xml", directoryPath ), FileMode.OpenOrCreate ) )
				{
					using ( var writer = new StreamWriter( file ) )
					{
						writer.Write( armoryCharacter.CharacterSheetPage.Document.ToString() );
						writer.Flush();
						writer.Close();
					}
				}

				using ( var file = store.OpenFile( String.Format( "{0}\\character-reputation.xml", directoryPath ), FileMode.OpenOrCreate ) )
				{
					using ( var writer = new StreamWriter( file ) )
					{
						writer.Write( armoryCharacter.CharacterReputationPage.Document.ToString() );
						writer.Flush();
						writer.Close();
					}
				}

				using ( var file = store.OpenFile( String.Format( "{0}\\character-talents.xml", directoryPath ), FileMode.OpenOrCreate ) )
				{
					using ( var writer = new StreamWriter( file ) )
					{
						writer.Write( armoryCharacter.CharacterTalentsPage.Document.ToString() );
						writer.Flush();
						writer.Close();
					}
				}

				using ( var file = store.OpenFile( String.Format( "{0}\\character-feed-data.xml", directoryPath ), FileMode.OpenOrCreate ) )
				{
					using ( var writer = new StreamWriter( file ) )
					{
						writer.Write( armoryCharacter.CharacterActivityFeedPage.Document.ToString() );
						writer.Flush();
						writer.Close();
					}
				}
			}

			fileName = "settings.xml";
			using ( var store = IsolatedStorageFile.GetUserStoreForApplication() )
			{
				if ( !store.DirectoryExists( directoryPath ) )
				{
					store.CreateDirectory( directoryPath );
				}

				var settingsElement = new XElement( "Settings" );
				var regionElement = new XElement( "Region" );
				var localeElement = new XElement( "Locale" );
				var lastUpdateElement = new XElement( "LastUpdate" );
				regionElement.SetValue( armoryCharacter.Region.ToString() );
				localeElement.SetValue( armoryCharacter.Locale );
				lastUpdateElement.SetValue( armoryCharacter.LastUpdate.ToString() );
				settingsElement.Add( regionElement );
				settingsElement.Add( localeElement );
				settingsElement.Add( lastUpdateElement );
				var settingsDocument = new XDocument( settingsElement );

				using ( var file = store.OpenFile( String.Format( "{0}\\{1}", directoryPath, fileName ), FileMode.OpenOrCreate ) )
				{
					using ( var writer = new StreamWriter( file ) )
					{
						writer.Write( settingsDocument.ToString() );
					}
				}
			}

			if ( characterStorageData == null )
			{
				SavedCharacters.Add( new CharacterStorageData()
										{
											RealmName = armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Realm,
											CharacterName = armoryCharacter.CharacterSheetPage.CharacterInfo.Character.Name,
											StorageGuid = storageGuid
										} );

				StoreCharacterStorageData();
			}

			_isDirty = true;
			_isStorageDirty = true;
		}

		public static ObservableCollection<ArmoryCharacter> GetStoredCharacters()
		{
			if ( _isStorageDirty )
			{
				_storedCharacters = new ObservableCollection<ArmoryCharacter>();
				var fileName = String.Empty;

				FillSavedCharacters();

				using ( var store = IsolatedStorageFile.GetUserStoreForApplication() )
				{
					foreach ( var characterStorageData in SavedCharacters )
					{
						var region = Armory.Current.Region;
						var locale = Armory.Current.Locale;
						var lastUpdate = String.Empty;
						var armoryCharacter = new ArmoryCharacter();
						CharacterSheetPage characterSheetPage = null;
						CharacterReputationPage characterReputationPage = null;
						CharacterTalentsPage characterTalentsPage = null;
						CharacterActivityFeedPage characterActivityFeedPage = null;

						using ( var file = store.OpenFile( String.Format( "CharacterData\\{0}\\settings.xml", characterStorageData.StorageGuid ), FileMode.Open ) )
						{
							var document = XDocument.Load( XmlReader.Create( file ) );
							var settingsElement = document.Element( "Settings" );
							if ( settingsElement != null )
							{
								var regionElement = settingsElement.Element( "Region" );
								if ( regionElement != null )
								{
									region = Armory.Current.GetArmoryRegionByString( regionElement.Value );
								}

								var localeElement = settingsElement.Element( "Locale" );
								if ( localeElement != null )
								{
									locale = localeElement.Value;
								}

								var lastUpdateElement = settingsElement.Element( "LastUpdate" );
								if ( lastUpdateElement != null )
								{
									lastUpdate = lastUpdateElement.Value;
								}
							}
						}

						fileName = "characterpage.xml";
						if ( store.FileExists( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ) ) )
						{
							using ( var file = store.OpenFile( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ), FileMode.Open ) )
							{
								try
								{
									characterSheetPage = CharacterSheetPage.FromArmoryPage( new ArmoryPage( XDocument.Load( XmlReader.Create( file ) ) ) );
								}
								catch ( Exception ex )
								{
								}
							}
						}

						fileName = "character-sheet.xml";
						if ( store.FileExists( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ) ) )
						{
							using ( var file = store.OpenFile( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ), FileMode.Open ) )
							{
								try
								{
									characterSheetPage = CharacterSheetPage.FromArmoryPage( new ArmoryPage( XDocument.Load( XmlReader.Create( file ) ) ) );
								}
								catch ( Exception ex )
								{
								}
							}
						}

						fileName = "character-reputation.xml";
						if ( store.FileExists( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ) ) )
						{
							using ( var file = store.OpenFile( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ), FileMode.Open ) )
							{
								try
								{
									characterReputationPage = CharacterReputationPage.FromArmoryPage( new ArmoryPage( XDocument.Load( XmlReader.Create( file ) ) ) );
								}
								catch ( Exception ex )
								{
								}
							}
						}

						fileName = "character-talents.xml";
						if ( store.FileExists( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ) ) )
						{
							using ( var file = store.OpenFile( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ), FileMode.Open ) )
							{
								try
								{
									characterTalentsPage = CharacterTalentsPage.FromArmoryPage( new ArmoryPage( XDocument.Load( XmlReader.Create( file ) ) ) );
								}
								catch ( Exception ex )
								{
								}
							}
						}

						fileName = "character-feed-data.xml";
						if ( store.FileExists( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ) ) )
						{
							using ( var file = store.OpenFile( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ), FileMode.Open ) )
							{
								try
								{
									characterActivityFeedPage = CharacterActivityFeedPage.FromArmoryPage( new ArmoryPage( XDocument.Load( XmlReader.Create( file ) ) ) );
								}
								catch ( Exception ex )
								{
								}
							}
						}

						armoryCharacter.CharacterSheetPage = characterSheetPage;
						armoryCharacter.CharacterReputationPage = characterReputationPage;
						armoryCharacter.CharacterTalentsPage = characterTalentsPage;
						armoryCharacter.CharacterActivityFeedPage = characterActivityFeedPage;
						armoryCharacter.Region = region;
						armoryCharacter.Locale = locale;
						var lastUpdateValue = DateTime.MinValue;
						DateTime.TryParse( lastUpdate, out lastUpdateValue );
						armoryCharacter.LastUpdate = lastUpdateValue;

						if ( armoryCharacter.CharacterSheetPage == null ||
							armoryCharacter.CharacterReputationPage == null ||
							armoryCharacter.CharacterTalentsPage == null ||
							armoryCharacter.CharacterActivityFeedPage == null ||
							!armoryCharacter.Locale.Equals( Armory.Current.Locale, StringComparison.CurrentCultureIgnoreCase ) )
						{
							armoryCharacter.IsDirty = true;
						}

						_storedCharacters.Add( armoryCharacter );
					}
				}

				_isStorageDirty = false;
			}

			return _storedCharacters;
		}

		public static void DeleteCharacter( string realm, string characterName )
		{
			var characterStorageData = SavedCharacters.Where( csd => csd.RealmName.Equals( realm, StringComparison.CurrentCultureIgnoreCase ) && csd.CharacterName.Equals( characterName, StringComparison.CurrentCultureIgnoreCase ) ).FirstOrDefault();
			if ( characterStorageData == null ) return;

			using ( var store = IsolatedStorageFile.GetUserStoreForApplication() )
			{
				foreach ( var fileName in store.GetFileNames( String.Format( "CharacterData\\{0}\\*", characterStorageData.StorageGuid ) ) )
				{
					store.DeleteFile( String.Format( "CharacterData\\{0}\\{1}", characterStorageData.StorageGuid, fileName ) );
				}

				store.DeleteDirectory( String.Format( "CharacterData\\{0}", characterStorageData.StorageGuid ) );
			}

			SavedCharacters.Remove( characterStorageData );
			StoreCharacterStorageData();
			_isStorageDirty = true;
			FillSavedCharacters();
		}

		public static void DeleteAllSavedCharacters()
		{
			using ( var store = IsolatedStorageFile.GetUserStoreForApplication() )
			{
				if ( store.DirectoryExists( "CharacterData\\" ) && store.FileExists( "CharacterData\\characterstoragedata.xml" ) )
				{
					store.DeleteFile( "CharacterData\\characterstoragedata.xml" );
				}

				if ( store.DirectoryExists( "CharacterData\\" ) )
				{
					foreach ( var directoryName in store.GetDirectoryNames( "CharacterData\\*" ) )
					{
						foreach ( var fileName in store.GetFileNames( String.Format( "CharacterData\\{0}\\*", directoryName ) ) )
						{
							store.DeleteFile( String.Format( "CharacterData\\{0}\\{1}", directoryName, fileName ) );
						}

						store.DeleteDirectory( String.Format( "CharacterData\\{0}", directoryName ) );
					}

					store.DeleteDirectory( "CharacterData" );
				}
			}

			_isStorageDirty = true;

			FillSavedCharacters();
		}

		public static void SaveSettings()
		{
			var fileName = "settings.xml";
			using ( var store = IsolatedStorageFile.GetUserStoreForApplication() )
			{
				if ( store.FileExists( fileName ) )
				{
					store.DeleteFile( fileName );
				}

				var settingsElement = new XElement( "Settings" );

				if ( Settings.ContainsKey( "region" ) )
				{
					var regionElement = new XElement( "Region" );
					regionElement.SetValue( ( (Region)Settings[ "region" ] ).ToString() );
					settingsElement.Add( regionElement );
				}

				if ( Settings.ContainsKey( "favoritesSortBy" ) )
				{
					var favoritesSortByElement = new XElement( "FavoritesSortBy" );
					favoritesSortByElement.SetValue( Settings[ "favoritesSortBy" ].ToString() );
					settingsElement.Add( favoritesSortByElement );
				}

				if ( Settings.ContainsKey( "autoUpdateCharacter" ) )
				{
					var autoUpdateCharacterElement = new XElement( "AutoUpdateCharacter" );
					var value = (bool)Settings[ "autoUpdateCharacter" ] ? 1 : 0;
					autoUpdateCharacterElement.SetValue( value );
					settingsElement.Add( autoUpdateCharacterElement );
				}

				if ( Settings.ContainsKey( "useAutoUpdateTimeInterval" ) )
				{
					var useAutoUpdateTimeIntervalElement = new XElement( "UseAutoUpdateTimeInterval" );
					var value = (bool)Settings[ "useAutoUpdateTimeInterval" ] ? 1 : 0;
					useAutoUpdateTimeIntervalElement.SetValue( value );
					settingsElement.Add( useAutoUpdateTimeIntervalElement );
				}

				if ( Settings.ContainsKey( "autoUpdateTimeInterval" ) )
				{
					var autoUpdateTimeIntervalElement = new XElement( "AutoUpdateTimeInterval" );
					var value = Settings[ "autoUpdateTimeInterval" ];
					autoUpdateTimeIntervalElement.SetValue( value );
					settingsElement.Add( autoUpdateTimeIntervalElement );
				}

				var settingsDocument = new XDocument( settingsElement );

				using ( var file = store.OpenFile( String.Format( "{0}", fileName ), FileMode.OpenOrCreate ) )
				{
					using ( var writer = new StreamWriter( file ) )
					{
						writer.Write( settingsDocument.ToString() );
					}
				}
			}
		}

		public static void LoadSettings()
		{
			var settings = new Dictionary<string, object>();

			using ( var store = IsolatedStorageFile.GetUserStoreForApplication() )
			{
				var fileName = "settings.xml";
				if ( store.FileExists( fileName ) )
				{
					using ( var file = store.OpenFile( fileName, FileMode.Open ) )
					{
						var document = XDocument.Load( XmlReader.Create( file ) );
						var settingsElement = document.Element( "Settings" );
						if ( settingsElement != null )
						{
							var regionElement = settingsElement.Element( "Region" );
							if ( regionElement != null )
							{
								settings.Add( "region", Armory.Current.GetArmoryRegionByString( regionElement.Value ) );
							}

							var favoritesSortByElement = settingsElement.Element( "FavoritesSortBy" );
							if ( favoritesSortByElement != null )
							{
								settings.Add( "favoritesSortBy", favoritesSortByElement.Value );
							}

							var autoUpdateCharacterElement = settingsElement.Element( "AutoUpdateCharacter" );
							if ( autoUpdateCharacterElement != null )
							{
								var value = autoUpdateCharacterElement.Value.Equals( "1" ) ? true : false;
								settings.Add( "autoUpdateCharacter", value );
							}

							var useAutoUpdateTimeIntervalElement = settingsElement.Element( "UseAutoUpdateTimeInterval" );
							if ( useAutoUpdateTimeIntervalElement != null )
							{
								var value = useAutoUpdateTimeIntervalElement.Value.Equals( "1" ) ? true : false;
								settings.Add( "useAutoUpdateTimeInterval", value );
							}

							var autoUpdateTimeIntervalElement = settingsElement.Element( "AutoUpdateTimeInterval" );
							if ( autoUpdateTimeIntervalElement != null )
							{
								settings.Add( "autoUpdateTimeInterval", autoUpdateTimeIntervalElement.Value );
							}
						}
					}
				}
			}

			Settings = settings;
		}

		public static bool IsCharacterStored( string realm, string characterName )
		{
			if ( GetCharacterStorageData( realm, characterName ) != null )
			{
				return true;
			}

			return false;
		}

		public static ImageSource GetImageSourceFromCache( string key )
		{
			return GetImageSourceFromCache( key, UriKind.Absolute );
		}

		public static ImageSource GetImageSourceFromCache( string key, UriKind uriKind )
		{
			if ( String.IsNullOrEmpty( key ) ) return null;

			if ( !ImageCache.ContainsKey( key ) )
			{
				var imageSource = new BitmapImage( new Uri( key, uriKind ) );
				ImageCache.Add( key, imageSource );
			}

			return ImageCache[ key ];
		}

		public static ImageSource GetImageSourceFromCache( string key, ImageSource imageSource )
		{
			return GetImageSourceFromCache( key, imageSource, UriKind.Absolute );
		}

		public static ImageSource GetImageSourceFromCache( string key, ImageSource imageSource, UriKind uriKind )
		{
			if ( String.IsNullOrEmpty( key ) ) return null;

			if ( !ImageCache.ContainsKey( key ) )
			{
				ImageCache.Add( key, imageSource );
			}

			return ImageCache[ key ];
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
