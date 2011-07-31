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
	public class BaseStats
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public Strength Strength { get; set; }
		[DataMember]
		public Agility Agility { get; set; }
		[DataMember]
		public Stamina Stamina { get; set; }
		[DataMember]
		public Intellect Intellect { get; set; }
		[DataMember]
		public Spirit Spirit { get; set; }
		[DataMember]
		public Armor Armor { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseStats"/> class.
		/// </summary>
		public BaseStats()
		{
		}

		public BaseStats( XElement root )
		{
			Strength = root.Elements( "strength" ).Select( e =>
				new Strength( (int)e.GetAttributeValue( "attack", ConvertToType.Int ),
					(int)e.GetAttributeValue( "base", ConvertToType.Int ),
					(int)e.GetAttributeValue( "block", ConvertToType.Int ),
					(int)e.GetAttributeValue( "effective", ConvertToType.Int )
				)
			).FirstOrDefault();
			Agility = root.Elements( "agility" ).Select( e =>
				new Agility( (int)e.GetAttributeValue( "armor", ConvertToType.Int ),
					(int)e.GetAttributeValue( "attack", ConvertToType.Int ),
					(int)e.GetAttributeValue( "base", ConvertToType.Int ),
					(double)e.GetAttributeValue( "critHitPercent", ConvertToType.Double ),
					(int)e.GetAttributeValue( "effective", ConvertToType.Int )
				)
			).FirstOrDefault();
			Stamina = root.Elements( "stamina" ).Select( e =>
				new Stamina( (int)e.GetAttributeValue( "base", ConvertToType.Int ),
					(int)e.GetAttributeValue( "effective", ConvertToType.Int ),
					(int)e.GetAttributeValue( "health", ConvertToType.Int ),
					(int)e.GetAttributeValue( "petBonus", ConvertToType.Int )
				)
			).FirstOrDefault();
			Intellect = root.Elements( "intellect" ).Select( e =>
				new Intellect( (int)e.GetAttributeValue( "base", ConvertToType.Int ),
					(double)e.GetAttributeValue( "critHitPercent", ConvertToType.Double ),
					(int)e.GetAttributeValue( "effective", ConvertToType.Int ),
					(int)e.GetAttributeValue( "mana", ConvertToType.Int ),
					(int)e.GetAttributeValue( "petBonus", ConvertToType.Int )
				)
			).FirstOrDefault();
			Spirit = root.Elements( "spirit" ).Select( e =>
				new Spirit( (int)e.GetAttributeValue( "base", ConvertToType.Int ),
					(int)e.GetAttributeValue( "effective", ConvertToType.Int ),
					(int)e.GetAttributeValue( "healthRegen", ConvertToType.Int ),
					(int)e.GetAttributeValue( "manaRegen", ConvertToType.Int )
				)
			).FirstOrDefault();
			Armor = root.Elements( "armor" ).Select( e =>
				new Armor( (int)e.GetAttributeValue( "base", ConvertToType.Int ),
					(int)e.GetAttributeValue( "effective", ConvertToType.Int ),
					(double)e.GetAttributeValue( "percent", ConvertToType.Double ),
					(int)e.GetAttributeValue( "petBonus", ConvertToType.Int )
				)
			).FirstOrDefault();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
