using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemWeaponInfoDamage
	{
		[DataMember]
		[XmlElement("minDamage")]
		public int MinDamage { get; set; }
		[DataMember]
		[XmlElement("maxDamage")]
		public int MaxDamage { get; set; }
	}
}