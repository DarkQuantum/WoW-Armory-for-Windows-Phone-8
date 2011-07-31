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
using WowArmory.Core.Languages;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ItemToolTipDamageData
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Max { get; set; }
		[DataMember]
		public int Min { get; set; }
		[DataMember]
		public double Speed { get; set; }
		[DataMember]
		public double DamagePerSecond { get; set; }

		public string DamageText
		{
			get { return Min > 0 && Max > 0 ? String.Format( "{0}-{1} {2}", Min, Max, AppResources.Item_Damage ) : ""; }
		}

		public string SpeedText
		{
			get { return Speed > 0.0 ? String.Format( "{0} {1}", AppResources.Item_Damage_Speed, Speed.ToString( "0.0", CultureInfo.InvariantCulture ) ) : ""; }
		}

		public string DamagePerSecondText
		{
			get { return DamagePerSecond > 0.0 ? String.Format( "({0} {1})", DamagePerSecond.ToString( "0.0", CultureInfo.InvariantCulture ), AppResources.Item_Damage_DamagePerSecond ) : ""; }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemToolTipDamageData"/> class.
		/// </summary>
		public ItemToolTipDamageData()
		{
		}

		public ItemToolTipDamageData( XElement root )
		{
			Max = root.Elements( "damage" ).Select( e => e.Element( "max" ) != null ? Int32.Parse( e.Element( "max" ).Value, CultureInfo.InvariantCulture ) : 0 ).FirstOrDefault();
			Min = root.Elements( "damage" ).Select( e => e.Element( "min" ) != null ? Int32.Parse( e.Element( "min" ).Value, CultureInfo.InvariantCulture ) : 0 ).FirstOrDefault();
			Speed = root.Element( "speed" ) != null ? Double.Parse( root.Element( "speed" ).Value, CultureInfo.InvariantCulture ) : 0.0;
			DamagePerSecond = root.Element( "dps" ) != null ? Double.Parse( root.Element( "dps" ).Value, CultureInfo.InvariantCulture ) : 0.0;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
