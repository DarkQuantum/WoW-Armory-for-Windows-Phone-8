using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum RealmType
	{
		[EnumMember]
		[XmlEnum("pve")]
		PvE,
		[EnumMember]
		[XmlEnum("pvp")]
		PvP,
		[EnumMember]
		[XmlEnum("rp")]
		RP,
		[EnumMember]
		[XmlEnum("rppvp")]
		RPPvP
	}
}