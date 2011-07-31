using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public class Spirit
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Base { get; set; }
		[DataMember]
		public int Effective { get; set; }
		[DataMember]
		public int HealthRegen { get; set; }
		[DataMember]
		public int ManaRegen { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public Spirit()
		{
		}

		public Spirit( int @base, int effective, int healthRegen, int manaRegen )
		{
			Base = @base;
			Effective = effective;
			HealthRegen = healthRegen;
			ManaRegen = manaRegen;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
