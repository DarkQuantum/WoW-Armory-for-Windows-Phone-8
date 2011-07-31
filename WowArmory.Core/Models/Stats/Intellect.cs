using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public class Intellect
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Base { get; set; }
		[DataMember]
		public double CritHitPercent { get; set; }
		[DataMember]
		public int Effective { get; set; }
		[DataMember]
		public int Mana { get; set; }
		[DataMember]
		public int PetBonus { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public Intellect()
		{
		}

		public Intellect( int @base, double critHitPercent, int effective, int mana, int petBonus )
		{
			Base = @base;
			CritHitPercent = critHitPercent;
			Effective = effective;
			Mana = mana;
			PetBonus = petBonus;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
