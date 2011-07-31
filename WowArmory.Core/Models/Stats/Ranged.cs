// 
// Copyright (c) 2010 Christian Krueger <christian.krueger@krueger-c.com>
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge and for non-commercial usage
// only, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify,
// merge, publish, and/or distribute copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

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
