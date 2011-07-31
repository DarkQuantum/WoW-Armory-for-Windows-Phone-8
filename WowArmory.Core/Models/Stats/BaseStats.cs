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
