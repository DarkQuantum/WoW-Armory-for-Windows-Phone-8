using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class Realm
	{
		[DataMember]
		[XmlElement("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public RealmType Type { get; set; }
		[DataMember]
		[XmlElement("queue")]
		public bool Queue { get; set; }
		[DataMember]
		[XmlElement("status")]
		public bool Status { get; set; }
		[DataMember]
		[XmlElement("population")]
		[JsonConverter(typeof(StringEnumConverter))]
		public RealmPopulation Population { get; set; }
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("slug")]
		public string Slug { get; set; }
	}
}