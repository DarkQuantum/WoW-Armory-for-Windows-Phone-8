using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct DefensesArmor
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Base { get; set; }
		[DataMember]
		public int Effective { get; set; }
		[DataMember]
		public double Percent { get; set; }
		[DataMember]
		public double PetBonus { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
