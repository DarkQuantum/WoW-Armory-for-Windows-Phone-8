using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class GuildPerk
	{
		[DataMember]
		[XmlElement("guildLevel")]
		public int GuildLevel { get; set; }
		[DataMember]
		[XmlElement("spell")]
		public GuildPerkSpell Spell { get; set; }
	}
}