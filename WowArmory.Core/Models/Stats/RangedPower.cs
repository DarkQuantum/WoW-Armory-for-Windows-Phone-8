using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct RangedPower
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Base { get; set; }
		[DataMember]
		public int Effective { get; set; }
		[DataMember]
		public double IncreasedDps { get; set; }
		[DataMember]
		public double PetAttack { get; set; }
		[DataMember]
		public double PetSpell { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
