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
	public class Melee
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public MeleeMainHandDamage MainHandDamage { get; set; }
		[DataMember]
		public MeleeOffHandDamage OffHandDamage { get; set; }
		[DataMember]
		public MeleeMainHandSpeed MainHandSpeed { get; set; }
		[DataMember]
		public MeleeOffHandSpeed OffHandSpeed { get; set; }
		[DataMember]
		public MeleePower Power { get; set; }
		[DataMember]
		public MeleeHitRating HitRating { get; set; }
		[DataMember]
		public MeleeCritChance CritChance { get; set; }
		[DataMember]
		public MeleeExpertise Expertise { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="Melee"/> class.
		/// </summary>
		public Melee()
		{
		}

		public Melee( XElement root )
		{
			MainHandDamage = root.Elements( "mainHandDamage" ).Select( e => new MeleeMainHandDamage
																			{
																				Dps = (double)e.GetAttributeValue( "dps", ConvertToType.Double ),
																				Max = (int)e.GetAttributeValue( "max", ConvertToType.Int ),
																				Min = (int)e.GetAttributeValue( "min", ConvertToType.Int ),
																				Percent = (double)e.GetAttributeValue( "percent", ConvertToType.Double ),
																				Speed = (double)e.GetAttributeValue( "speed", ConvertToType.Double )
																			} ).FirstOrDefault();

			OffHandDamage = root.Elements( "offHandDamage" ).Select( e => new MeleeOffHandDamage
																		  {
																			  Dps = (double)e.GetAttributeValue( "dps", ConvertToType.Double ),
																			  Max = (int)e.GetAttributeValue( "max", ConvertToType.Int ),
																			  Min = (int)e.GetAttributeValue( "min", ConvertToType.Int ),
																			  Percent = (double)e.GetAttributeValue( "percent", ConvertToType.Double ),
																			  Speed = (double)e.GetAttributeValue( "speed", ConvertToType.Double )
																		  } ).FirstOrDefault();

			MainHandSpeed = root.Elements( "mainHandSpeed" ).Select( e => new MeleeMainHandSpeed
																		  {
																			  HastePercent = (double)e.GetAttributeValue( "hastePercent", ConvertToType.Double ),
																			  HasteRating = (int)e.GetAttributeValue( "hasteRating", ConvertToType.Int ),
																			  Value = (double)e.GetAttributeValue( "value", ConvertToType.Double )
																		  } ).FirstOrDefault();

			OffHandSpeed = root.Elements( "offHandSpeed" ).Select( e => new MeleeOffHandSpeed
																		 {
																			 HastePercent = (double)e.GetAttributeValue( "hastePercent", ConvertToType.Double ),
																			 HasteRating = (int)e.GetAttributeValue( "hasteRating", ConvertToType.Int ),
																			 Value = (double)e.GetAttributeValue( "value", ConvertToType.Double )
																		 } ).FirstOrDefault();

			Power = root.Elements( "power" ).Select( e => new MeleePower
																  {
																	  Base = (int)e.GetAttributeValue( "base", ConvertToType.Int ),
																	  Effective = (int)e.GetAttributeValue( "effective", ConvertToType.Int ),
																	  IncreasedDps = (double)e.GetAttributeValue( "increasedDps", ConvertToType.Double )
																  } ).FirstOrDefault();

			HitRating = root.Elements( "hitRating" ).Select( e => new MeleeHitRating
																	  {
																		  IncreasedHitPercent = (double)e.GetAttributeValue( "increasedHitPercent", ConvertToType.Double ),
																		  Penetration = (int)e.GetAttributeValue( "penetration", ConvertToType.Int ),
																		  ReducedArmorPercent = (double)e.GetAttributeValue( "reducedArmorPercent", ConvertToType.Double ),
																		  Value = (int)e.GetAttributeValue( "value", ConvertToType.Int )
																	  } ).FirstOrDefault();

			CritChance = root.Elements( "critChance" ).Select( e => new MeleeCritChance
																	   {
																		   Percent = (double)e.GetAttributeValue( "percent", ConvertToType.Double ),
																		   PlusPercent = (double)e.GetAttributeValue( "plusPercent", ConvertToType.Double ),
																		   Rating = (int)e.GetAttributeValue( "rating", ConvertToType.Int )
																	   } ).FirstOrDefault();

			Expertise = root.Elements( "expertise" ).Select( e => new MeleeExpertise
																	   {
																		   Additional = (int)e.GetAttributeValue( "additional", ConvertToType.Int ),
																		   Percent = (double)e.GetAttributeValue( "percent", ConvertToType.Double ),
																		   Rating = (int)e.GetAttributeValue( "rating", ConvertToType.Int ),
																		   Value = (int)e.GetAttributeValue( "value", ConvertToType.Int )
																	   } ).FirstOrDefault();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
