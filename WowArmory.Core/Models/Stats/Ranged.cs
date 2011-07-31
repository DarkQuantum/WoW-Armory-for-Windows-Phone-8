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
	public class Ranged
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public RangedWeaponSkill WeaponSkill { get; set; }
		[DataMember]
		public RangedDamage Damage { get; set; }
		[DataMember]
		public RangedSpeed Speed { get; set; }
		[DataMember]
		public RangedPower Power { get; set; }
		[DataMember]
		public RangedHitRating HitRating { get; set; }
		[DataMember]
		public RangedCritChance CritChance { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="Ranged"/> class.
		/// </summary>
		public Ranged()
		{
		}

		public Ranged( XElement root )
		{
			WeaponSkill = root.Elements( "weaponSkill" ).Select( e => new RangedWeaponSkill
																	  {
																		  Rating = (int)e.GetAttributeValue( "rating", ConvertToType.Int ),
																		  Value = (int)e.GetAttributeValue( "value", ConvertToType.Int )
																	  } ).FirstOrDefault();

			Damage = root.Elements( "damage" ).Select( e => new RangedDamage
																 {
																	 Dps = (double)e.GetAttributeValue( "dps", ConvertToType.Double ),
																	 Max = (int)e.GetAttributeValue( "max", ConvertToType.Int ),
																	 Min = (int)e.GetAttributeValue( "min", ConvertToType.Int ),
																	 Percent = (int)e.GetAttributeValue( "percent", ConvertToType.Int ),
																	 Speed = (double)e.GetAttributeValue( "speed", ConvertToType.Double )
																 } ).FirstOrDefault();

			Speed = root.Elements( "speed" ).Select( e => new RangedSpeed
																{
																	HastePercent = (double)e.GetAttributeValue( "hastePercent", ConvertToType.Double ),
																	HasteRating = (double)e.GetAttributeValue( "hasteRating", ConvertToType.Double ),
																	Value = (double)e.GetAttributeValue( "value", ConvertToType.Double )
																} ).FirstOrDefault();

			Power = root.Elements( "power" ).Select( e => new RangedPower
														  {
															  Base = (int)e.GetAttributeValue( "base", ConvertToType.Int ),
															  Effective = (int)e.GetAttributeValue( "effective", ConvertToType.Int ),
															  IncreasedDps = (double)e.GetAttributeValue( "increasedDps", ConvertToType.Double ),
															  PetAttack = (double)e.GetAttributeValue( "petAttack", ConvertToType.Double ),
															  PetSpell = (double)e.GetAttributeValue( "petSpell", ConvertToType.Double )
														  } ).FirstOrDefault();

			HitRating = root.Elements( "hitRating" ).Select( e => new RangedHitRating
																  {
																	  IncreasedHitPercent = (double)e.GetAttributeValue( "increasedHitPercent", ConvertToType.Double ),
																	  Penetration = (int)e.GetAttributeValue( "penetration", ConvertToType.Int ),
																	  ReducedArmorPercent = (double)e.GetAttributeValue( "reducedArmorPercent", ConvertToType.Double ),
																	  Value = (int)e.GetAttributeValue( "value", ConvertToType.Int )
																  } ).FirstOrDefault();

			CritChance = root.Elements( "critChance" ).Select( e => new RangedCritChance
																	{
																		Percent = (double)e.GetAttributeValue( "percent", ConvertToType.Double ),
																		PlusPercent = (double)e.GetAttributeValue( "plusPercent", ConvertToType.Double ),
																		Rating = (int)e.GetAttributeValue( "rating", ConvertToType.Int )
																	} ).FirstOrDefault();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
