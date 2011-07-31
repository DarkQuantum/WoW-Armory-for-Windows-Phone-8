using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct SpellBonusDamagePetBonus
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Attack { get; set; }
		[DataMember]
		public int Damage { get; set; }
		[DataMember]
		public string FromType { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
