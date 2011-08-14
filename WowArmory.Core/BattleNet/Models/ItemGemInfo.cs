using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemGemInfo
	{
		[DataMember]
		[XmlElement("bonus")]
		public ItemGemInfoBonus Bonus { get; set; }
		[DataMember]
		[XmlElement("type")]
		public ItemGemInfoType Type { get; set; }
	}
}