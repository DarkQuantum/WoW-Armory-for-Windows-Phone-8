using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class CharacterTalents
	{
		[DataMember]
		[XmlElement("selected", IsNullable = true)]
		public bool? Selected { get; set; }
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("icon")]
		public string Icon { get; set; }
		[DataMember]
		[XmlElement("build")]
		public string Build { get; set; }
		[DataMember]
		[XmlArray("trees")]
		public List<TalentTree> Trees { get; set; }
		[DataMember]
		[XmlElement("glyphs")]
		public CharacterGlyphs Glyphs { get; set; }
	}
}