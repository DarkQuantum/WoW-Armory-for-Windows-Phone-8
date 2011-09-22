using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;
using WowArmory.Core.BattleNet.Helpers;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Extensions;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;

namespace WowArmory.Core.BattleNet
{
	public class BattleNetClient
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private static BattleNetClient _instance;
		private Dictionary<CharacterFaction, List<CharacterRace>> _factionRaces;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the singleton for this instance.
		/// </summary>
		public static BattleNetClient Current
		{
			get { return _instance ?? (_instance = new BattleNetClient(AppSettingsManager.Region, CultureInfo.CurrentUICulture)); }
		}

		/// <summary>
		/// Gets the faction races.
		/// </summary>
		public Dictionary<CharacterFaction, List<CharacterRace>> FactionRaces
		{
			get { return _factionRaces; }
		}

		/// <summary>
		/// Gets or sets the Battle.Net region used to retrieve data from.
		/// </summary>
		/// <value>
		/// The Battle.Net region used to retrieve data from.
		/// </value>
		public Region Region { get; set; }

		/// <summary>
		/// Gets or sets the locale in which the data is retrieved.
		/// </summary>
		/// <value>
		/// The locale in which the data is retrieved.
		/// </value>
		public CultureInfo Locale { get; set; }

		/// <summary>
		/// Gets the Battle.Net region code used in web requests.
		/// </summary>
		public string BattleNetRegionCode
		{
			get
			{
				var regionCode = BattleNetSettings.ResourceManager.GetString(String.Format("BattleNet_Region_{0}", Region));
				return !String.IsNullOrEmpty(regionCode) ? regionCode : "us";
			}
		}

		/// <summary>
		/// Gets the Battle.Net base URI.
		/// </summary>
		public Uri BattleNetBaseUri
		{
			get
			{
				var baseUriTemplate = BattleNetSettings.ResourceManager.GetString("BattleNet_BaseUri");
				return !String.IsNullOrEmpty(baseUriTemplate) ? new Uri(String.Format(baseUriTemplate, BattleNetRegionCode)) : new Uri(String.Format("http://{0}.battle.net/api/wow/", BattleNetRegionCode));
			}
		}

		/// <summary>
		/// Gets the Battle.Net icon URI.
		/// </summary>
		public Uri BattleNetIconUri
		{
			get
			{
				var baseUriTemplate = BattleNetSettings.ResourceManager.GetString("BattleNet_IconUri");
				return !String.IsNullOrEmpty(baseUriTemplate) ? new Uri(String.Format(baseUriTemplate, BattleNetRegionCode)) : new Uri(String.Format("http://{0}.media.blizzard.com/wow/icons/", BattleNetRegionCode));
			}
		}

		/// <summary>
		/// Gets the Battle.Net thumbnail URI.
		/// </summary>
		public Uri BattleNetThumbnailUri
		{
			get
			{
				var baseUriTemplate = BattleNetSettings.ResourceManager.GetString("BattleNet_ThumbnailUri");
				return !String.IsNullOrEmpty(baseUriTemplate) ? new Uri(String.Format(baseUriTemplate, BattleNetRegionCode)) : new Uri(String.Format("http://{0}.battle.net/static-render/{0}/", BattleNetRegionCode));
			}
		}

		/// <summary>
		/// Gets the Battle.Net item render URI.
		/// </summary>
		public Uri BattleNetItemRenderUri
		{
			get
			{
				var baseUriTemplate = BattleNetSettings.ResourceManager.GetString("BattleNet_ItemRenderUri");
				return !String.IsNullOrEmpty(baseUriTemplate) ? new Uri(String.Format(baseUriTemplate, BattleNetRegionCode)) : new Uri(String.Format("http://{0}.media.blizzard.com/wow/renders/items/", BattleNetRegionCode));
			}
		}

		/// <summary>
		/// Gets the battle net guild emblem URI.
		/// </summary>
		public Uri BattleNetGuildEmblemUri
		{
			get
			{
				var baseUriTemplate = BattleNetSettings.ResourceManager.GetString("BattleNet_GuildEmblemUri");
				return !String.IsNullOrEmpty(baseUriTemplate) ? new Uri(String.Format(baseUriTemplate, BattleNetRegionCode)) : new Uri(String.Format("http://{0}.battle.net/wow/static/images/guild/tabards/", BattleNetRegionCode));
			}
		}

