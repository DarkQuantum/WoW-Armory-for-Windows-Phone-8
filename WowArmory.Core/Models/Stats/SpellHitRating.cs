using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct SpellHitRating
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public double IncreasedHitPercent { get; set; }
		[DataMember]
		public int Penetration { get; set; }
		[DataMember]
		public int ReducedResist { get; set; }
		[DataMember]
		public int Value { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
