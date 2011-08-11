using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class Reputation
	{
		[DataMember]
		[XmlElement("id")]
		public int Id { get; set; }
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("standing")]
		public int Standing { get; set; }
		[DataMember]
		[XmlElement("value")]
		public int Value { get; set; }
		[DataMember]
		[XmlElement("max")]
		public int Max { get; set; }
	}
}