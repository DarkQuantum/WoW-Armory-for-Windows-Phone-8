using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Languages;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ItemToolTip
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public bool HasAdditionalItemSourceInformation { get; set; }
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string IconName { get; set; }
		[DataMember]
		public CharacterItemRarity ItemRarity { get; set; }
		[DataMember]
		public ItemBonding Bonding { get; set; }
		[DataMember]
		public int MaxCount { get; set; }
		[DataMember]
		public bool IsUniqueEquipabble { get; set; }
		[DataMember]
		public List<string> AllowableClasses { get; set; }
		[DataMember]
		public ItemInventoryType Slot { get; set; }
		[DataMember]
		public string SubclassName { get; set; }
		[DataMember]
		public int RequiredLevel { get; set; }
		[DataMember]
		public int ItemLevel { get; set; }
		[DataMember]
		public ItemSource ItemSource { get; set; }
		[DataMember]
		public int ItemSourceAreaId { get; set; }
		[DataMember]
		public string ItemSourceAreaName { get; set; }
		[DataMember]
		public int ItemSourceCreatureId { get; set; }
		[DataMember]
		public string ItemSourceCreatureName { get; set; }
		[DataMember]
		public string ItemSourceDifficulty { get; set; }
		[DataMember]
		public int ItemSourceDropRate { get; set; }
		[DataMember]
		public string ItemSourceValue { get; set; }
		[DataMember]
		public string ItemSourceValueText { get; set; }
		[DataMember]
		public int Armor { get; set; }
		[DataMember]
		public int ArmorBonus { get; set; }
		[DataMember]
		public int BonusStrength { get; set; }
		[DataMember]
		public int BonusAgility { get; set; }
		[DataMember]
		public int BonusStamina { get; set; }
		[DataMember]
		public int BonusIntellect { get; set; }
		[DataMember]
		public int BonusSpirit { get; set; }
		[DataMember]
		public string Enchant { get; set; }
		[DataMember]
		public int DurabilityCurrent { get; set; }
		[DataMember]
		public int DurabilityMax { get; set; }
		[DataMember]
		public string Heroic { get; set; }
		[DataMember]
		public ItemToolTipDamageData DamageData { get; set; }
		[DataMember]
		public ItemToolTipSocketData SocketData { get; set; }
		[DataMember]
		public ItemToolTipSetData SetData { get; set; }
		[DataMember]
		public Region Region { get; set; }
		[DataMember]
		public int BonusDefenseSkillRating { get; set; }
		[DataMember]
		public int BonusDodgeRating { get; set; }
		[DataMember]
		public int BonusParryRating { get; set; }
		[DataMember]
		public int BonusBlockRating { get; set; }
		[DataMember]
		public int BonusHitSpellRating { get; set; }
		[DataMember]
		public int BonusCritSpellRating { get; set; }
		[DataMember]
		public int BonusHasteSpellRating { get; set; }
		[DataMember]
		public int BonusHitRating { get; set; }
		[DataMember]
		public int BonusCritRating { get; set; }
		[DataMember]
		public int BonusHitTakenRating { get; set; }
		[DataMember]
		public int BonusResilienceRating { get; set; }
		[DataMember]
		public int BonusHasteRating { get; set; }
		[DataMember]
		public int BonusSpellPower { get; set; }
		[DataMember]
		public int BonusAttackPower { get; set; }
		[DataMember]
		public int BonusRangedAttackPower { get; set; }
		[DataMember]
		public int BonusFeralAttackPower { get; set; }
		[DataMember]
		public int BonusManaRegen { get; set; }
		[DataMember]
		public int BonusArmorPenetration { get; set; }
		[DataMember]
		public int BonusBlockValue { get; set; }
		[DataMember]
		public int BonusHealthRegen { get; set; }
		[DataMember]
		public int BonusSpellPenetration { get; set; }
		[DataMember]
		public int BonusExpertiseRating { get; set; }

		public string BondingText
		{
			get { return AppResources.ResourceManager.GetString( String.Format( "Item_Bonding_{0}", Bonding.ToString() ) ); }
		}

		public string MaxCountText
		{
			get
			{
				var result = "";

				if ( MaxCount > 0 )
				{
					result = AppResources.Item_MaxCount_Unique;
					if ( IsUniqueEquipabble )
					{
						result = String.Format( AppResources.Item_MaxCount_UniqueEquipabble, result );
					}
				}

				return result;
			}
		}

		public string SlotText
		{
			get { return AppResources.ResourceManager.GetString( String.Format( "Item_InventoryType_{0}", Slot.ToString() ) ); }
		}

		public string DurabilityText
		{
			get
			{
				var result = "";

				if ( DurabilityCurrent != 0 && DurabilityMax != 0 )
				{
					result = String.Format( AppResources.Item_Durability, DurabilityCurrent, DurabilityMax );
				}

				return result;
			}
		}

		public string AllowableClassesText
		{
			get
			{
				var result = "";

				if ( AllowableClasses != null && AllowableClasses.Count > 0 )
				{
					result = AllowableClasses.Aggregate( String.Format( "{0}: ", AppResources.Item_AllowableClasses ), ( current, allowableClass ) => String.Format( "{0}{1}, ", current, allowableClass ) );
					result = result.Substring( 0, ( result.Length - 2 ) );
				}

				return result;
			}
		}

		public string RequiredLevelText
		{
			get { return RequiredLevel > 0 ? String.Format( AppResources.Item_RequiredLevel, RequiredLevel ) : ""; }
		}

		public string ItemLevelText
		{
			get { return ItemLevel > 0 ? String.Format( AppResources.Item_Level, ItemLevel ) : ""; }
		}

		public string ArmorText
		{
			get { return Armor != 0 ? String.Format( AppResources.Item_Armor, Armor ) : ""; }
		}

		public bool IsBonusArmor
		{
			get { return ArmorBonus > 0; }
		}

		public string BonusStrengthText
		{
			get { return BonusStrength != 0 ? String.Format( AppResources.Item_BonusStrength, BonusStrength ) : ""; }
		}

		public string BonusAgilityText
		{
			get { return BonusAgility != 0 ? String.Format( AppResources.Item_BonusAgility, BonusAgility ) : ""; }
		}

		public string BonusStaminaText
		{
			get { return BonusStamina != 0 ? String.Format( AppResources.Item_BonusStamina, BonusStamina ) : ""; }
		}

		public string BonusIntellectText
		{
			get { return BonusIntellect != 0 ? String.Format( AppResources.Item_BonusIntellect, BonusIntellect ) : ""; }
		}

		public string BonusSpiritText
		{
			get { return BonusSpirit != 0 ? String.Format( AppResources.Item_BonusSpirit, BonusSpirit ) : ""; }
		}

		public string ItemSourceText
		{
			get
			{
				var result = ItemSourceValueText;

				switch ( ItemSource )
				{
					case ItemSource.CreatureDrop:
						{
							if ( String.IsNullOrEmpty( ItemSourceAreaName ) )
							{
								result = AppResources.Item_Source_bossDrop;
							}
							else
							{
								result = ItemSourceAreaName;
								HasAdditionalItemSourceInformation = true;
								if ( ItemSourceDifficulty == "h" )
								{
									result = String.Format( "{0} ({1})", result, AppResources.Item_Heroic );
								}
							}
						} break;
				}

				return result;
			}
		}

		public string ItemSourceBossName
		{
			get
			{
				var result = "";
				if ( HasAdditionalItemSourceInformation )
				{
					if ( !String.IsNullOrEmpty( ItemSourceCreatureName ) )
					{
						result = ItemSourceCreatureName;
					}
				}
				return result;
			}
		}

		public string ItemSourceDropRateText
		{
			get
			{
				var result = "";
				if ( HasAdditionalItemSourceInformation )
				{
					result = AppResources.ResourceManager.GetString( String.Format( "Item_DropRate_{0}", (int)ItemSourceDropRate ) );
				}
				return result;
			}
		}

		public string BonusDefenseSkillRatingText { get { return BonusDefenseSkillRating != 0 ? String.Format( AppResources.Item_Bonus_DefenseSkillRating, BonusDefenseSkillRating ) : ""; } }
		public string BonusDodgeRatingText { get { return BonusDodgeRating != 0 ? String.Format( AppResources.Item_Bonus_DodgeRating, BonusDodgeRating ) : ""; } }
		public string BonusParryRatingText { get { return BonusParryRating != 0 ? String.Format( AppResources.Item_Bonus_ParryRating, BonusParryRating ) : ""; } }
		public string BonusBlockRatingText { get { return BonusBlockRating != 0 ? String.Format( AppResources.Item_Bonus_BlockRating, BonusBlockRating ) : ""; } }
		public string BonusHitSpellRatingText { get { return BonusHitSpellRating != 0 ? String.Format( AppResources.Item_Bonus_HitSpellRating, BonusHitSpellRating ) : ""; } }
		public string BonusCritSpellRatingText { get { return BonusCritSpellRating != 0 ? String.Format( AppResources.Item_Bonus_CritSpellRating, BonusCritSpellRating ) : ""; } }
		public string BonusHasteSpellRatingText { get { return BonusHasteSpellRating != 0 ? String.Format( AppResources.Item_Bonus_HasteSpellRating, BonusHasteSpellRating ) : ""; } }
		public string BonusHitRatingText { get { return BonusHitRating != 0 ? String.Format( AppResources.Item_Bonus_HitRating, BonusHitRating ) : ""; } }
		public string BonusCritRatingText { get { return BonusCritRating != 0 ? String.Format( AppResources.Item_Bonus_CritRating, BonusCritRating ) : ""; } }
		public string BonusHitTakenRatingText { get { return BonusHitTakenRating != 0 ? String.Format( AppResources.Item_Bonus_HitTakenRating, BonusHitTakenRating ) : ""; } }
		public string BonusResilienceRatingText { get { return BonusResilienceRating != 0 ? String.Format( AppResources.Item_Bonus_ResilienceRating, BonusResilienceRating ) : ""; } }
		public string BonusHasteRatingText { get { return BonusHasteRating != 0 ? String.Format( AppResources.Item_Bonus_HasteRating, BonusHasteRating ) : ""; } }
		public string BonusSpellPowerText { get { return BonusSpellPower != 0 ? String.Format( AppResources.Item_Bonus_SpellPower, BonusSpellPower ) : ""; } }
		public string BonusAttackPowerText { get { return BonusAttackPower != 0 ? String.Format( AppResources.Item_Bonus_AttackPower, BonusAttackPower ) : ""; } }
		public string BonusRangedAttackPowerText { get { return BonusRangedAttackPower != 0 ? String.Format( AppResources.Item_Bonus_RangedAttackPower, BonusRangedAttackPower ) : ""; } }
		public string BonusFeralAttackPowerText { get { return BonusFeralAttackPower != 0 ? String.Format( AppResources.Item_Bonus_FeralAttackPower, BonusFeralAttackPower ) : ""; } }
		public string BonusManaRegenText { get { return BonusManaRegen != 0 ? String.Format( AppResources.Item_Bonus_ManaRegen, BonusManaRegen ) : ""; } }
		public string BonusArmorPenetrationText { get { return BonusArmorPenetration != 0 ? String.Format( AppResources.Item_Bonus_ArmorPenetration, BonusArmorPenetration ) : ""; } }
		public string BonusBlockValueText { get { return BonusBlockValue != 0 ? String.Format( AppResources.Item_Bonus_BlockValue, BonusBlockValue ) : ""; } }
		public string BonusHealthRegenText { get { return BonusHealthRegen != 0 ? String.Format( AppResources.Item_Bonus_HealthRegen, BonusHealthRegen ) : ""; } }
		public string BonusSpellPenetrationText { get { return BonusSpellPenetration != 0 ? String.Format( AppResources.Item_Bonus_SpellPenetration, BonusSpellPenetration ) : ""; } }
		public string BonusExpertiseRatingText { get { return BonusExpertiseRating != 0 ? String.Format( AppResources.Item_Bonus_ExpertiseRating, BonusExpertiseRating ) : ""; } }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemToolTip"/> class.
		/// </summary>
		public ItemToolTip()
		{
		}

		public ItemToolTip( XElement root, Region region )
		{
			Region = region;
			Id = root.Elements( "id" ).Select( e => Int32.Parse( e.Value, CultureInfo.InvariantCulture ) ).FirstOrDefault();
			Name = root.Elements( "name" ).Select( e => e.Value ).FirstOrDefault();
			IconName = root.Elements( "icon" ).Select( e => e.Value ).FirstOrDefault();
			ItemRarity = root.Elements( "overallQualityId" ).Select( e => (CharacterItemRarity)Int32.Parse( e.Value, CultureInfo.InvariantCulture ) ).FirstOrDefault();
			Bonding = root.Elements( "bonding" ).Select( e => (ItemBonding)Int32.Parse( e.Value, CultureInfo.InvariantCulture ) ).FirstOrDefault();
			AllowableClasses = root.Elements( "allowableClasses" ).Select( e => e.Elements( "class" ).Select( f => f.Value ).ToList() ).FirstOrDefault();
			Slot = root.Elements( "equipData" ).Select( e => e.Elements( "inventoryType" ).Select( f => (ItemInventoryType)Int32.Parse( f.Value, CultureInfo.InvariantCulture ) ).FirstOrDefault() ).FirstOrDefault();
			SubclassName = root.Elements( "equipData" ).Select( e => e.Elements( "subclassName" ).Select( f => f.Value ).FirstOrDefault() ).FirstOrDefault();
			RequiredLevel = root.Elements( "requiredLevel" ).Select( e => Int32.Parse( e.Value, CultureInfo.InvariantCulture ) ).FirstOrDefault();
			ItemLevel = root.Elements( "itemLevel" ).Select( e => Int32.Parse( e.Value, CultureInfo.InvariantCulture ) ).FirstOrDefault();
			Enchant = root.Elements( "enchant" ).Select( e => e.Value ).FirstOrDefault();
			DamageData = root.Elements( "damageData" ).Select( e => new ItemToolTipDamageData( e ) ).FirstOrDefault();
			SocketData = root.Elements( "socketData" ).Select( e => new ItemToolTipSocketData( e, Region ) ).FirstOrDefault();
			SetData = root.Elements( "setData" ).Select( e => new ItemToolTipSetData( e ) ).FirstOrDefault();

			var durability = root.Element( "durability" );
			if ( durability != null )
			{
				DurabilityCurrent = (int)durability.GetAttributeValue( "current", ConvertToType.Int );
				DurabilityMax = (int)durability.GetAttributeValue( "max", ConvertToType.Int );
			}

			var heroic = root.Element( "heroic" );
			if ( heroic != null )
			{
				Heroic = String.Empty;
				if ( Int32.Parse( heroic.Value, CultureInfo.InvariantCulture ) == 1 )
				{
					Heroic = AppResources.Item_Heroic;
				}
			}
			
			var maxCount = root.Element( "maxCount" );
			if ( maxCount != null )
			{
				MaxCount = Int32.Parse( maxCount.Value, CultureInfo.InvariantCulture );
				IsUniqueEquipabble = false;
				if ( !String.IsNullOrEmpty( maxCount.GetAttributeValue( "uniqueEquippable" ) ) )
				{
					if ( maxCount.GetAttributeValue( "uniqueEquippable" ).Equals( "1" ) )
					{
						IsUniqueEquipabble = true;
					}
				}
			}
			else
			{
				MaxCount = 0;
				IsUniqueEquipabble = false;
			}
			
			var armor = root.Element( "armor" );
			if ( armor != null )
			{
				Armor = Int32.Parse( armor.Value, CultureInfo.InvariantCulture );
				ArmorBonus = (int)armor.GetAttributeValue( "armorBonus", ConvertToType.Int );
			}
			else
			{
				Armor = 0;
				ArmorBonus = 0;
			}

			var bonusStrength = root.Element( "bonusStrength" );
			var bonusAgility = root.Element( "bonusAgility" );
			var bonusStamina = root.Element( "bonusStamina" );
			var bonusIntellect = root.Element( "bonusIntellect" );
			var bonusSpirit = root.Element( "bonusSpirit" );
			BonusStrength = bonusStrength != null ? Int32.Parse( bonusStrength.Value, CultureInfo.InvariantCulture ) : 0;
			BonusAgility = bonusAgility != null ? Int32.Parse( bonusAgility.Value, CultureInfo.InvariantCulture ) : 0;
			BonusStamina = bonusStamina != null ? Int32.Parse( bonusStamina.Value, CultureInfo.InvariantCulture ) : 0;
			BonusIntellect = bonusIntellect != null ? Int32.Parse( bonusIntellect.Value, CultureInfo.InvariantCulture ) : 0;
			BonusSpirit = bonusSpirit != null ? Int32.Parse( bonusSpirit.Value, CultureInfo.InvariantCulture ) : 0;

			var bonusDefenseSkillRating = root.Element( "bonusDefenseSkillRating" );
			var bonusDodgeRating = root.Element( "bonusDodgeRating" );
			var bonusParryRating = root.Element( "bonusParryRating" );
			var bonusBlockRating = root.Element( "bonusBlockRating" );
			//var bonusHitMeleeRating = root.Element( "bonusHitMeleeRating" );
			//var bonusHitRangedRating = root.Element( "bonusHitRangedRating" );
			var bonusHitSpellRating = root.Element( "bonusHitSpellRating" );
			//var bonusCritMeleeRating = root.Element( "bonusCritMeleeRating" );
			//var bonusCritRangedRating = root.Element( "bonusCritRangedRating" );
			var bonusCritSpellRating = root.Element( "bonusCritSpellRating" );
			//var bonusHitTakenMeleeRating = root.Element( "bonusHitTakenMeleeRating" );
			//var bonusHitTakenRangedRating = root.Element( "bonusHitTakenRangedRating" );
			//var bonusHitTakenSpellRating = root.Element( "bonusHitTakenSpellRating" );
			//var bonusCritTakenMeleeRating = root.Element( "bonusCritTakenMeleeRating" );
			//var bonusCritTakenRangedRating = root.Element( "bonusCritTakenRangedRating" );
			//var bonusCritTakenSpellRating = root.Element( "bonusCritTakenSpellRating" );
			//var bonusHasteMeleeRating = root.Element( "bonusHasteMeleeRating" );
			//var bonusHasteRangedRating = root.Element( "bonusHasteRangedRating" );
			var bonusHasteSpellRating = root.Element( "bonusHasteSpellRating" );
			var bonusHitRating = root.Element( "bonusHitRating" );
			var bonusCritRating = root.Element( "bonusCritRating" );
			var bonusHitTakenRating = root.Element( "bonusHitTakenRating" );
			//var bonusCritTakenRating = root.Element( "bonusCritTakenRating" );
			var bonusResilienceRating = root.Element( "bonusResilienceRating" );
			var bonusHasteRating = root.Element( "bonusHasteRating" );
			var bonusSpellPower = root.Element( "bonusSpellPower" );
			var bonusAttackPower = root.Element( "bonusAttackPower" );
			var bonusRangedAttackPower = root.Element( "bonusRangedAttackPower" );
			var bonusFeralAttackPower = root.Element( "bonusFeralAttackPower" );
			var bonusManaRegen = root.Element( "bonusManaRegen" );
			var bonusArmorPenetration = root.Element( "bonusArmorPenetration" );
			var bonusBlockValue = root.Element( "bonusBlockValue" );
			var bonusHealthRegen = root.Element( "bonusHealthRegen" );
			var bonusSpellPenetration = root.Element( "bonusSpellPenetration" );
			var bonusExpertiseRating = root.Element( "bonusExpertiseRating" );
			BonusDefenseSkillRating = bonusDefenseSkillRating != null ? Int32.Parse( bonusDefenseSkillRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusDodgeRating = bonusDodgeRating != null ? Int32.Parse( bonusDodgeRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusParryRating = bonusParryRating != null ? Int32.Parse( bonusParryRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusBlockRating = bonusBlockRating != null ? Int32.Parse( bonusBlockRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusHitMeleeRating = bonusHitMeleeRating != null ? Int32.Parse( bonusHitMeleeRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusHitRangedRating = bonusHitRangedRating != null ? Int32.Parse( bonusHitRangedRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusHitSpellRating = bonusHitSpellRating != null ? Int32.Parse( bonusHitSpellRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusCritMeleeRating = bonusCritMeleeRating != null ? Int32.Parse( bonusCritMeleeRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusCritRangedRating = bonusCritRangedRating != null ? Int32.Parse( bonusCritRangedRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusCritSpellRating = bonusCritSpellRating != null ? Int32.Parse( bonusCritSpellRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusHitTakenMeleeRating = bonusHitTakenMeleeRating != null ? Int32.Parse( bonusHitTakenMeleeRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusHitTakenRangedRating = bonusHitTakenRangedRating != null ? Int32.Parse( bonusHitTakenRangedRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusHitTakenSpellRating = bonusHitTakenSpellRating != null ? Int32.Parse( bonusHitTakenSpellRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusCritTakenMeleeRating = bonusCritTakenMeleeRating != null ? Int32.Parse( bonusCritTakenMeleeRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusCritTakenRangedRating = bonusCritTakenRangedRating != null ? Int32.Parse( bonusCritTakenRangedRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusCritTakenSpellRating = bonusCritTakenSpellRating != null ? Int32.Parse( bonusCritTakenSpellRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusHasteMeleeRating = bonusHasteMeleeRating != null ? Int32.Parse( bonusHasteMeleeRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusHasteRangedRating = bonusHasteRangedRating != null ? Int32.Parse( bonusHasteRangedRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusHasteSpellRating = bonusHasteSpellRating != null ? Int32.Parse( bonusHasteSpellRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusHitRating = bonusHitRating != null ? Int32.Parse( bonusHitRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusCritRating = bonusCritRating != null ? Int32.Parse( bonusCritRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusHitTakenRating = bonusHitTakenRating != null ? Int32.Parse( bonusHitTakenRating.Value, CultureInfo.InvariantCulture ) : 0;
			//BonusCritTakenRating = bonusCritTakenRating != null ? Int32.Parse( bonusCritTakenRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusResilienceRating = bonusResilienceRating != null ? Int32.Parse( bonusResilienceRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusHasteRating = bonusHasteRating != null ? Int32.Parse( bonusHasteRating.Value, CultureInfo.InvariantCulture ) : 0;
			BonusSpellPower = bonusSpellPower != null ? Int32.Parse( bonusSpellPower.Value, CultureInfo.InvariantCulture ) : 0;
			BonusAttackPower = bonusAttackPower != null ? Int32.Parse( bonusAttackPower.Value, CultureInfo.InvariantCulture ) : 0;
			BonusRangedAttackPower = bonusRangedAttackPower != null ? Int32.Parse( bonusRangedAttackPower.Value, CultureInfo.InvariantCulture ) : 0;
			BonusFeralAttackPower = bonusFeralAttackPower != null ? Int32.Parse( bonusFeralAttackPower.Value, CultureInfo.InvariantCulture ) : 0;
			BonusManaRegen = bonusManaRegen != null ? Int32.Parse( bonusManaRegen.Value, CultureInfo.InvariantCulture ) : 0;
			BonusArmorPenetration = bonusArmorPenetration != null ? Int32.Parse( bonusArmorPenetration.Value, CultureInfo.InvariantCulture ) : 0;
			BonusBlockValue = bonusBlockValue != null ? Int32.Parse( bonusBlockValue.Value, CultureInfo.InvariantCulture ) : 0;
			BonusHealthRegen = bonusHealthRegen != null ? Int32.Parse( bonusHealthRegen.Value, CultureInfo.InvariantCulture ) : 0;
			BonusSpellPenetration = bonusSpellPenetration != null ? Int32.Parse( bonusSpellPenetration.Value, CultureInfo.InvariantCulture ) : 0;
			BonusExpertiseRating = bonusExpertiseRating != null ? Int32.Parse( bonusExpertiseRating.Value, CultureInfo.InvariantCulture ) : 0;

			var itemSource = root.Element( "itemSource" );
			if ( itemSource != null )
			{
				ItemSourceAreaId = !String.IsNullOrEmpty( itemSource.GetAttributeValue( "areaId" ) ) ? (int)itemSource.GetAttributeValue( "areaId", ConvertToType.Int ) : 0;
				ItemSourceAreaName = !String.IsNullOrEmpty( itemSource.GetAttributeValue( "areaName" ) ) ? itemSource.GetAttributeValue( "areaName" ) : String.Empty;
				ItemSourceCreatureId = !String.IsNullOrEmpty( itemSource.GetAttributeValue( "creatureId" ) ) ? (int)itemSource.GetAttributeValue( "creatureId", ConvertToType.Int ) : 0;
				ItemSourceCreatureName = !String.IsNullOrEmpty( itemSource.GetAttributeValue( "creatureName" ) ) ? itemSource.GetAttributeValue( "creatureName" ) : String.Empty;
				ItemSourceDifficulty = !String.IsNullOrEmpty( itemSource.GetAttributeValue( "difficulty" ) ) ? itemSource.GetAttributeValue( "difficulty" ) : String.Empty;
				ItemSourceDropRate = !String.IsNullOrEmpty( itemSource.GetAttributeValue( "dropRate" ) ) ? (int)itemSource.GetAttributeValue( "dropRate", ConvertToType.Int ) : 0;
				ItemSourceValue = itemSource.GetAttributeValue( "value" );

				var sourceType = ItemSourceValue.Substring( ItemSourceValue.IndexOf( "." ) + 1 );
				ItemSourceValueText = AppResources.ResourceManager.GetString( String.Format( "Item_Source_{0}", sourceType ) );

				switch ( sourceType )
				{
					case "createdByPlans": ItemSource = ItemSource.CreatedByPlans; break;
					case "createdBySpell": ItemSource = ItemSource.CreatedBySpell; break;
					case "creatureDrop": ItemSource = ItemSource.CreatureDrop; break;
					case "creatureHerb": ItemSource = ItemSource.CreatureHerb; break;
					case "creatureMine": ItemSource = ItemSource.CreatureMine; break;
					case "creatureSkin": ItemSource = ItemSource.CreatureSkin; break;
					case "currency": ItemSource = ItemSource.Currency; break;
					case "factionReward": ItemSource = ItemSource.FactionReward; break;
					case "gameObjectDrop": ItemSource = ItemSource.GameObjectDrop; break;
					case "none": ItemSource = ItemSource.None; break;
					case "objQuest": ItemSource = ItemSource.ObjectQuest; break;
					case "pickPocket": ItemSource = ItemSource.PickPocket; break;
					case "planForItem": ItemSource = ItemSource.PlanForItems; break;
					case "providedQuest": ItemSource = ItemSource.ProvidedQuest; break;
					case "pvpReward": ItemSource = ItemSource.PvpReward; break;
					case "questReward": ItemSource = ItemSource.QuestReward; break;
					case "reagentSpell": ItemSource = ItemSource.ReagentSpell; break;
					case "startsQuest": ItemSource = ItemSource.StartsQuest; break;
					case "vendor": ItemSource = ItemSource.Vendor; break;
					case "vendorPvp": ItemSource = ItemSource.VendorPvp; break;
					case "worldDrop": ItemSource = ItemSource.WorldDrop; break;
				}
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
