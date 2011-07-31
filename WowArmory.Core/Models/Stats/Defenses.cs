﻿// 
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
