using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum Reforge
	{
		[EnumMember]
		None = 0,
		[EnumMember]
		SpiritToDodgeRating = 113,
		[EnumMember]
		SpiritToParryRating = 114,
		[EnumMember]
		SpiritToHitRating = 115,
		[EnumMember]
		SpiritToCritRating = 116,
		[EnumMember]
		SpiritToHasteRating = 117,
		[EnumMember]
		SpiritToExpertiseRating = 118,
		[EnumMember]
		SpiritToMastery = 119,
		[EnumMember]
		DodgeRatingToSpirit = 120,
		[EnumMember]
		DodgeRatingToParryRating = 121,
		[EnumMember]
		DodgeRatingToHitRating = 122,
		[EnumMember]
		DodgeRatingToCritRating = 123,
		[EnumMember]
		DodgeRatingToHasteRating = 124,
		[EnumMember]
		DodgeRatingToExpertiseRating = 125,
		[EnumMember]
		DodgeRatingToMastery = 126,
		[EnumMember]
		ParryRatingToSpirit = 127,
		[EnumMember]
		ParryRatingToDodgeRating = 128,
		[EnumMember]
		ParryRatingToHitRating = 129,
		[EnumMember]
		ParryRatingToCritRating = 130,
		[EnumMember]
		ParryRatingToHasteRating = 131,
		[EnumMember]
		ParryRatingToExpertiseRating = 132,
		[EnumMember]
		ParryRatingToMastery = 133,
		[EnumMember]
		HitRatingToSpirit = 134,
		[EnumMember]
		HitRatingToDodgeRating = 135,
		[EnumMember]
		HitRatingToParryRating = 136,
		[EnumMember]
		HitRatingToCritRating = 137,
		[EnumMember]
		HitRatingToHasteRating = 138,
		[EnumMember]
		HitRatingToExpertiseRating = 139,
		[EnumMember]
		HitRatingToMastery = 140,
		[EnumMember]
		CritRatingToSpirit = 141,
		[EnumMember]
		CritRatingToDodgeRating = 142,
		[EnumMember]
		CritRatingToParryRating = 143,
		[EnumMember]
		CritRatingToHitRating = 144,
		[EnumMember]
		CritRatingToHasteRating = 145,
		[EnumMember]
		CritRatingToExpertiseRating = 146,
		[EnumMember]
		CritRatingToMastery = 147,
		[EnumMember]
		HasteRatingToSpirit = 148,
		[EnumMember]
		HasteRatingToDodgeRating = 149,
		[EnumMember]
		HasteRatingToParryRating = 150,
		[EnumMember]
		HasteRatingToHitRating = 151,
		[EnumMember]
		HasteRatingToCritRating = 152,
		[EnumMember]
		HasteRatingToExpertiseRating = 153,
		[EnumMember]
		HasteRatingToMastery = 154,
		[EnumMember]
		ExpertiseRatingToSpirit = 155,
		[EnumMember]
		ExpertiseRatingToDodgeRating = 156,
		[EnumMember]
		ExpertiseRatingToParryRating = 157,
		[EnumMember]
		ExpertiseRatingToHitRating = 158,
		[EnumMember]
		ExpertiseRatingToCritRating = 159,
		[EnumMember]
		ExpertiseRatingToHasteRating = 160,
		[EnumMember]
		ExpertiseRatingToMastery = 161,
		[EnumMember]
		MasteryToSpirit = 162,
		[EnumMember]
		MasteryToDodgeRating = 163,
		[EnumMember]
		MasteryToParryRating = 164,
		[EnumMember]
		MasteryToHitRating = 165,
		[EnumMember]
		MasteryToCritRating = 166,
		[EnumMember]
		MasteryToHasteRating = 167,
		[EnumMember]
		MasteryToExpertiseRating = 168
	}
}