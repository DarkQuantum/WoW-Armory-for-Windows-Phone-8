using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum ItemClass
	{
		[EnumMember]
		Consumable = 0,
		[EnumMember]
		Container = 1,
		[EnumMember]
		Weapon = 2,
		[EnumMember]
		Gem = 3,
		[EnumMember]
		Armor = 4,
		[EnumMember]
		Reagent = 5,
		[EnumMember]
		Projectile = 6,
		[EnumMember]
		TradeGoods = 7,
		[EnumMember]
		Recipe = 9,
		[EnumMember]
		Quiver = 11,
		[EnumMember]
		Quest = 12,
		[EnumMember]
		Key = 13,
		[EnumMember]
		Miscellaneous = 15,
		[EnumMember]
		Glyph = 16
	}
}