using System.Collections.Generic;
using WowArmory.Core.BattleNet.Models;

namespace WowArmory.Core.BattleNet.Helpers
{
	public static class ReforgeHelper
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private static Dictionary<Reforge, ReforgeStat> _reforgeMapping;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the reforge mapping.
		/// </summary>
		/// <value>
		/// The reforge mapping.
		/// </value>
		public static Dictionary<Reforge, ReforgeStat> ReforgeMapping
		{
			get
			{
				return _reforgeMapping;
			}
			set
			{
				_reforgeMapping = value;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		static ReforgeHelper()
		{
			_reforgeMapping = new Dictionary<Reforge, ReforgeStat>();
			_reforgeMapping.Add(Reforge.SpiritToDodgeRating, new ReforgeStat(ItemBonusStatType.Spirit, ItemBonusStatType.DodgeRating));
			_reforgeMapping.Add(Reforge.SpiritToParryRating, new ReforgeStat(ItemBonusStatType.Spirit, ItemBonusStatType.ParryRating));
			_reforgeMapping.Add(Reforge.SpiritToHitRating, new ReforgeStat(ItemBonusStatType.Spirit, ItemBonusStatType.HitRating));
			_reforgeMapping.Add(Reforge.SpiritToCritRating, new ReforgeStat(ItemBonusStatType.Spirit, ItemBonusStatType.CriticalStrikeRating));
			_reforgeMapping.Add(Reforge.SpiritToHasteRating, new ReforgeStat(ItemBonusStatType.Spirit, ItemBonusStatType.HasteRating));
			_reforgeMapping.Add(Reforge.SpiritToExpertiseRating, new ReforgeStat(ItemBonusStatType.Spirit, ItemBonusStatType.ExpertiseRating));
			_reforgeMapping.Add(Reforge.SpiritToMastery, new ReforgeStat(ItemBonusStatType.Spirit, ItemBonusStatType.Mastery));
			_reforgeMapping.Add(Reforge.DodgeRatingToSpirit, new ReforgeStat(ItemBonusStatType.DodgeRating, ItemBonusStatType.Spirit));
			_reforgeMapping.Add(Reforge.DodgeRatingToParryRating, new ReforgeStat(ItemBonusStatType.DodgeRating, ItemBonusStatType.ParryRating));
			_reforgeMapping.Add(Reforge.DodgeRatingToHitRating, new ReforgeStat(ItemBonusStatType.DodgeRating, ItemBonusStatType.HitRating));
			_reforgeMapping.Add(Reforge.DodgeRatingToCritRating, new ReforgeStat(ItemBonusStatType.DodgeRating, ItemBonusStatType.CriticalStrikeRating));
			_reforgeMapping.Add(Reforge.DodgeRatingToHasteRating, new ReforgeStat(ItemBonusStatType.DodgeRating, ItemBonusStatType.HasteRating));
			_reforgeMapping.Add(Reforge.DodgeRatingToExpertiseRating, new ReforgeStat(ItemBonusStatType.DodgeRating, ItemBonusStatType.ExpertiseRating));
			_reforgeMapping.Add(Reforge.DodgeRatingToMastery, new ReforgeStat(ItemBonusStatType.DodgeRating, ItemBonusStatType.Mastery));
			_reforgeMapping.Add(Reforge.ParryRatingToSpirit, new ReforgeStat(ItemBonusStatType.ParryRating, ItemBonusStatType.Spirit));
			_reforgeMapping.Add(Reforge.ParryRatingToDodgeRating, new ReforgeStat(ItemBonusStatType.ParryRating, ItemBonusStatType.DodgeRating));
			_reforgeMapping.Add(Reforge.ParryRatingToHitRating, new ReforgeStat(ItemBonusStatType.ParryRating, ItemBonusStatType.HitRating));
			_reforgeMapping.Add(Reforge.ParryRatingToCritRating, new ReforgeStat(ItemBonusStatType.ParryRating, ItemBonusStatType.CriticalStrikeRating));
			_reforgeMapping.Add(Reforge.ParryRatingToHasteRating, new ReforgeStat(ItemBonusStatType.ParryRating, ItemBonusStatType.HasteRating));
			_reforgeMapping.Add(Reforge.ParryRatingToExpertiseRating, new ReforgeStat(ItemBonusStatType.ParryRating, ItemBonusStatType.ExpertiseRating));
			_reforgeMapping.Add(Reforge.ParryRatingToMastery, new ReforgeStat(ItemBonusStatType.ParryRating, ItemBonusStatType.Mastery));
			_reforgeMapping.Add(Reforge.HitRatingToSpirit, new ReforgeStat(ItemBonusStatType.HitRating, ItemBonusStatType.Spirit));
			_reforgeMapping.Add(Reforge.HitRatingToDodgeRating, new ReforgeStat(ItemBonusStatType.HitRating, ItemBonusStatType.DodgeRating));
			_reforgeMapping.Add(Reforge.HitRatingToParryRating, new ReforgeStat(ItemBonusStatType.HitRating, ItemBonusStatType.ParryRating));
			_reforgeMapping.Add(Reforge.HitRatingToCritRating, new ReforgeStat(ItemBonusStatType.HitRating, ItemBonusStatType.CriticalStrikeRating));
			_reforgeMapping.Add(Reforge.HitRatingToHasteRating, new ReforgeStat(ItemBonusStatType.HitRating, ItemBonusStatType.HasteRating));
			_reforgeMapping.Add(Reforge.HitRatingToExpertiseRating, new ReforgeStat(ItemBonusStatType.HitRating, ItemBonusStatType.ExpertiseRating));
			_reforgeMapping.Add(Reforge.HitRatingToMastery, new ReforgeStat(ItemBonusStatType.HitRating, ItemBonusStatType.Mastery));
			_reforgeMapping.Add(Reforge.CritRatingToSpirit, new ReforgeStat(ItemBonusStatType.CriticalStrikeRating, ItemBonusStatType.Spirit));
			_reforgeMapping.Add(Reforge.CritRatingToDodgeRating, new ReforgeStat(ItemBonusStatType.CriticalStrikeRating, ItemBonusStatType.DodgeRating));
			_reforgeMapping.Add(Reforge.CritRatingToParryRating, new ReforgeStat(ItemBonusStatType.CriticalStrikeRating, ItemBonusStatType.ParryRating));
			_reforgeMapping.Add(Reforge.CritRatingToHitRating, new ReforgeStat(ItemBonusStatType.CriticalStrikeRating, ItemBonusStatType.HitRating));
			_reforgeMapping.Add(Reforge.CritRatingToHasteRating, new ReforgeStat(ItemBonusStatType.CriticalStrikeRating, ItemBonusStatType.HasteRating));
			_reforgeMapping.Add(Reforge.CritRatingToExpertiseRating, new ReforgeStat(ItemBonusStatType.CriticalStrikeRating, ItemBonusStatType.ExpertiseRating));
			_reforgeMapping.Add(Reforge.CritRatingToMastery, new ReforgeStat(ItemBonusStatType.CriticalStrikeRating, ItemBonusStatType.Mastery));
			_reforgeMapping.Add(Reforge.HasteRatingToSpirit, new ReforgeStat(ItemBonusStatType.HasteRating, ItemBonusStatType.Spirit));
			_reforgeMapping.Add(Reforge.HasteRatingToDodgeRating, new ReforgeStat(ItemBonusStatType.HasteRating, ItemBonusStatType.DodgeRating));
			_reforgeMapping.Add(Reforge.HasteRatingToParryRating, new ReforgeStat(ItemBonusStatType.HasteRating, ItemBonusStatType.ParryRating));
			_reforgeMapping.Add(Reforge.HasteRatingToHitRating, new ReforgeStat(ItemBonusStatType.HasteRating, ItemBonusStatType.HitRating));
			_reforgeMapping.Add(Reforge.HasteRatingToCritRating, new ReforgeStat(ItemBonusStatType.HasteRating, ItemBonusStatType.CriticalStrikeRating));
			_reforgeMapping.Add(Reforge.HasteRatingToExpertiseRating, new ReforgeStat(ItemBonusStatType.HasteRating, ItemBonusStatType.ExpertiseRating));
			_reforgeMapping.Add(Reforge.HasteRatingToMastery, new ReforgeStat(ItemBonusStatType.HasteRating, ItemBonusStatType.Mastery));
			_reforgeMapping.Add(Reforge.ExpertiseRatingToSpirit, new ReforgeStat(ItemBonusStatType.ExpertiseRating, ItemBonusStatType.Spirit));
			_reforgeMapping.Add(Reforge.ExpertiseRatingToDodgeRating, new ReforgeStat(ItemBonusStatType.ExpertiseRating, ItemBonusStatType.DodgeRating));
			_reforgeMapping.Add(Reforge.ExpertiseRatingToParryRating, new ReforgeStat(ItemBonusStatType.ExpertiseRating, ItemBonusStatType.ParryRating));
			_reforgeMapping.Add(Reforge.ExpertiseRatingToHitRating, new ReforgeStat(ItemBonusStatType.ExpertiseRating, ItemBonusStatType.HitRating));
			_reforgeMapping.Add(Reforge.ExpertiseRatingToCritRating, new ReforgeStat(ItemBonusStatType.ExpertiseRating, ItemBonusStatType.CriticalStrikeRating));
			_reforgeMapping.Add(Reforge.ExpertiseRatingToHasteRating, new ReforgeStat(ItemBonusStatType.ExpertiseRating, ItemBonusStatType.HasteRating));
			_reforgeMapping.Add(Reforge.ExpertiseRatingToMastery, new ReforgeStat(ItemBonusStatType.ExpertiseRating, ItemBonusStatType.Mastery));
			_reforgeMapping.Add(Reforge.MasteryToSpirit, new ReforgeStat(ItemBonusStatType.Mastery, ItemBonusStatType.Spirit));
			_reforgeMapping.Add(Reforge.MasteryToDodgeRating, new ReforgeStat(ItemBonusStatType.Mastery, ItemBonusStatType.DodgeRating));
			_reforgeMapping.Add(Reforge.MasteryToParryRating, new ReforgeStat(ItemBonusStatType.Mastery, ItemBonusStatType.ParryRating));
			_reforgeMapping.Add(Reforge.MasteryToHitRating, new ReforgeStat(ItemBonusStatType.Mastery, ItemBonusStatType.HitRating));
			_reforgeMapping.Add(Reforge.MasteryToCritRating, new ReforgeStat(ItemBonusStatType.Mastery, ItemBonusStatType.CriticalStrikeRating));
			_reforgeMapping.Add(Reforge.MasteryToHasteRating, new ReforgeStat(ItemBonusStatType.Mastery, ItemBonusStatType.HasteRating));
			_reforgeMapping.Add(Reforge.MasteryToExpertiseRating, new ReforgeStat(ItemBonusStatType.Mastery, ItemBonusStatType.ExpertiseRating));
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}