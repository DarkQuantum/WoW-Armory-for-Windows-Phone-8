using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemWeaponInfo
	{
		[DataMember]
		[XmlArray("damage")]
		public ItemWeaponInfoDamage Damage { get; set; }
		[DataMember]
		[XmlElement("weaponSpeed")]
		public float WeaponSpeed { get; set; }
		[DataMember]
		[XmlElement("dps")]
		public float Dps { get; set; }
	}
}