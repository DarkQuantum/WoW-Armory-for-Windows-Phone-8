namespace WowArmory.ViewModels
{
	public class ViewModelLocator
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private static MainViewModel _main;
		private static SettingsViewModel _settings;
		private static HelpViewModel _help;
		private static NewsViewModel _news;
		private static RealmListViewModel _realmList;
		private static CharacterListViewModel _characterList;
		private static CharacterSearchViewModel _characterSearch;
		private static CharacterDetailsViewModel _characterDetails;
		private static GuildListViewModel _guildList;
		private static GuildSearchViewModel _guildSearch;
		private static GuildDetailsViewModel _guildDetails;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the main view model.
		/// </summary>
		public MainViewModel Main
		{
			get { return MainStatic; }
		}

		/// <summary>
		/// Gets the static main view model.
		/// </summary>
		public static MainViewModel MainStatic
		{
			get { return _main ?? (_main = new MainViewModel()); }
		}

		/// <summary>
		/// Gets the settings view model.
		/// </summary>
		public SettingsViewModel Settings
		{
			get { return SettingsStatic; }
		}

		/// <summary>
		/// Gets the static settings view model.
		/// </summary>
		public static SettingsViewModel SettingsStatic
		{
			get { return _settings ?? (_settings = new SettingsViewModel()); }
		}

		/// <summary>
		/// Gets the help view model.
		/// </summary>
		public HelpViewModel Help
		{
			get { return HelpStatic; }
		}

		/// <summary>
		/// Gets the static settings view model.
		/// </summary>
		public static HelpViewModel HelpStatic
		{
			get { return _help ?? (_help = new HelpViewModel()); }
		}

		/// <summary>
		/// Gets the settings view model.
		/// </summary>
		public NewsViewModel News
		{
			get { return NewsStatic; }
		}

		/// <summary>
		/// Gets the static settings view model.
		/// </summary>
		public static NewsViewModel NewsStatic
		{
			get { return _news ?? (_news = new NewsViewModel()); }
		}

		/// <summary>
		/// Gets the realm list view model.
		/// </summary>
		public RealmListViewModel RealmList
		{
			get { return RealmListStatic; }
		}

		/// <summary>
		/// Gets the static realm list view model.
		/// </summary>
		public static RealmListViewModel RealmListStatic
		{
			get { return _realmList ?? (_realmList = new RealmListViewModel()); }
		}

		/// <summary>
		/// Gets the character list view model.
		/// </summary>
		public CharacterListViewModel CharacterList
		{
			get { return CharacterListStatic; }
		}

		/// <summary>
		/// Gets the static character list view model.
		/// </summary>
		public static CharacterListViewModel CharacterListStatic
		{
			get { return _characterList ?? (_characterList = new CharacterListViewModel()); }
		}

		/// <summary>
		/// Gets the character search view model.
		/// </summary>
		public CharacterSearchViewModel CharacterSearch
		{
			get { return CharacterSearchStatic; }
		}

		/// <summary>
		/// Gets the static character search view model.
		/// </summary>
		public static CharacterSearchViewModel CharacterSearchStatic
		{
			get { return _characterSearch ?? (_characterSearch = new CharacterSearchViewModel()); }
		}

		/// <summary>
		/// Gets the character details view model.
		/// </summary>
		public CharacterDetailsViewModel CharacterDetails
		{
			get { return CharacterDetailsStatic; }
		}

		/// <summary>
		/// Gets the static character details view model.
		/// </summary>
		public static CharacterDetailsViewModel CharacterDetailsStatic
		{
			get { return _characterDetails ?? (_characterDetails = new CharacterDetailsViewModel()); }
		}

		/// <summary>
		/// Gets the guild list view model.
		/// </summary>
		public GuildListViewModel GuildList
		{
			get { return GuildListStatic; }
		}

		/// <summary>
		/// Gets the static guild list view model.
		/// </summary>
		public static GuildListViewModel GuildListStatic
		{
			get { return _guildList ?? (_guildList = new GuildListViewModel()); }
		}

		/// <summary>
		/// Gets the guild search view model.
		/// </summary>
		public GuildSearchViewModel GuildSearch
		{
			get { return GuildSearchStatic; }
		}

		/// <summary>
		/// Gets the static guild search view model.
		/// </summary>
		public static GuildSearchViewModel GuildSearchStatic
		{
			get { return _guildSearch ?? (_guildSearch = new GuildSearchViewModel()); }
		}

		/// <summary>
		/// Gets the guild details view model.
		/// </summary>
		public GuildDetailsViewModel GuildDetails
		{
			get { return GuildDetailsStatic; }
		}

		/// <summary>
		/// Gets the static guild details view model.
		/// </summary>
		public static GuildDetailsViewModel GuildDetailsStatic
		{
			get { return _guildDetails ?? (_guildDetails = new GuildDetailsViewModel()); }
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
		/// </summary>
		public ViewModelLocator()
		{
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}