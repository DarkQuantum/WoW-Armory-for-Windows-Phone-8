using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum RealmPopulation
	{
		[EnumMember]
		[XmlEnum("notavailable")]
		NotAvailable,
		[EnumMember]
		[XmlEnum("low")]
		Low,
		[EnumMember]
		[XmlEnum("medium")]
		Medium,
		[EnumMember]
		[XmlEnum("high")]
		High
	}
}