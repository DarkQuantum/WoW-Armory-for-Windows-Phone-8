using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Models.Stats;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class CharacterStats
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public BaseStats BaseStats { get; set; }
		[DataMember]
		public Melee Melee { get; set; }
		[DataMember]
		public Ranged Ranged { get; set; }
		[DataMember]
		public Spell Spell { get; set; }
		[DataMember]
		public Defenses Defenses { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterStats"/> class.
		/// </summary>
		public CharacterStats()
		{
		}

		public CharacterStats( XElement root )
		{
			BaseStats = root.Elements( "baseStats" ).Select( e => new BaseStats( e ) ).FirstOrDefault();
			Melee = root.Elements( "melee" ).Select( e => new Melee( e ) ).FirstOrDefault();
			Ranged = root.Elements( "ranged" ).Select( e => new Ranged( e ) ).FirstOrDefault();
			Spell = root.Elements( "spell" ).Select( e => new Spell( e ) ).FirstOrDefault();
			Defenses = root.Elements( "defenses" ).Select( e => new Defenses( e ) ).FirstOrDefault();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}