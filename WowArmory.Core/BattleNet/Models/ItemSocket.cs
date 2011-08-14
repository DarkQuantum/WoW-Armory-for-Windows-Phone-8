using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemSocket
	{
		[DataMember]
		[XmlElement("type")]
		public string Type { get; set; }
	}
}