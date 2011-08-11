using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class Profession
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
		[XmlElement("rank")]
		public int Rank { get; set; }
		[DataMember]
		[XmlElement("max")]
		public int Max { get; set; }
		[DataMember]
		[XmlArray("recipes")]
		public List<int> Recipes { get; set; }
	}
}