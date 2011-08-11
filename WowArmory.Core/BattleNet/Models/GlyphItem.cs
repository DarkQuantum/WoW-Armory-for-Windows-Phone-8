using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class GlyphItem
	{
		[DataMember]
		[XmlElement("glyph")]
		public int Glyph { get; set; }
		[DataMember]
		[XmlElement("item")]
		public int Item { get; set; }
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("icon")]
		public string Icon { get; set; }
	}
}