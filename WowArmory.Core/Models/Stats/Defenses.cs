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
	public class Defenses
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public DefensesArmor Armor { get; set; }
		[DataMember]
		public DefensesDefense Defense { get; set; }
		[DataMember]
		public DefensesDodge Dodge { get; set; }
		[DataMember]
		public DefensesParry Parry { get; set; }
		[DataMember]
		public DefensesBlock Block { get; set; }
		[DataMember]
		public DefensesResilience Resilience { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="Defenses"/> class.
		/// </summary>
		public Defenses()
		{
		}

		public Defenses( XElement root )
		{
			Armor = root.Elements( "armor" ).Select( e => new DefensesArmor
														  {
															  Base = (int)e.GetAttributeValue( "base", ConvertToType.Int ),
															  Effective = (int)e.GetAttributeValue( "effective", ConvertToType.Int ),
															  Percent = (double)e.GetAttributeValue( "percent", ConvertToType.Double ),
															  PetBonus = (double)e.GetAttributeValue( "petBonus", ConvertToType.Double )
														  } ).FirstOrDefault();

			Defense = root.Elements( "defense" ).Select( e => new DefensesDefense
															  {
																  DecreasePercent = (double)e.GetAttributeValue( "decreasePercent", ConvertToType.Double ),
																  IncreasePercent = (double)e.GetAttributeValue( "increasePercent", ConvertToType.Double ),
																  PlusDefense = (int)e.GetAttributeValue( "plusDefense", ConvertToType.Int ),
																  Rating = (int)e.GetAttributeValue( "rating", ConvertToType.Int ),
																  Value = (double)e.GetAttributeValue( "value", ConvertToType.Double ),
															  } ).FirstOrDefault();

			Dodge = root.Elements( "dodge" ).Select( e => new DefensesDodge
														  {
															  IncreasePercent = (double)e.GetAttributeValue( "increasePercent", ConvertToType.Double ),
															  Percent = (double)e.GetAttributeValue( "percent", ConvertToType.Double ),
															  Rating = (int)e.GetAttributeValue( "rating", ConvertToType.Int ),
														  } ).FirstOrDefault();

			Parry = root.Elements( "parry" ).Select( e => new DefensesParry
														  {
															  IncreasePercent = (double)e.GetAttributeValue( "increasePercent", ConvertToType.Double ),
															  Percent = (double)e.GetAttributeValue( "percent", ConvertToType.Double ),
															  Rating = (int)e.GetAttributeValue( "rating", ConvertToType.Int ),
														  } ).FirstOrDefault();

			Block = root.Elements( "block" ).Select( e => new DefensesBlock
														  {
															  IncreasePercent = (double)e.GetAttributeValue( "increasePercent", ConvertToType.Double ),
															  Percent = (double)e.GetAttributeValue( "percent", ConvertToType.Double ),
															  Rating = (int)e.GetAttributeValue( "rating", ConvertToType.Int ),
														  } ).FirstOrDefault();

			Resilience = root.Elements( "resilience" ).Select( e => new DefensesResilience
																	{
																		DamagePercent = (double)e.GetAttributeValue( "damagePercent", ConvertToType.Double ),
																		HitPercent = (double)e.GetAttributeValue( "hitPercent", ConvertToType.Double ),
																		Value = (double)e.GetAttributeValue( "value", ConvertToType.Double ),
																	} ).FirstOrDefault();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
