using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct MeleeMainHandSpeed
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public double HastePercent { get; set; }
		[DataMember]
		public int HasteRating { get; set; }
		[DataMember]
		public double Value { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