		/// <summary>
		/// Gets the battle net profile image URI.
		/// </summary>
		public Uri BattleNetProfileImageUri
		{
			get
			{
				var baseUriTemplate = BattleNetSettings.ResourceManager.GetString("BattleNet_ProfileImageUri");
				return !String.IsNullOrEmpty(baseUriTemplate) ? new Uri(String.Format(baseUriTemplate, BattleNetRegionCode)) : new Uri(String.Format("http://{0}.battle.net/static-render/{0}/{{0}}/{{1}}/{{2}}-profilemain.jpg?alt=/wow/static/images/2d/profilemain/race/{{3}}-{{4}}.jpg", BattleNetRegionCode));
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="BattleNetClient"/> class.
		/// </summary>
		public BattleNetClient(Region region, CultureInfo locale)
		{
			Region = region;
			Locale = locale;

			_factionRaces = new Dictionary<CharacterFaction, List<CharacterRace>>();

			var allianceFactions = new List<CharacterRace>();
			var hordeFactions = new List<CharacterRace>();
			
			allianceFactions.Add(CharacterRace.Draenei);
			allianceFactions.Add(CharacterRace.Dwarf);
			allianceFactions.Add(CharacterRace.Gnome);
			allianceFactions.Add(CharacterRace.Human);
			allianceFactions.Add(CharacterRace.NightElf);
			allianceFactions.Add(CharacterRace.Worgen);
			hordeFactions.Add(CharacterRace.BloodElf);
			hordeFactions.Add(CharacterRace.Goblin);
			hordeFactions.Add(CharacterRace.Orc);
			hordeFactions.Add(CharacterRace.Tauren);
			hordeFactions.Add(CharacterRace.Troll);
			hordeFactions.Add(CharacterRace.Undead);

			_factionRaces.Add(CharacterFaction.Alliance, allianceFactions);
			_factionRaces.Add(CharacterFaction.Horde, hordeFactions);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the basic character information for the specified realm and character name.
		/// </summary>
		/// <param name="realmName">The name of the realm.</param>
		/// <param name="characterName">The name of the character.</param>
		/// <param name="action">The action to execute once the response was received.</param>
		public void GetCharacterAsync(string realmName, string characterName, Action<Character> action)
		{
			GetCharacterAsync(realmName, characterName, CharacterFields.Basic, action);
		}

		/// <summary>
		/// Gets the character information for the specified realm and character name.
		/// </summary>
		/// <param name="realmName">The name of the realm.</param>
		/// <param name="characterName">The name of the character.</param>
		/// <param name="fields">Specifies the information to retrieve for this character.</param>
		/// <param name="action">The action to execute once the response was received.</param>
		public void GetCharacterAsync(string realmName, string characterName, CharacterFields fields, Action<Character> action)
		{
			try
			{
				var fieldsQueryString = BuildCharacterFieldsQueryString(fields);
				var apiMethod = new Uri(BattleNetBaseUri, String.Format(BattleNetSettings.BattleNet_Api_Character, realmName, characterName, fieldsQueryString)).ToString();
				CallApiMethodAsync(apiMethod, jsonResult =>
				{
					try
					{
						var character = JsonConvert.DeserializeObject<Character>(jsonResult);
						if (character != null)
						{
							character.Region = Region;
						}

						action(character);
					}
					catch (Exception ex)
					{
						action(null);
					}
				});
			}
			catch (Exception ex)
			{
				action(null);
			}
		}

		/// <summary>
		/// Gets the basic guild information for the specified realm and guild name.
		/// </summary>
		/// <param name="realmName">The name of the realm.</param>
		/// <param name="guildName">The name of the guild.</param>
		/// <param name="action">The action to execute once the response was received.</param>
		public void GetGuildAsync(string realmName, string guildName, Action<Guild> action)
		{
			GetGuildAsync(realmName, guildName, GuildFields.Basic, action);
		}

		/// <summary>
		/// Gets the guild information for the specified realm and guild name.
		/// </summary>
		/// <param name="realmName">The name of the realm.</param>
		/// <param name="guildName">The name of the guild.</param>
		/// <param name="fields">Specifies the information to retrieve for this guild.</param>
		/// <param name="action">The action to execute once the response was received.</param>
		public void GetGuildAsync(string realmName, string guildName, GuildFields fields, Action<Guild> action)
		{
			try
			{
				var fieldsQueryString = BuildGuildFieldsQueryString(fields);
				var apiMethod = new Uri(BattleNetBaseUri, String.Format(BattleNetSettings.BattleNet_Api_Guild, realmName, guildName, fieldsQueryString)).ToString();
				CallApiMethodAsync(apiMethod, jsonResult =>
				{
					try
					{
						var guild = JsonConvert.DeserializeObject<Guild>(jsonResult);
						if (guild != null)
						{
							guild.Region = Region;
						}
						action(guild);
					}
					catch (Exception ex)
					{
						action(null);
					}
				});
			}
			catch (Exception ex)
			{
				action(null);
			}
		}

		/// <summary>
		/// Gets the realm list for the specified region.
		/// </summary>
		/// <param name="region">The region to receive the realm list for.</param>
		/// <param name="action">The action to execute once the response was received.</param>
		public void GetRealmListAsync(Region region, Action<RealmList> action)
		{
			try
			{
				var oldRegion = Region;
				Region = region;
				var apiMethod = new Uri(BattleNetBaseUri, BattleNetSettings.BattleNet_Api_RealmStatus).ToString();
				Region = oldRegion;
				CallApiMethodAsync(apiMethod, jsonResult =>
				{
					try
					{
						var realmList = JsonConvert.DeserializeObject<RealmList>(jsonResult.Replace("n/a", "notavailable"));
						if (realmList != null)
						{
							realmList.Region = Region;
						}
						action(realmList);
					}
					catch (Exception ex)
					{
						action(null);
					}
				});
			}
			catch (Exception ex)
			{
				action(null);
			}
		}

		/// <summary>
		/// Gets the item for the specified id.
		/// </summary>
		/// <param name="itemId">The item id.</param>
		/// <param name="action">The action to execute once the response was received.</param>
		public void GetItemAsync(int itemId, Action<Item> action)
		{
			try
			{
				var apiMethod = new Uri(BattleNetBaseUri, String.Format(BattleNetSettings.BattleNet_Api_Item, itemId)).ToString();
				CallApiMethodAsync(apiMethod, jsonResult =>
				{
					try
					{
						var item = JsonConvert.DeserializeObject<Item>(jsonResult);
						action(item);
					}
					catch (Exception ex)
					{
						action(null);
					}
				});
			}
			catch (Exception ex)
			{
				action(null);
			}
		}

		/// <summary>
		/// Gets the quest for the specified id.
		/// </summary>
		/// <param name="questId">The quest id.</param>
		/// <param name="action">The action to execute once the response was received.</param>
		public void GetQuestAsync(int questId, Action<Quest> action)
		{
			try
			{
				var apiMethod = new Uri(BattleNetBaseUri, String.Format(BattleNetSettings.BattleNet_Api_Quest, questId)).ToString();
				CallApiMethodAsync(apiMethod, jsonResult =>
				{
					try
					{
						var quest = JsonConvert.DeserializeObject<Quest>(jsonResult);
						action(quest);
					}
					catch (Exception ex)
					{
						action(null);
					}
				});
			}
			catch (Exception ex)
			{
				action(null);
			}
		}

		/// <summary>
		/// Gets the guild perks.
		/// </summary>
		/// <param name="action">The action to execute once the response was received.</param>
		public void GetGuildPerksAsync(Action<GuildPerks> action)
		{
			try
			{
				var apiMethod = new Uri(BattleNetBaseUri, BattleNetSettings.BattleNet_Api_GuildPerks).ToString();
				CallApiMethodAsync(apiMethod, jsonResult =>
				{
					try
					{
						var guildPerks = JsonConvert.DeserializeObject<GuildPerks>(jsonResult);
						action(guildPerks);
					}
					catch (Exception ex)
					{
						action(null);
					}
				});
			}
			catch (Exception ex)
			{
				action(null);
			}
		}

		/// <summary>
		/// Gets the thumbnail url for the specified path.
		/// </summary>
		/// <param name="thumbnailPath">The path to the thumbnail.</param>
		/// <returns>
		/// The thumbnail url for the specified path.
		/// </returns>
		public string GetThumbnailUrl(string thumbnailPath)
		{
			return new Uri(BattleNetThumbnailUri, thumbnailPath).ToString();
		}

		/// <summary>
		/// Gets the thumbnail from the specified path.
		/// </summary>
		/// <param name="thumbnailPath">The path to the thumbnail.</param>
		/// <returns>
		/// The thumbnail from the specified path.
		/// </returns>
		public ImageSource GetThumbnail(string thumbnailPath)
		{
			return CacheManager.GetImageSourceFromCache(GetThumbnailUrl(thumbnailPath));
		}

		/// <summary>
		/// Gets the icon url for the specified path.
		/// </summary>
		/// <param name="iconPath">The path to the icon.</param>
		/// <param name="iconSize">The desired size of the icon.</param>
		/// <returns>The icon url for the specified path.</returns>
		public string GetIconUrl(string iconPath, IconSize iconSize = IconSize.Large)
		{
			return new Uri(BattleNetIconUri, String.Format("{0}/{1}.jpg", GetSettingValue(iconSize), iconPath)).ToString();
		}

		/// <summary>
		/// Gets the icon from the specified path.
		/// </summary>
		/// <param name="iconPath">The path to the icon.</param>
		/// <param name="iconSize">The desired size of the icon.</param>
		/// <returns>
		/// The icon from the specified path.
		/// </returns>
		public ImageSource GetIcon(string iconPath, IconSize iconSize = IconSize.Large)
		{
			var iconUrl = GetIconUrl(iconPath, iconSize);
			return CacheManager.GetImageSourceFromCache(iconUrl);
		}

		/// <summary>
		/// Gets the item render url for the specified item.
		/// </summary>
		/// <param name="itemId">The item id.</param>
		/// <returns></returns>
		public string GetItemRenderUrl(int itemId)
		{
			return String.Format("{0}item{1}.jpg", BattleNetItemRenderUri, itemId);
		}

		/// <summary>
		/// Gets the guild emblem url for the specified emblem path.
		/// </summary>
		/// <param name="emblemPath">The emblem path.</param>
		/// <returns></returns>
		public string GetGuildEmblemUrl(string emblemPath)
		{
			return String.Format("{0}{1}.png", BattleNetGuildEmblemUri, emblemPath);
		}

		/// <summary>
		/// Gets the profile image URL for the specified character.
		/// </summary>
		/// <param name="character">The character.</param>
		/// <returns></returns>
		public string GetProfileImageUrl(Character character)
		{
			var regex = new Regex("([a-z-]*)/([0-9]*)/([0-9]*)-avatar.jpg");
			var matches = regex.Matches(character.Thumbnail);
			if (matches.Count == 1)
			{
				var realm = matches[0].Groups[1].Value;
				var key1 = matches[0].Groups[2].Value;
				var key2 = matches[0].Groups[3].Value;

				return String.Format(BattleNetProfileImageUri.ToString(), realm, key1, key2, character.Race, character.Gender);
			}

			return String.Empty;
		}

		/// <summary>
		/// Builds the character fields query string from the specified fields object.
		/// </summary>
		/// <param name="fields">The character fields object to build the query string from.</param>
		/// <returns>
		/// The character fields query string from the specified fields object.
		/// </returns>
		internal string BuildCharacterFieldsQueryString(CharacterFields fields)
		{
			var fieldQueryString = fields.GetValues().Cast<CharacterFields>().Where(value => value != CharacterFields.All && (fields & value) == value).Aggregate(String.Empty, (current, value) => String.Format("{0}{1}{2}", current, !String.IsNullOrEmpty(current) ? "," : String.Empty, EnumHelper.GetApiUrlFieldName(value)));
			return !String.IsNullOrEmpty(fieldQueryString) ? String.Format("?fields={0}", fieldQueryString) : String.Empty;
		}

		/// <summary>
		/// Builds the guild fields query string from the specified fields object.
		/// </summary>
		/// <param name="fields">The guild fields object to build the query string from.</param>
		/// <returns>
		/// The guild fields query string from the specified fields object.
		/// </returns>
		internal string BuildGuildFieldsQueryString(GuildFields fields)
		{
			var fieldQueryString = fields.GetValues().Cast<GuildFields>().Where(value => value != GuildFields.All && (fields & value) == value).Aggregate(String.Empty, (current, value) => String.Format("{0}{1}{2}", current, !String.IsNullOrEmpty(current) ? "," : String.Empty, EnumHelper.GetApiUrlFieldName(value)));
			return !String.IsNullOrEmpty(fieldQueryString) ? String.Format("?fields={0}", fieldQueryString) : String.Empty;
		}

		/// <summary>
		/// Handler which asynchronously calls the specified api method.
		/// </summary>
		/// <param name="apiMethod">The API method to call.</param>
		/// <param name="action">The json result action.</param>
		internal void CallApiMethodAsync(string apiMethod, Action<string> action)
		{
			try
			{
				var apiMethodLocalized = String.Format("{0}{1}locale={2}", apiMethod, apiMethod.Contains("?") ? "&" : "?", Locale.Name.Replace("-", "_").ToLower());
				var request = (HttpWebRequest)WebRequest.Create(apiMethodLocalized);
				request.Headers["user-agent"] = BattleNetSettings.WebRequest_Header_UserAgent;
				request.AllowAutoRedirect = true;

				//if (AuthenticationManager.UseAuthentication)
				//{
				//    var date = DateTime.Now.ToUniversalTime();
				//    var dateString = date.ToString("r");
				//    //var type = request.Headers.GetType();
				//    //var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
				//    //var methodInfo = type.GetMethod("AddWithoutValidate", bindingFlags);
				//    //methodInfo.Invoke(request.Headers, new[] { "Date", date.ToString("r") });

				//    var stringToSign = String.Format("{0}\n{1}\n{2}\n", request.Method, dateString, UrlPathEncode(request.RequestUri.LocalPath));
				//    var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(AuthenticationManager.PrivateKey));
				//    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
				//    var signature = Convert.ToBase64String(hash);
				//    var authorization = String.Format("BNET {0}:{1}", AuthenticationManager.PublicKey, signature);
				//    request.Headers["Authorization"] = authorization;
				//}

				request.BeginGetResponse(delegate(IAsyncResult result)
				{
					try
					{
						WebResponse response;

						// in some cases the Battle.Net community api may return an exception (404/500 error)
						// we just ignore it and try to read the content of the response
						try
						{
							response = request.EndGetResponse(result);
						}
						catch (WebException ex)
						{
							response = ex.Response;
						}

						using (var stream = response.GetResponseStream())
						{
							var streamReader = new StreamReader(stream);
							var jsonResultString = streamReader.ReadToEnd();
							
							var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResultString);
							if (apiResponse == null)
							{
								Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(AppResources.UI_Common_Error_NoData_Text, AppResources.UI_Common_Error_NoData_Caption, MessageBoxButton.OK));
								jsonResultString = String.Empty;
							}
							else
							{
								if (!apiResponse.IsValid)
								{
									var reasonCaption = AppResources.ResourceManager.GetString(String.Format("UI_ApiResponseError_{0}_Caption", apiResponse.ReasonType)) ?? AppResources.UI_Common_Error_NoData_Caption;
									var reasonText = AppResources.ResourceManager.GetString(String.Format("UI_ApiResponseError_{0}_Text", apiResponse.ReasonType)) ?? AppResources.UI_Common_Error_NoData_Text;
									Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(reasonText, reasonCaption, MessageBoxButton.OK));
									jsonResultString = String.Empty;
								}
							}

							Deployment.Current.Dispatcher.BeginInvoke(() => action(jsonResultString));
						}
					}
					catch (Exception ex)
					{
						Deployment.Current.Dispatcher.BeginInvoke(() => action(null));
					}
				}, null);
			}
			catch(Exception ex)
			{
				action(null);
			}
		}

		/// <summary>
		/// Encodes the specified url path the way the Battle.Net Community API needs it.
		/// </summary>
		/// <param name="urlPath">The URL path.</param>
		/// <returns></returns>
		private string UrlPathEncode(string urlPath)
		{
			var urlPathBytes = Encoding.UTF8.GetBytes(urlPath);
			var encodedString = String.Empty;
			foreach (var urlPathByte in urlPathBytes)
			{
				if (urlPathByte <= 0x20 || urlPathByte > 0x7f)
				{
					encodedString = String.Format("{0}%{1:X2}", encodedString, urlPathByte);
				}
				else
				{
					encodedString = String.Format("{0}{1}", encodedString, (char)urlPathByte);
				}
			}

			return encodedString;
		}

		/// <summary>
		/// Gets the configured setting for the specified icon size.
		/// </summary>
		/// <param name="iconSize">The icon size to retrieve the configured setting for.</param>
		/// <returns>
		/// The configured setting for the specified icon size.
		/// </returns>
		internal string GetSettingValue(IconSize iconSize)
		{
			var resourceName = String.Format("BattleNet_IconSize_{0}", iconSize);
			return BattleNetSettings.ResourceManager.GetString(resourceName);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}