using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class GuildMemberCharacter
	{
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("realm")]
		public string Realm { get; set; }
		[DataMember]
		[XmlElement("class")]
		public CharacterClass Class { get; set; }
		[DataMember]
		[XmlElement("race")]
		public CharacterRace Race { get; set; }
		[DataMember]
		[XmlElement("gender")]
		public CharacterGender Gender { get; set; }
		[DataMember]
		[XmlElement("level")]
		public int Level { get; set; }
		[DataMember]
		[XmlElement("achievementPoints")]
		public int AchievementPoints { get; set; }
		[DataMember]
		[XmlElement("thumbnail")]
		public string Thumbnail { get; set; }
	}
}