using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct SpellCritChance
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Rating { get; set; }
		[DataMember]
		public double Arcane { get; set; }
		[DataMember]
		public double Fire { get; set; }
		[DataMember]
		public double Frost { get; set; }
		[DataMember]
		public double Holy { get; set; }
		[DataMember]
		public double Nature { get; set; }
		[DataMember]
		public double Shadow { get; set; }

		public string Text
		{
			get
			{
				var result = Arcane;
				if ( Fire > result ) result = Fire;
				if ( Frost > result ) result = Frost;
				if ( Holy > result ) result = Holy;
				if ( Nature > result ) result = Nature;
				if ( Shadow > result ) result = Shadow;

				return String.Format( "{0}%", result.ToString( "0.00", CultureInfo.InvariantCulture ) );
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
