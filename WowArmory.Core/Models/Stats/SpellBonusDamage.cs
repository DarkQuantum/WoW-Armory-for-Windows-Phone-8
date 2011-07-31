using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Helpers;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public class SpellBonusDamage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Arcane { get; set; }
		[DataMember]
		public int Fire { get; set; }
		[DataMember]
		public int Frost { get; set; }
		[DataMember]
		public int Holy { get; set; }
		[DataMember]
		public int Nature { get; set; }
		[DataMember]
		public int Shadow { get; set; }
		[DataMember]
		public SpellBonusDamagePetBonus PetBonus { get; set; }

		public int Value
		{
			get
			{
				var result = Arcane;
				if ( Fire > result ) result = Fire;
				if ( Frost > result ) result = Frost;
				if ( Holy > result ) result = Holy;
				if ( Nature > result ) result = Nature;
				if ( Shadow > result ) result = Shadow;
				return result;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public SpellBonusDamage()
		{
		}

		public SpellBonusDamage( XElement root )
		{
			Arcane = root.Elements( "arcane" ).Select( e => Int32.Parse( e.GetAttributeValue( "value" ), CultureInfo.InvariantCulture ) ).FirstOrDefault();
			Fire = root.Elements( "fire" ).Select( e => Int32.Parse( e.GetAttributeValue( "value" ), CultureInfo.InvariantCulture ) ).FirstOrDefault();
			Frost = root.Elements( "frost" ).Select( e => Int32.Parse( e.GetAttributeValue( "value" ), CultureInfo.InvariantCulture ) ).FirstOrDefault();
			Holy = root.Elements( "holy" ).Select( e => Int32.Parse( e.GetAttributeValue( "value" ), CultureInfo.InvariantCulture ) ).FirstOrDefault();
			Nature = root.Elements( "nature" ).Select( e => Int32.Parse( e.GetAttributeValue( "value" ), CultureInfo.InvariantCulture ) ).FirstOrDefault();
			Shadow = root.Elements( "shadow" ).Select( e => Int32.Parse( e.GetAttributeValue( "value" ), CultureInfo.InvariantCulture ) ).FirstOrDefault();
			PetBonus = root.Elements( "petBonus" ).Select( e => new SpellBonusDamagePetBonus
			                                                    {
																	Attack = Int32.Parse( e.GetAttributeValue( "attack" ), CultureInfo.InvariantCulture ),
																	Damage = Int32.Parse( e.GetAttributeValue( "damage" ), CultureInfo.InvariantCulture ),
			                                                    	FromType = e.GetAttributeValue( "fromType" )
			                                                    } ).FirstOrDefault();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
