using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemWeaponInfoDamage
	{
		[DataMember]
		[XmlElement("min")]
		public int Min { get; set; }
		[DataMember]
		[XmlElement("max")]
		public int Max { get; set; }
	}
}