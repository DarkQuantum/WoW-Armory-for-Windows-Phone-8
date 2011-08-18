using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum ItemSlot
	{
		[EnumMember]
		Head,
		[EnumMember]
		Neck,
		[EnumMember]
		Shoulder,
		[EnumMember]
		Back,
		[EnumMember]
		Chest,
		[EnumMember]
		Shirt,
		[EnumMember]
		Tabard,
		[EnumMember]
		Wrist,
		[EnumMember]
		Hands,
		[EnumMember]
		Waist,
		[EnumMember]
		Legs,
		[EnumMember]
		Feet,
		[EnumMember]
		Finger1,
		[EnumMember]
		Finger2,
		[EnumMember]
		Trinket1,
		[EnumMember]
		Trinket2,
		[EnumMember]
		MainHand,
		[EnumMember]
		OffHand,
		[EnumMember]
		Ranged
	}
}