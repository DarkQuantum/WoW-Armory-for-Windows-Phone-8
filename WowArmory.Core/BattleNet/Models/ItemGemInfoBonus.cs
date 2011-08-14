using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemGemInfoBonus
	{
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("srcItemId")]
		public int SrcItemId { get; set; }
		[DataMember]
		[XmlElement("requiredSkillId")]
		public int RequiredSkillId { get; set; }
		[DataMember]
		[XmlElement("requiredSkillRank")]
		public int RequiredSkillRank { get; set; }
		[DataMember]
		[XmlElement("minLevel")]
		public int MinLevel { get; set; }
		[DataMember]
		[XmlElement("itemLevel")]
		public int ItemLevel { get; set; }
	}
}