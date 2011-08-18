using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class CharacterGuild
	{
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("realm")]
		public string Realm { get; set; }
		[DataMember]
		[XmlElement("level")]
		public int Level { get; set; }
		[DataMember]
		[XmlElement("members")]
		public int Members { get; set; }
		[DataMember]
		[XmlElement("achievementPoints")]
		public int AchievementPoints { get; set; }
		[DataMember]
		[XmlElement("emblem")]
		public GuildEmblem Emblem { get; set; }
	}
}