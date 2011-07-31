using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Models.Bars;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class CharacterBars
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public HealthBar Health { get; set; }
		[DataMember]
		public SecondBar SecondBar { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterBars"/> class.
		/// </summary>
		public CharacterBars()
		{
		}

		public CharacterBars( XElement root )
		{
			Health = root.Elements( "health" ).Select( element => new HealthBar { Effective = (int)element.GetAttributeValue( "effective", ConvertToType.Int ) } ).FirstOrDefault();
			SecondBar = root.Elements( "secondBar" ).Select( element => new SecondBar
			                                                            {
																			Casting = (int)element.GetAttributeValue( "casting", ConvertToType.Int ),
																			Effective = (int)element.GetAttributeValue( "effective", ConvertToType.Int ),
																			NotCasting = (int)element.GetAttributeValue( "notCasting", ConvertToType.Int ),
																			Type = element.GetAttributeValue( "type" )
			                                                            } ).FirstOrDefault();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
