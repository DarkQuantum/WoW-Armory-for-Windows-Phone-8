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
