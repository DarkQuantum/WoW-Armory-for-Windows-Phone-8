using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class CharacterAppearance
	{
		[DataMember]
		[XmlElement("faceVariation")]
		public int FaceVariation { get; set; }
		[DataMember]
		[XmlElement("skinColor")]
		public int SkinColor { get; set; }
		[DataMember]
		[XmlElement("hairVariation")]
		public int HairVariation { get; set; }
		[DataMember]
		[XmlElement("hairColor")]
		public int HairColor { get; set; }
		[DataMember]
		[XmlElement("featureVariation")]
		public int FeatureVariation { get; set; }
		[DataMember]
		[XmlElement("showHelm")]
		public bool ShowHelm { get; set; }
		[DataMember]
		[XmlElement("showCloak")]
		public bool ShowCloak { get; set; }
	}
}