using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum ItemQuality
	{
		[EnumMember]
		Poor = 0,
		[EnumMember]
		Common = 1,
		[EnumMember]
		Uncommon = 2,
		[EnumMember]
		Rare = 3,
		[EnumMember]
		Epic = 4,
		[EnumMember]
		Legendary = 5,
		[EnumMember]
		Artifact = 6,
		[EnumMember]
		Heirloom = 7
	}
}