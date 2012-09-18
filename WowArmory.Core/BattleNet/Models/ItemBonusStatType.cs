using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum ItemBonusStatType
	{
		[EnumMember]
		None = -1,
		[EnumMember]
		Health = 0,
		[EnumMember]
		Mana = 2,
		[EnumMember]
		Agility = 3,
		[EnumMember]
		Strength = 4,
		[EnumMember]
		Intellect = 5,
		[EnumMember]
		Spirit = 6,
		[EnumMember]
		Stamina = 7,
		[EnumMember]
		DefenseRating = 12,
		[EnumMember]
		DodgeRating = 13,
		[EnumMember]
		ParryRating = 14,
		[EnumMember]
		ShieldBlockRating = 15,
		[EnumMember]
		MeleeHitRating = 16,
		[EnumMember]
		RangedHitRating = 17,
		[EnumMember]
		SpellHitRating = 18,
		[EnumMember]
		MeleeCriticalStrikeRating = 19,
		[EnumMember]
		RangedCriticalStrikeRating = 20,
		[EnumMember]
		SpellCriticalStrikeRating = 21,
		[EnumMember]
		MeleeHitAvoidanceRating = 22,
		[EnumMember]
		RangedHitAvoidanceRating = 23,
		[EnumMember]
		SpellHitAvoidanceRating = 24,
		[EnumMember]
		MeleeCriticalAvoidanceRating = 25,
		[EnumMember]
		RangedCriticalAvoidanceRating = 26,
		[EnumMember]
		SpellCriticalAvoidanceRating = 27,
		[EnumMember]
		MeleeHasteRating = 28,
		[EnumMember]
		RangedHasteRating = 29,
		[EnumMember]
		SpellHasteRating = 30,
		[EnumMember]
		HitRating = 31,
		[EnumMember]
		CriticalStrikeRating = 32,
		[EnumMember]
		HitAvoidanceRating = 33,
		[EnumMember]
		CriticalAvoidanceRating = 34,
		[EnumMember]
		PvPResilienceRating = 35,
		[EnumMember]
		HasteRating = 36,
		[EnumMember]
		ExpertiseRating = 37,
		[EnumMember]
		AttackPower = 38,
		[EnumMember]
		RangedPower = 39,
		[EnumMember]
		FeralAttackPower = 40,
		[EnumMember]
		DamageDone = 41,
		[EnumMember]
		HealingDone = 42,
		[EnumMember]
		Mp5 = 43,
		[EnumMember]
		ArmorPenetrationRating = 44,
		[EnumMember]
		SpellPower = 45,
		[EnumMember]
		Hp5 = 46,
		[EnumMember]
		SpellPenetration = 47,
		[EnumMember]
		BlockValue = 48,
		[EnumMember]
		Mastery = 49,
        [EnumMember]
        PvPPower = 57
	}
}