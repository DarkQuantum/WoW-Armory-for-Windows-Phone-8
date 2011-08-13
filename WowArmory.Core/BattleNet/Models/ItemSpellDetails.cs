using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemSpellDetails
	{
		[DataMember]
		[XmlElement("id")]
		public int Id { get; set; }
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("icon")]
		public string Icon { get; set; }
		[DataMember]
		[XmlElement("description")]
		public string Description { get; set; }
		[DataMember]
		[XmlElement("castTime")]
		public string CastTime { get; set; }
	}
}