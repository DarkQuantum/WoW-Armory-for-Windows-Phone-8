using System;
using System.Runtime.Serialization;

namespace WowArmory.Core.Models.Stats
{
	[DataContract]
	public struct MeleeOffHandDamage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public double Dps { get; set; }
		[DataMember]
		public int Max { get; set; }
		[DataMember]
		public int Min { get; set; }
		[DataMember]
		public double Percent { get; set; }
		[DataMember]
		public double Speed { get; set; }

		public string Text
		{
			get { return String.Format( "{0} - {1}", Min, Max ); }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
