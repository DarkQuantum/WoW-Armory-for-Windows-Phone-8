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
