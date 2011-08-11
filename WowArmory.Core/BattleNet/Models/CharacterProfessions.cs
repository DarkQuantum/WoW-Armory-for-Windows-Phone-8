using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class CharacterProfessions
	{
		[DataMember]
		[XmlArray("primary")]
		public List<Profession> Primary { get; set; }
		[DataMember]
		[XmlArray("secondary")]
		public List<Profession> Secondary { get; set; }
	}
}