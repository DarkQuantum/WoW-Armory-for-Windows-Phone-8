using System;
using System.Runtime.Serialization;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;

namespace WowArmory.Core.Storage
{
	[DataContract]
	public class CharacterStorageData
	{
		[DataMember]
		public Guid Guid { get; set; }
		[DataMember]
		public Region Region { get; set; }
		[DataMember]
		public string Realm { get; set; }
		[DataMember]
		public string Character { get; set; }
		[DataMember]
		public string Thumbnail { get; set; }
		[DataMember]
		public string Guild { get; set; }
		[DataMember]
		public int Level { get; set; }
		[DataMember]
		public CharacterClass Class { get; set; }
		[DataMember]
		public CharacterFaction Faction { get; set; }
		[DataMember]
		public CharacterRace Race { get; set; }
	}
}