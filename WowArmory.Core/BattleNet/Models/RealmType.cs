using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	public enum RealmType
	{
		[XmlEnum("pve")]
		PvE,
		[XmlEnum("pvp")]
		PvP,
		[XmlEnum("rp")]
		RP,
		[XmlEnum("rppvp")]
		RPPvP
	}
}