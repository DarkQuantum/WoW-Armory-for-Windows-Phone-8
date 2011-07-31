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
