using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class RaidProgression
	{
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("normal")]
		public int Normal { get; set; }
		[DataMember]
		[XmlElement("heroic")]
		public int Heroic { get; set; }
		[DataMember]
		[XmlElement("id")]
		public int Id { get; set; }
		[DataMember]
		[XmlArray("bosses")]
		public List<RaidBoss> Bosses { get; set; }
	}
}