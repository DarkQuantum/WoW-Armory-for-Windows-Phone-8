using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public class Spell
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public SpellBonusDamage BonusDamage { get; set; }
		[DataMember]
		public SpellBonusHealing BonusHealing { get; set; }
		[DataMember]
		public SpellHitRating HitRating { get; set; }
		[DataMember]
		public SpellCritChance CritChance { get; set; }
		[DataMember]
		public SpellPenetration Penetration { get; set; }
		[DataMember]
		public SpellManaRegen ManaRegen { get; set; }
		[DataMember]
		public SpellHasteRating HasteRating { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="Spell"/> class.
		/// </summary>
		public Spell()
		{
		}

		public Spell( XElement root )
		{
			BonusDamage = root.Elements( "bonusDamage" ).Select( e => new SpellBonusDamage( e ) ).FirstOrDefault();

			BonusHealing = root.Elements( "bonusHealing" ).Select( e => new SpellBonusHealing
																		{
																			Value = (int)e.GetAttributeValue( "value", ConvertToType.Int )
																		} ).FirstOrDefault();

			HitRating = root.Elements( "hitRating" ).Select( e => new SpellHitRating
																  {
																	  IncreasedHitPercent = (double)e.GetAttributeValue( "increasedHitPercent", ConvertToType.Double ),
																	  Penetration = (int)e.GetAttributeValue( "penetration", ConvertToType.Int ),
																	  ReducedResist = (int)e.GetAttributeValue( "reducedResist", ConvertToType.Int ),
																	  Value = (int)e.GetAttributeValue( "value", ConvertToType.Int )
																  } ).FirstOrDefault();

			CritChance = root.Elements( "critChance" ).Select( e => new SpellCritChance
																	{
																		Rating = (int)e.GetAttributeValue( "rating", ConvertToType.Int ),
																		Arcane = e.Elements( "arcane" ).Select( p => (double)p.GetAttributeValue( "percent", ConvertToType.Double ) ).FirstOrDefault(),
																		Fire = e.Elements( "fire" ).Select( p => (double)p.GetAttributeValue( "percent", ConvertToType.Double ) ).FirstOrDefault(),
																		Frost = e.Elements( "frost" ).Select( p => (double)p.GetAttributeValue( "percent", ConvertToType.Double ) ).FirstOrDefault(),
																		Holy = e.Elements( "holy" ).Select( p => (double)p.GetAttributeValue( "percent", ConvertToType.Double ) ).FirstOrDefault(),
																		Nature = e.Elements( "nature" ).Select( p => (double)p.GetAttributeValue( "percent", ConvertToType.Double ) ).FirstOrDefault(),
																		Shadow = e.Elements( "shadow" ).Select( p => (double)p.GetAttributeValue( "percent", ConvertToType.Double ) ).FirstOrDefault()
																	} ).FirstOrDefault();

			Penetration = root.Elements( "penetration" ).Select( e => new SpellPenetration
																	  {
																		  Value = (int)e.GetAttributeValue( "value", ConvertToType.Int )
																	  } ).FirstOrDefault();

			ManaRegen = root.Elements( "manaRegen" ).Select( e => new SpellManaRegen
																  {
																	  Casting = (double)e.GetAttributeValue( "casting", ConvertToType.Double ),
																	  NotCasting = (double)e.GetAttributeValue( "notCasting", ConvertToType.Double )
																  } ).FirstOrDefault();

			HasteRating = root.Elements( "hasteRating" ).Select( e => new SpellHasteRating
																	  {
																		  HastePercent = (double)e.GetAttributeValue( "hastePercent", ConvertToType.Double ),
																		  HasteRating = (int)e.GetAttributeValue( "hasteRating", ConvertToType.Int )
																	  } ).FirstOrDefault();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
