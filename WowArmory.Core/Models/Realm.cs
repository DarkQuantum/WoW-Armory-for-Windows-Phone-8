using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class Realm
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public RealmLanguage Language { get; set; }
		[DataMember]
		public RealmType Type { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="Realm"/> class.
		/// </summary>
		public Realm()
		{
		}

		public Realm( XElement root, Region region )
		{
			switch ( region )
			{
				case Region.Europe:
					{
						var title = root.Element( "title" );
						if ( title != null )
						{
							Name = title.Value;
						}

						var categories = root.Elements( "category" );
						foreach ( var category in categories )
						{
							var domain = category.GetAttributeValue( "domain" );
							switch ( domain )
							{
								case "language":
									{
										switch ( category.Value )
										{
											case "en":
												{
													Language = RealmLanguage.English;
												} break;
											case "de":
												{
													Language = RealmLanguage.German;
												} break;
											case "fr":
												{
													Language = RealmLanguage.French;
												} break;
											case "ru":
												{
													Language = RealmLanguage.Russian;
												} break;
										}
									} break;
								case "type":
									{
										switch ( category.Value )
										{
											case "PvE":
												{
													Type = RealmType.PvE;
												} break;
											case "PvP":
												{
													Type = RealmType.PvP;
												} break;
											case "RP-PvE":
												{
													Type = RealmType.RpPvE;
												} break;
											case "RP-PvP":
												{
													Type = RealmType.RpPvP;
												} break;
										}
									} break;
							}
						}
					} break;
				case Region.USA:
					{
						Language = RealmLanguage.English;

						var title = root.Element( "title" );
						if ( title != null )
						{
							var name = title.Value.Substring( 0, title.Value.IndexOf( "(" ) ).Trim();
							Name = name;
							var type = title.Value.Substring( title.Value.IndexOf( "(" ), title.Value.IndexOf( ")" ) - title.Value.IndexOf( "(" ) ).Trim();
							switch ( type )
							{
								case "PVP":
									{
										Type = RealmType.PvP;
									} break;
								case "PVE":
									{
										Type = RealmType.PvE;
									} break;
								case "RP":
									{
										Type = RealmType.Rp;
									} break;
								case "Unknown":
									{
										Type = RealmType.Unknown;
									} break;
							}
						}
					} break;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
