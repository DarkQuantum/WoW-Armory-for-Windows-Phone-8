using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemSource
	{
		[DataMember]
		[XmlElement("sourceId")]
		public int SourceId { get; set; }
		[DataMember]
		[XmlElement("sourceType")]
		public string SourceType { get; set; }
	}
}