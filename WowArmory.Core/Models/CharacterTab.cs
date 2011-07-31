using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class CharacterTab
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public CharacterStats Stats { get; set; }
		[DataMember]
		public CharacterItems Items { get; set; }
		[DataMember]
		public CharacterBars Bars { get; set; }
		[DataMember]
		public CharacterTalentSpecs TalentSpecs { get; set; }
		[DataMember]
		public CharacterProfessions Professions { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterTab"/> class.
		/// </summary>
		public CharacterTab()
		{
		}

		public CharacterTab( XElement root, Region region )
		{
			Stats = new CharacterStats( root );
			Items = root.Elements( "items" ).Select( e => new CharacterItems( e, region ) ).FirstOrDefault();
			Bars = root.Elements( "characterBars" ).Select( e => new CharacterBars( e ) ).FirstOrDefault();
			TalentSpecs = root.Elements( "talentSpecs" ).Select( e => new CharacterTalentSpecs( e, region ) ).FirstOrDefault();
			Professions = new CharacterProfessions( root );
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
