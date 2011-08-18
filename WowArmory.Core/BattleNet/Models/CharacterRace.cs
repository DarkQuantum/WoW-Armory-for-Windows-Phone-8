using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum CharacterRace
	{
		[EnumMember]
		BloodElf = 10,
		[EnumMember]
		Draenei = 11,
		[EnumMember]
		Dwarf = 3,
		[EnumMember]
		Gnome = 7,
		[EnumMember]
		Goblin = 9,
		[EnumMember]
		Human = 1,
		[EnumMember]
		NightElf = 4,
		[EnumMember]
		Orc = 2,
		[EnumMember]
		Tauren = 6,
		[EnumMember]
		Troll = 8,
		[EnumMember]
		Undead = 5,
		[EnumMember]
		Worgen = 22
	}
}