using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public class Strength
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Attack { get; set; }
		[DataMember]
		public int Base { get; set; }
		[DataMember]
		public int Block { get; set; }
		[DataMember]
		public int Effective { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public Strength()
		{
		}

		public Strength( int attack, int @base, int block, int effective )
		{
			Attack = attack;
			Base = @base;
			Block = block;
			Effective = effective;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
