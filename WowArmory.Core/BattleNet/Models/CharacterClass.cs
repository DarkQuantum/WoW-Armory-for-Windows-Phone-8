using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum CharacterClass
	{
		[EnumMember]
		DeathKnight = 6,
		[EnumMember]
		Druid = 11,
		[EnumMember]
		Hunter = 3,
		[EnumMember]
		Mage = 8,
		[EnumMember]
		Paladin = 2,
		[EnumMember]
		Priest = 5,
		[EnumMember]
		Rogue = 4,
		[EnumMember]
		Shaman = 7,
		[EnumMember]
		Warlock = 9,
		[EnumMember]
		Warrior = 1
	}
}