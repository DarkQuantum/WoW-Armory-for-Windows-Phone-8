using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class CharacterProgression
	{
		[DataMember]
		[XmlArray("raids", IsNullable = true)]
		public List<RaidProgression> Raids { get; set; }
	}
}