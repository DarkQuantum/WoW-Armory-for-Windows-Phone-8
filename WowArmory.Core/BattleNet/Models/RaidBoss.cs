using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class RaidBoss
	{
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("normalKills")]
		public int NormalKills { get; set; }
		[DataMember]
		[XmlElement("heroicKills")]
		public int HeroicKills { get; set; }
		[DataMember]
		[XmlElement("id")]
		public int Id { get; set; }
	}
}