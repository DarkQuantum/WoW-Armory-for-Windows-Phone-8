using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class GuildPerks : ApiResponse
	{
		[DataMember]
		[XmlArray("perks", IsNullable = true)]
		public List<GuildPerk> Perks { get; set; }
	}
}