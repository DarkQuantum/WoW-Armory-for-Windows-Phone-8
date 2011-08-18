using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class Quest
	{
		[DataMember]
		[XmlElement("id")]
		public int Id { get; set; }
		[DataMember]
		[XmlElement("title")]
		public string Title { get; set; }
		[DataMember]
		[XmlElement("reqLevel")]
		public int ReqLevel { get; set; }
		[DataMember]
		[XmlElement("suggestedPartyMembers")]
		public int SuggestedPartyMembers { get; set; }
		[DataMember]
		[XmlElement("category")]
		public string Category { get; set; }
		[DataMember]
		[XmlElement("level")]
		public int Level { get; set; }
	}
}