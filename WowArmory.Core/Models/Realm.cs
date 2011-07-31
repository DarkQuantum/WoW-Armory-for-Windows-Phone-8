// 
// Copyright (c) 2010 Christian Krueger <christian.krueger@krueger-c.com>
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge and for non-commercial usage
// only, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify,
// merge, publish, and/or distribute copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

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
