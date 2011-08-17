using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class GuildMember
	{
		[DataMember]
		[XmlElement("character")]
		public GuildMemberCharacter Character { get; set; }
		[DataMember]
		[XmlElement("rank")]
		public int Rank { get; set; }
	}
}