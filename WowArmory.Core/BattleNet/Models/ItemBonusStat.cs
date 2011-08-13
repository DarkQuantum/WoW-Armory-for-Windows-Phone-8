using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemBonusStat
	{
		[DataMember]
		[XmlElement("stat")]
		public ItemBonusStatType Stat { get; set; }
		[DataMember]
		[XmlElement("amount")]
		public int Amount { get; set; }
		[DataMember]
		[XmlElement("reforged")]
		public bool Reforged { get; set; }
	}
}