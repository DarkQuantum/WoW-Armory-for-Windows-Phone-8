using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Media;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class Character : ApiResponse
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
		[XmlElement("class")]
		public int Class { get; set; }
		[DataMember]
		[XmlElement("race")]
		public int Race { get; set; }
		[DataMember]
		[XmlElement("gender")]
		public int Gender { get; set; }
		[DataMember]
		[XmlElement("level")]
		public int Level { get; set; }
		[DataMember]
		[XmlElement("achievementPoints")]
		public int AchievementPoints { get; set; }
		[DataMember]
		[XmlElement("thumbnail")]
		public string Thumbnail { get; set; }
		[DataMember]
		[XmlElement("guild", IsNullable = true)]
		public CharacterGuild Guild { get; set; }
		[DataMember]
		[XmlElement("items", IsNullable = true)]
		public CharacterItems Items { get; set; }
		[DataMember]
		[XmlElement("stats", IsNullable = true)]
		public CharacterStats Stats { get; set; }
		[DataMember]
		[XmlElement("professions", IsNullable = true)]
		public CharacterProfessions Professions { get; set; }
		[DataMember]
		[XmlArray("reputation", IsNullable = true)]
		public List<Reputation> Reputation { get; set; }
		[DataMember]
		[XmlArray("titles")]
		public List<CharacterTitle> Titles { get; set; }
		[DataMember]
		[XmlElement("achievements", IsNullable = true)]
		public CharacterAchievements Achievements { get; set; }
		[DataMember]
		[XmlArray("talents", IsNullable = true)]
		public List<CharacterTalents> Talents { get; set; }
		[DataMember]
		[XmlElement("appearance", IsNullable = true)]
		public CharacterAppearance Appearance { get; set; }
		[DataMember]
		[XmlArray("mounts", IsNullable = true)]
		public List<int> Mounts { get; set; }
		[DataMember]
		[XmlArray("companions", IsNullable = true)]
		public List<int> Companions { get; set; }
		[DataMember]
		[XmlElement("progression", IsNullable = true)]
		public CharacterProgression Progression { get; set; }
	}
}