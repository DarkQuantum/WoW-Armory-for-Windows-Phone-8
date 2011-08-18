using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum InventoryType
	{
		[EnumMember]
		None = 0,
		[EnumMember]
		Head = 1,
		[EnumMember]
		Neck = 2,
		[EnumMember]
		Shoulder = 3,
		[EnumMember]
		Shirt = 4,
		[EnumMember]
		Chest = 5,
		[EnumMember]
		Waist = 6,
		[EnumMember]
		Legs = 7,
		[EnumMember]
		Feet = 8,
		[EnumMember]
		Wrist = 9,
		[EnumMember]
		Hands = 10,
		[EnumMember]
		Finger = 11,
		[EnumMember]
		Trinket = 12,
		[EnumMember]
		OneHand = 13,
		[EnumMember]
		Shield = 14,
		[EnumMember]
		Ranged = 15,
		[EnumMember]
		Cloak = 16,
		[EnumMember]
		TwoHand = 17,
		[EnumMember]
		Bag = 18,
		[EnumMember]
		Tabard = 19,
		[EnumMember]
		Robe = 20,
		[EnumMember]
		MainHand = 21,
		[EnumMember]
		OffHand = 22,
		[EnumMember]
		HeldInOffHand = 23,
		[EnumMember]
		Ammo = 24,
		[EnumMember]
		Thrown = 25,
		[EnumMember]
		RangedRight = 26,
		[EnumMember]
		Relic = 28
	}
}