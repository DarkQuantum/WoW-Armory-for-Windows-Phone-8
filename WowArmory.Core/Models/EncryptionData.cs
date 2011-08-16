using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.Models
{
	[DataContract]
	[XmlRoot("EncryptionData")]
	public class EncryptionData
	{
		[DataMember]
		[XmlElement("Password")]
		public string Password { get; set; }
		[DataMember]
		[XmlElement("Salt")]
		public string Salt { get; set; }
	}
}