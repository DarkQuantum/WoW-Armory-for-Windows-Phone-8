using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class CharacterTalentSpecs
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public CharacterTalentSpec Primary { get; set; }
		[DataMember]
		public CharacterTalentSpec Secondary { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterTalentSpecs"/> class.
		/// </summary>
		public CharacterTalentSpecs()
		{
		}

		public CharacterTalentSpecs( XElement root, Region region )
		{
			var specs = root.Elements( "talentSpec" );
			if ( specs != null )
			{
				Primary = specs.Where( e => e.GetAttributeValue( "group" ).Equals( "1" ) ).Select( e => new CharacterTalentSpec( e, region ) ).FirstOrDefault();
				Secondary = specs.Where( e => e.GetAttributeValue( "group" ).Equals( "2" ) ).Select( e => new CharacterTalentSpec( e, region ) ).FirstOrDefault();
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
