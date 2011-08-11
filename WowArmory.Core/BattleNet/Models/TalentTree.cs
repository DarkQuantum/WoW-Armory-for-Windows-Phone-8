using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class TalentTree
	{
		[DataMember]
		[XmlElement("points")]
		public string Points { get; set; }
		[DataMember]
		[XmlElement("total")]
		public int Total { get; set; }
	}
}