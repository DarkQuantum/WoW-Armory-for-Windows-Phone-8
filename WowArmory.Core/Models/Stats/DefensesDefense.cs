using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct DefensesDefense
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public double DecreasePercent { get; set; }
		[DataMember]
		public double IncreasePercent { get; set; }
		[DataMember]
		public int PlusDefense { get; set; }
		[DataMember]
		public int Rating { get; set; }
		[DataMember]
		public double Value { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
