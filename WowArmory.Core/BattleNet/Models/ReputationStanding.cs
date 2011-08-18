using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum ReputationStanding
	{
		[EnumMember]
		Hated = 0,
		[EnumMember]
		Hostile = 1,
		[EnumMember]
		Unfriendly = 2,
		[EnumMember]
		Neutral = 3,
		[EnumMember]
		Friendly = 4,
		[EnumMember]
		Honored = 5,
		[EnumMember]
		Revered = 6,
		[EnumMember]
		Exalted = 7
	}
}