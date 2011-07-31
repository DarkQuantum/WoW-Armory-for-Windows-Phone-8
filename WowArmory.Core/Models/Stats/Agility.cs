using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public class Agility
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Armor { get; set; }
		[DataMember]
		public int Attack { get; set; }
		[DataMember]
		public int Base { get; set; }
		[DataMember]
		public double CritHitPercent { get; set; }
		[DataMember]
		public int Effective { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public Agility()
		{
		}

		public Agility( int armor, int attack, int @base, double critHitPercent, int effective )
		{
			Armor = armor;
			Attack = attack;
			Base = @base;
			CritHitPercent = critHitPercent;
			Effective = effective;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
