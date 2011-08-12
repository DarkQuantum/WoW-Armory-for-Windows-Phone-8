using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Xml.Serialization;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Converters;
using WowArmory.Core.Storage;

namespace WowArmory.Core.Managers
{
	public static class IsolatedStorageManager
	{
		//----------------------------------------------------------------------
		#region --- Constants ---
		//----------------------------------------------------------------------
		const string CHARACTER_STORAGE_PATH = "Characters";
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the stored characters.
		/// </summary>
		/// <value>
		/// The stored characters.
		/// </value>
		public static ObservableCollection<CharacterStorageData> StoredCharacters
		{
			get
			{
				return GetValue("Storage_Characters", new ObservableCollection<CharacterStorageData>());
			}
			set
			{
				SetValue("Storage_Characters", value);
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		

		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the value of the application setting for the specified key from the isolated storage settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The application setting key.</param>
		/// <param name="defaultValue">The default value to use.</param>
		/// <returns>
		/// The value of the application setting for the specified key from the isolated storage settings.
		/// </returns>
		public static T GetValue<T>(string key, T defaultValue)
		{
			try
			{
				T value;
				if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out value))
				{
					value = defaultValue;
					SetValue(key, value);
					Save();
				}

				return value;
			}
			catch
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// Sets the value of the application setting for the specified key in the isolated storage settings.
		/// </summary>
		/// <param name="key">The application setting key.</param>
		/// <param name="value">The application setting value to set.</param>
		public static void SetValue(string key, object value)
		{
			if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
			{
				IsolatedStorageSettings.ApplicationSettings[key] = value;
			}
			else
			{
				IsolatedStorageSettings.ApplicationSettings.Add(key, value);
			}
		}

		/// <summary>
		/// Saves the current state of the isolated storage settings.
		/// </summary>
		public static void Save()
		{
			IsolatedStorageSettings.ApplicationSettings.Save();
		}

		/// <summary>
		/// Determines whether the specified character is alread stored.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="realm">The realm.</param>
		/// <param name="name">The name of the character.</param>
		/// <returns>
		///   <c>true</c> if the specified character is already stored; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsCharacterStored(Region region, string realm, string name)
		{
			return GetStoredCharacter(region, realm, name) != null;
		}

		/// <summary>
		/// Try to get the specified character from the storage.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="realm">The realm.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static CharacterStorageData GetStoredCharacter(Region region, string realm, string name)
		{
			return StoredCharacters.Where(c => c.Region == region && c.Realm.Equals(realm, StringComparison.CurrentCultureIgnoreCase) && c.Character.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
		}

		/// <summary>
		/// Stores the specified character.
		/// </summary>
		/// <param name="character">The character to store.</param>
		public static void StoreCharacter(Character character)
		{
			try
			{
				var converter = new RaceToFactionConverter();

				var storageData = GetStoredCharacter(character.Region, character.Realm, character.Name);
				if (storageData == null)
				{
					storageData = new CharacterStorageData();
					storageData.Guid = Guid.NewGuid();
				}
				storageData.Region = character.Region;
				storageData.Realm = character.Realm;
				storageData.Character = character.Name;
				if (character.Guild != null)
				{
					storageData.Guild = character.Guild.Name;
				}
				storageData.Thumbnail = character.Thumbnail;
				storageData.Level = character.Level;
				storageData.Gender = character.Gender;
				storageData.Class = (CharacterClass)character.Class;
				storageData.Faction = (CharacterFaction)(int)converter.Convert(character.Race, typeof(Int32), null, CultureInfo.CurrentCulture);
				storageData.Race = (CharacterRace)character.Race;

				using (var store = IsolatedStorageFile.GetUserStoreForApplication())
				{
					if (!store.DirectoryExists(CHARACTER_STORAGE_PATH))
					{
						store.CreateDirectory(CHARACTER_STORAGE_PATH);
					}

					var stream = new MemoryStream();
					var serializer = new XmlSerializer(typeof(Character));
					serializer.Serialize(stream, character);
					stream.Seek(0, SeekOrigin.Begin);
					using (stream)
					{
						var file = store.CreateFile(String.Format("{0}\\{1}.xml", CHARACTER_STORAGE_PATH, storageData.Guid));
						const int readChunk = 1024 * 1024;
						const int writeChunk = 1024 * 1024;
						var buffer = new byte[readChunk];
						while (true)
						{
							var read = stream.Read(buffer, 0, readChunk);
							if (read <= 0)
							{
								break;
							}

							var write = read;
							while (write > 0)
							{
								file.Write(buffer, 0, Math.Min(write, writeChunk));
								write -= Math.Min(write, writeChunk);
							}
						}
						file.Close();
					}
				}

				StoredCharacters.Add(storageData);
			}
			catch (Exception ex)
			{
				// TODO add some error message
				return;
			}
		}

		/// <summary>
		/// Unstores the specified character from the phone.
		/// </summary>
		/// <param name="character">The character to unstore.</param>
		public static void UnstoreCharacter(Character character)
		{
			try
			{
				var storageData = GetStoredCharacter(character.Region, character.Realm, character.Name);
				if (storageData == null)
				{
					return;
				}

				using (var store = IsolatedStorageFile.GetUserStoreForApplication())
				{
					if (store.FileExists(String.Format("{0}\\{1}.xml", CHARACTER_STORAGE_PATH, storageData.Guid)))
					{
						store.DeleteFile(String.Format("{0}\\{1}.xml", CHARACTER_STORAGE_PATH, storageData.Guid));
					}
				}

				StoredCharacters.Remove(storageData);
			}
			catch (Exception ex)
			{
				// TODO add some error message
				return;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}