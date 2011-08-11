using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class CharacterGuildEmblem
	{
		[DataMember]
		[XmlElement("icon")]		
		public int Icon { get; set; }
		[DataMember]
		[XmlElement("iconColor")]
		public string IconColor { get; set; }
		[DataMember]
		[XmlElement("border")]
		public int Border { get; set; }
		[DataMember]
		[XmlElement("borderColor")]
		public string BorderColor { get; set; }
		[DataMember]
		[XmlElement("backgroundColor")]
		public string BackgroundColor { get; set; }
	}
}