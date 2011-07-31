using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct MeleeExpertise
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Additional { get; set; }
		[DataMember]
		public double Percent { get; set; }
		[DataMember]
		public int Rating { get; set; }
		[DataMember]
		public int Value { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
