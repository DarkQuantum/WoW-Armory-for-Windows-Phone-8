using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public class Stamina
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Base { get; set; }
		[DataMember]
		public int Effective { get; set; }
		[DataMember]
		public int Health { get; set; }
		[DataMember]
		public int PetBonus { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public Stamina()
		{
		}

		public Stamina( int @base, int effective, int health, int petBonus )
		{
			Base = @base;
			Effective = effective;
			Health = health;
			PetBonus = petBonus;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
