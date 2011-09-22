using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class GuildPerkSpell
	{
		[DataMember]
		[XmlElement("id")]
		public int Id { get; set; }
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("subtext", IsNullable = true)]
		public string Subtext { get; set; }
		[DataMember]
		[XmlElement("icon")]
		public string Icon { get; set; }
		[DataMember]
		[XmlElement("description")]
		public string Description { get; set; }
		[DataMember]
		[XmlElement("range", IsNullable = true)]
		public string Range { get; set; }
		[DataMember]
		[XmlElement("castTime", IsNullable = true)]
		public string CastTime { get; set; }
		[DataMember]
		[XmlElement("cooldown", IsNullable = true)]
		public string Cooldown { get; set; }
	}
}