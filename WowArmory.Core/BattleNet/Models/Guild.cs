using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class Guild : ApiResponse
	{
		[DataMember]
		[XmlElement("lastModified")]
		public long LastModified { get; set; }
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
		[XmlElement("side")]
		public GuildSide Side { get; set; }
		[DataMember]
		[XmlElement("achievementPoints")]
		public int AchievementPoints { get; set; }
		[DataMember]
		[XmlElement("achievements", IsNullable = true)]
		public GuildAchievements Achievements { get; set; }
		[DataMember]
		[XmlArray("members")]
		public List<GuildMember> Members { get; set; }
		[DataMember]
		[XmlElement("emblem")]
		public GuildEmblem Emblem { get; set; }
	}
}