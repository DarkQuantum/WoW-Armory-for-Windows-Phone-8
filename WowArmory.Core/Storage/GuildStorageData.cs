using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;

namespace WowArmory.Core.Storage
{
	[DataContract]
	public class GuildStorageData
	{
		[DataMember]
		public Guid Guid { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Realm { get; set; }
		[DataMember]
		public Region Region { get; set; }
		[DataMember]
		public int Level { get; set; }
		[DataMember]
		public GuildSide Side { get; set; }
		[DataMember]
		public int AchievementPoints { get; set; }
		[DataMember]
		public List<GuildMember> Members { get; set; }
	}
}