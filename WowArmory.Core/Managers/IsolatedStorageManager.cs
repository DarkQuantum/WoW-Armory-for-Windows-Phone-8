using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Converters;
using WowArmory.Core.Models;
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

		/// <summary>
		/// Gets or sets the stored guilds.
		/// </summary>
		/// <value>
		/// The stored guilds.
		/// </value>
		public static ObservableCollection<GuildStorageData> StoredGuilds
		{
			get
			{
				return GetValue("Storage_Guilds", new ObservableCollection<GuildStorageData>());
			}
			set
			{
				SetValue("Storage_Guilds", value);
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
		/// Determines whether the specified guild is already stored.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="realm">The realm.</param>
		/// <param name="name">The name of the guild.</param>
		/// <returns>
		///   <c>true</c> if the specified guild is already stored; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsGuildStored(Region region, string realm, string name)
		{
			return GetStoredGuild(region, realm, name) != null;
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
		/// Try to get the specified guild from the storage.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="realm">The realm.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static GuildStorageData GetStoredGuild(Region region, string realm, string name)
		{
			return StoredGuilds.Where(g => g.Region == region && g.Realm.Equals(realm, StringComparison.CurrentCultureIgnoreCase) && g.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
		}

		/// <summary>
		/// Stores the specified character.
		/// </summary>
		/// <param name="character">The character to store.</param>
		public static void Store(Character character)
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
				else
				{
					StoredCharacters.Remove(storageData);
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
				storageData.AchievementPoints = character.AchievementPoints;

				StoredCharacters.Add(storageData);
			}
			catch (Exception ex)
			{
				// TODO add some error message
				return;
			}
		}

		/// <summary>
		/// Stores the specified guild.
		/// </summary>
		/// <param name="guild">The guild to store.</param>
		public static void Store(Guild guild)
		{
			try
			{
				var storageData = GetStoredGuild(guild.Region, guild.Realm, guild.Name);
				if (storageData == null)
				{
					storageData = new GuildStorageData();
					storageData.Guid = Guid.NewGuid();
				}
				else
				{
					StoredGuilds.Remove(storageData);
				}
				storageData.Region = guild.Region;
				storageData.Realm = guild.Realm;
				storageData.Name = guild.Name;
				storageData.Level = guild.Level;
				storageData.Side = guild.Side;
				storageData.AchievementPoints = guild.AchievementPoints;
				storageData.Members = guild.Members;

				StoredGuilds.Add(storageData);
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
		public static void Unstore(Character character)
		{
			try
			{
				var storageData = GetStoredCharacter(character.Region, character.Realm, character.Name);
				if (storageData == null)
				{
					return;
				}

				StoredCharacters.Remove(storageData);
			}
			catch (Exception ex)
			{
				// TODO add some error message
				return;
			}
		}

		/// <summary>
		/// Unstores the specified guild from the phone.
		/// </summary>
		/// <param name="guild">The guild to unstore.</param>
		public static void Unstore(Guild guild)
		{
			try
			{
				var storageData = GetStoredGuild(guild.Region, guild.Realm, guild.Name);
				if (storageData == null)
				{
					return;
				}

				StoredGuilds.Remove(storageData);
			}
			catch (Exception ex)
			{
				// TODO add some error message
				return;
			}
		}

		/// <summary>
		/// Gets the encryption keys.
		/// </summary>
		/// <returns></returns>
		public static bool GetEncryptionKeys(out EncryptionData encryptionData)
		{
			encryptionData = null;

			try
			{
				var xmlSerializer = new XmlSerializer(typeof(EncryptionData));
				var resource = Application.GetResourceStream(new Uri("/WowArmory.Core;Component/Encryption.key", UriKind.Relative));
				using (var streamReader = new StreamReader(resource.Stream))
				{
					var text = streamReader.ReadToEnd();
					var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(text));
					encryptionData = (EncryptionData)xmlSerializer.Deserialize(memoryStream);
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Gets the authentication keys.
		/// </summary>
		/// <param name="authenticationData">The authentication data.</param>
		/// <returns></returns>
		public static bool GetAuthenticationKeys(out AuthenticationData authenticationData)
		{
			authenticationData = null;

			try
			{
				var xmlSerializer = new XmlSerializer(typeof(AuthenticationData));
				var resource = Application.GetResourceStream(new Uri("/WowArmory.Core;Component/Authentication.key", UriKind.Relative));
				using (var streamReader = new StreamReader(resource.Stream))
				{
					var text = streamReader.ReadToEnd();
					var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(text));
					authenticationData = (AuthenticationData)xmlSerializer.Deserialize(memoryStream);
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}