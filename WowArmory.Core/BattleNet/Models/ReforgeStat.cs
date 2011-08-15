using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ReforgeStat
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		[DataMember]
		public ItemBonusStatType From { get; set; }
		[DataMember]
		public ItemBonusStatType To { get; set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ReforgeStat"/> class.
		/// </summary>
		public ReforgeStat()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReforgeStat"/> class.
		/// </summary>
		/// <param name="from">Item bonus stat type to reforge from.</param>
		/// <param name="to">Item bonus stat type to reforge to.</param>
		public ReforgeStat(ItemBonusStatType from, ItemBonusStatType to)
		{
			From = from;
			To = to;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}