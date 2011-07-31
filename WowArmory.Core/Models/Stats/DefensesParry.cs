using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct DefensesParry
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public double IncreasePercent { get; set; }
		[DataMember]
		public double Percent { get; set; }
		[DataMember]
		public int Rating { get; set; }

		public string Text
		{
			get { return String.Format( "{0}%", Percent.ToString( "0.00", CultureInfo.InvariantCulture ) ); }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
