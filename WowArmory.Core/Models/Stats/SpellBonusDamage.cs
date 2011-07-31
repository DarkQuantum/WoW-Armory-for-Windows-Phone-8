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
