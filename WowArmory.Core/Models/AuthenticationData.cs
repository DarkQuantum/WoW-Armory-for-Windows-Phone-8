using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.Models
{
	[DataContract]
	[XmlRoot("AuthenticationData")]
	public class AuthenticationData
	{
		[DataMember]
		[XmlElement("PublicKey")]
		public string PublicKey { get; set; }
		[DataMember]
		[XmlElement("PrivateKey")]
		public string PrivateKey { get; set; }
	}
}