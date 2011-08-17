using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class GuildAchievements
	{
		[DataMember]
		[XmlArray("achievementsCompleted", IsNullable = true)]
		public List<int> AchievementsCompleted { get; set; }
		[DataMember]
		[XmlArray("achievementsCompletedTimestamp", IsNullable = true)]
		public List<long> AchievementsCompletedTimestamp { get; set; }
		[DataMember]
		[XmlArray("criteria", IsNullable = true)]
		public List<int> Criteria { get; set; }
		[DataMember]
		[XmlArray("criteriaQuantity", IsNullable = true)]
		public List<long> CriteriaQuantity { get; set; }
		[DataMember]
		[XmlArray("criteriaTimestamp", IsNullable = true)]
		public List<long> CriteriaTimestamp { get; set; }
		[DataMember]
		[XmlArray("criteriaCreated", IsNullable = true)]
		public List<long> CriteriaCreated { get; set; }
	}
}