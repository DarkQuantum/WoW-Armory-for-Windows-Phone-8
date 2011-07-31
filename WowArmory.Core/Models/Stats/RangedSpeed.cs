using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct RangedSpeed
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public double HastePercent { get; set; }
		[DataMember]
		public double HasteRating { get; set; }
		[DataMember]
		public double Value { get; set; }

		public string Text
		{
			get { return String.Format( "{0}", Value.ToString( "0.0", CultureInfo.InvariantCulture ) ); }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
