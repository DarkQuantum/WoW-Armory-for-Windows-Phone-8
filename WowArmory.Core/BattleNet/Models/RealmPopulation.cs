using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	public enum RealmPopulation
	{
		[XmlEnum("notavailable")]
		NotAvailable,
		[XmlEnum("low")]
		Low,
		[XmlEnum("medium")]
		Medium,
		[XmlEnum("high")]
		High
	}
}