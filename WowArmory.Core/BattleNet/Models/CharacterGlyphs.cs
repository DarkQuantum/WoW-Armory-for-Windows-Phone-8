using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class CharacterGlyphs
	{
		[DataMember]
		[XmlArray("prime", IsNullable = true)]
		public List<GlyphItem> Prime { get; set; }
		[DataMember]
		[XmlArray("major", IsNullable = true)]
		public List<GlyphItem> Major { get; set; }
		[DataMember]
		[XmlArray("minor", IsNullable = true)]
		public List<GlyphItem> Minor { get; set; }
	}
}