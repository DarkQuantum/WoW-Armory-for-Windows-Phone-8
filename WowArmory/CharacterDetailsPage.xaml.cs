// 
// Copyright (c) 2010 Christian Krueger <christian.krueger@krueger-c.com>
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge and for non-commercial usage
// only, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify,
// merge, publish, and/or distribute copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WowArmory.Core;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Languages;
using WowArmory.Core.Models;
using WowArmory.Core.Pages;
using WowArmory.Core.Storage;
using WowArmory.ViewModels;

namespace WowArmory
{
	public partial class CharacterDetailsPage : PhoneApplicationPage
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private Region _prevRegion;
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public CharacterDetailsViewModel ViewModel
		{
			get { return (CharacterDetailsViewModel)DataContext; }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public CharacterDetailsPage()
		{
			InitializeComponent();

			imgBookmarkButton.Source = ViewModel.BookmarkImage;

			if ( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.TalentSpecs != null )
			{
				if ( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.TalentSpecs.Primary != null && ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.TalentSpecs.Primary.HasSpec )
				{
					gTalentsPrimary.Visibility = Visibility.Visible;
					if ( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.TalentSpecs.Primary.IsActive )
					{
						tbTalentSpecPrimaryName.Style = (Style)Resources[ "TalentNameActiveTextStyle" ];
						tbTalentSpecPrimaryTreeOne.Style = (Style)Resources[ "TalentTreeActiveTextStyle" ];
						tbTalentSpecPrimaryTreeOneSeparator.Style = (Style)Resources[ "TalentTreeActiveTextStyle" ];
						tbTalentSpecPrimaryTreeTwo.Style = (Style)Resources[ "TalentTreeActiveTextStyle" ];
						tbTalentSpecPrimaryTreeTwoSeparator.Style = (Style)Resources[ "TalentTreeActiveTextStyle" ];
						tbTalentSpecPrimaryTreeThree.Style = (Style)Resources[ "TalentTreeActiveTextStyle" ];
					}
				}
				if ( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.TalentSpecs.Secondary != null && ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.TalentSpecs.Secondary.HasSpec )
				{
					gTalentsSecondary.Visibility = Visibility.Visible;
					if ( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.TalentSpecs.Secondary.IsActive )
					{
						tbTalentSpecSecondaryName.Style = (Style)Resources[ "TalentNameActiveTextStyle" ];
						tbTalentSpecSecondaryTreeOne.Style = (Style)Resources[ "TalentTreeActiveTextStyle" ];
						tbTalentSpecSecondaryTreeOneSeparator.Style = (Style)Resources[ "TalentTreeActiveTextStyle" ];
						tbTalentSpecSecondaryTreeTwo.Style = (Style)Resources[ "TalentTreeActiveTextStyle" ];
						tbTalentSpecSecondaryTreeTwoSeparator.Style = (Style)Resources[ "TalentTreeActiveTextStyle" ];
						tbTalentSpecSecondaryTreeThree.Style = (Style)Resources[ "TalentTreeActiveTextStyle" ];
					}
				}
			}

			BuildReputation();
			BuildProfession();
			BuildActivityFeed();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		private void GearItemHead_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Head );
		}

		private void GearItemNeck_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Neck );
		}

		private void GearItemShoulder_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Shoulder );
		}

		private void GearItemCape_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Cape );
		}

		private void GearItemChest_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Chest );
		}

		private void GearItemShirt_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Shirt );
		}

		private void GearItemTabard_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Tabard );
		}

		private void GearItemWrist_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Wrist );
		}

		private void GearItemMainHand_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.MainHand );
		}

		private void GearItemSecondaryHand_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.SecondaryHand );
		}

		private void GearItemRanged_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Ranged );
		}

		private void GearItemHands_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Hands );
		}

		private void GearItemWaist_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Waist );
		}

		private void GearItemLegs_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Legs );
		}

		private void GearItemFeet_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.Feet );
		}

		private void GearItemFingerLeft_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.FingerLeft );
		}

		private void GearItemFingerRight_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.FingerRight );
		}

		private void GearItemTrinketLeft_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.TrinketLeft );
		}

		private void GearItemTrinketRight_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			ToggleToolTip( CharacterItemSlot.TrinketRight );
		}

		private void ToggleToolTip( CharacterItemSlot slot )
		{
			FrameworkElement rootElement = imgHeadSelected;
			switch ( slot )
			{
				case CharacterItemSlot.Head: { rootElement = imgHeadSelected; } break;
				case CharacterItemSlot.Neck: { rootElement = imgNeckSelected; } break;
				case CharacterItemSlot.Shoulder: { rootElement = imgShoulderSelected; } break;
				case CharacterItemSlot.Cape: { rootElement = imgCapeSelected; } break;
				case CharacterItemSlot.Chest: { rootElement = imgChestSelected; } break;
				case CharacterItemSlot.Shirt: { rootElement = imgShirtSelected; } break;
				case CharacterItemSlot.Tabard: { rootElement = imgTabardSelected; } break;
				case CharacterItemSlot.Wrist: { rootElement = imgWristSelected; } break;
				case CharacterItemSlot.MainHand: { rootElement = imgMainHandSelected; } break;
				case CharacterItemSlot.SecondaryHand: { rootElement = imgSecondaryHandSelected; } break;
				case CharacterItemSlot.Ranged: { rootElement = imgRangedSelected; } break;
				case CharacterItemSlot.Hands: { rootElement = imgHandsSelected; } break;
				case CharacterItemSlot.Waist: { rootElement = imgWaistSelected; } break;
				case CharacterItemSlot.Legs: { rootElement = imgLegsSelected; } break;
				case CharacterItemSlot.Feet: { rootElement = imgFeetSelected; } break;
				case CharacterItemSlot.FingerLeft: { rootElement = imgFingerLeftSelected; } break;
				case CharacterItemSlot.FingerRight: { rootElement = imgFingerRightSelected; } break;
				case CharacterItemSlot.TrinketLeft: { rootElement = imgTrinketLeftSelected; } break;
				case CharacterItemSlot.TrinketRight: { rootElement = imgTrinketRightSelected; } break;
			}

			var isSelected = rootElement.Visibility == Visibility.Visible;

			HideItemTooltip();

			if ( !isSelected )
			{
				rootElement.Visibility = Visibility.Visible;
				RetrieveItemToolTip( slot );
			}
		}

		private bool IsItemTooltipShown()
		{
			if ( imgHeadSelected.Visibility == Visibility.Visible ) return true;
			if ( imgNeckSelected.Visibility == Visibility.Visible ) return true;
			if ( imgShoulderSelected.Visibility == Visibility.Visible ) return true;
			if ( imgCapeSelected.Visibility == Visibility.Visible ) return true;
			if ( imgChestSelected.Visibility == Visibility.Visible ) return true;
			if ( imgShirtSelected.Visibility == Visibility.Visible ) return true;
			if ( imgTabardSelected.Visibility == Visibility.Visible ) return true;
			if ( imgWristSelected.Visibility == Visibility.Visible ) return true;
			if ( imgMainHandSelected.Visibility == Visibility.Visible ) return true;
			if ( imgSecondaryHandSelected.Visibility == Visibility.Visible ) return true;
			if ( imgRangedSelected.Visibility == Visibility.Visible ) return true;
			if ( imgHandsSelected.Visibility == Visibility.Visible ) return true;
			if ( imgWaistSelected.Visibility == Visibility.Visible ) return true;
			if ( imgLegsSelected.Visibility == Visibility.Visible ) return true;
			if ( imgFeetSelected.Visibility == Visibility.Visible ) return true;
			if ( imgFingerLeftSelected.Visibility == Visibility.Visible ) return true;
			if ( imgFingerRightSelected.Visibility == Visibility.Visible ) return true;
			if ( imgTrinketLeftSelected.Visibility == Visibility.Visible ) return true;
			if ( imgTrinketRightSelected.Visibility == Visibility.Visible ) return true;

			return false;
		}

		private void HideItemTooltip()
		{
			imgHeadSelected.Visibility = Visibility.Collapsed;
			imgNeckSelected.Visibility = Visibility.Collapsed;
			imgShoulderSelected.Visibility = Visibility.Collapsed;
			imgCapeSelected.Visibility = Visibility.Collapsed;
			imgChestSelected.Visibility = Visibility.Collapsed;
			imgShirtSelected.Visibility = Visibility.Collapsed;
			imgTabardSelected.Visibility = Visibility.Collapsed;
			imgWristSelected.Visibility = Visibility.Collapsed;
			imgMainHandSelected.Visibility = Visibility.Collapsed;
			imgSecondaryHandSelected.Visibility = Visibility.Collapsed;
			imgRangedSelected.Visibility = Visibility.Collapsed;
			imgHandsSelected.Visibility = Visibility.Collapsed;
			imgWaistSelected.Visibility = Visibility.Collapsed;
			imgLegsSelected.Visibility = Visibility.Collapsed;
			imgFeetSelected.Visibility = Visibility.Collapsed;
			imgFingerLeftSelected.Visibility = Visibility.Collapsed;
			imgFingerRightSelected.Visibility = Visibility.Collapsed;
			imgTrinketLeftSelected.Visibility = Visibility.Collapsed;
			imgTrinketRightSelected.Visibility = Visibility.Collapsed;

			brdItemToolTip.Visibility = Visibility.Collapsed;
			svCharacterData.IsEnabled = true;
			svCharacterData.Opacity = 1.0;
		}

		private void RetrieveItemToolTip( CharacterItemSlot slot )
		{
			var itemId = 0;
			switch ( slot )
			{
				case CharacterItemSlot.Head: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Head.Id; } break;
				case CharacterItemSlot.Neck: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Neck.Id; } break;
				case CharacterItemSlot.Shoulder: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Shoulder.Id; } break;
				case CharacterItemSlot.Cape: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Cape.Id; } break;
				case CharacterItemSlot.Chest: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Chest.Id; } break;
				case CharacterItemSlot.Shirt: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Shirt.Id; } break;
				case CharacterItemSlot.Tabard: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Tabard.Id; } break;
				case CharacterItemSlot.Wrist: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Wrist.Id; } break;
				case CharacterItemSlot.MainHand: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.MainHand.Id; } break;
				case CharacterItemSlot.SecondaryHand: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.SecondaryHand.Id; } break;
				case CharacterItemSlot.Ranged: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Ranged.Id; } break;
				case CharacterItemSlot.Hands: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Hands.Id; } break;
				case CharacterItemSlot.Waist: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Waist.Id; } break;
				case CharacterItemSlot.Legs: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Legs.Id; } break;
				case CharacterItemSlot.Feet: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.Feet.Id; } break;
				case CharacterItemSlot.FingerLeft: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.FingerLeft.Id; } break;
				case CharacterItemSlot.FingerRight: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.FingerRight.Id; } break;
				case CharacterItemSlot.TrinketLeft: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.TrinketLeft.Id; } break;
				case CharacterItemSlot.TrinketRight: { itemId = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Items.TrinketRight.Id; } break;
			}
			var region = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Region;
			var realmName = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Realm;
			var characterName = ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Name;
			var slotId = (int)slot;

			ClearToolTip();
			ShowToolTipInfo( tbItemToolTipLoading, AppResources.UI_String_Loading );
			pbLoading.Visibility = Visibility.Visible;
			pbLoading.IsIndeterminate = true;
			brdItemToolTip.Visibility = Visibility.Visible;
			svCharacterData.IsEnabled = false;
			svCharacterData.Opacity = 0.3;

			try
			{
				Armory.Current.GetItemTooltipPageAsync( itemId, region, realmName, characterName, slotId, GotToolTip );
			}
			catch ( Exception ex )
			{
				ClearToolTip();
				ShowToolTipInfo( tbItemToolTipLoading, AppResources.UI_CharacterDetails_CouldNotLoadItemToolTip );
				brdItemToolTip.Visibility = Visibility.Visible;
			}
		}

		private void GotToolTip( ItemToolTipPage toolTipPage )
		{
			ClearToolTip();

			try
			{
				ShowToolTipInfo( tbItemToolTipName, toolTipPage.ItemToolTip.Name );
				tbItemToolTipName.Style = (Style)Resources[ String.Format( "ItemInfoTitleText{0}Style", ( (int)toolTipPage.ItemToolTip.ItemRarity ) ) ];
				ShowToolTipInfo( tbItemToolTipHeroic, toolTipPage.ItemToolTip.Heroic );
				ShowToolTipInfo( tbItemToolTipBonding, toolTipPage.ItemToolTip.BondingText );
				ShowToolTipInfo( tbItemToolTipMaxCount, toolTipPage.ItemToolTip.MaxCountText );
				ShowToolTipInfo( tbItemToolTipEquipDataInventoryType, toolTipPage.ItemToolTip.SlotText );
				ShowToolTipInfo( tbItemToolTipEquipDataSubclassName, toolTipPage.ItemToolTip.SubclassName );
				ShowToolTipInfo( tbItemToolTipArmor, toolTipPage.ItemToolTip.ArmorText );
				if ( toolTipPage.ItemToolTip.Armor != 0 && toolTipPage.ItemToolTip.IsBonusArmor )
				{
					tbItemToolTipArmor.Style = (Style)Resources[ "ItemInfoHighlightStyle" ];
				}
				ShowToolTipInfo( tbItemToolTipBonusStrength, toolTipPage.ItemToolTip.BonusStrengthText );
				ShowToolTipInfo( tbItemToolTipBonusAgility, toolTipPage.ItemToolTip.BonusAgilityText );
				ShowToolTipInfo( tbItemToolTipBonusStamina, toolTipPage.ItemToolTip.BonusStaminaText );
				ShowToolTipInfo( tbItemToolTipBonusIntellect, toolTipPage.ItemToolTip.BonusIntellectText );
				ShowToolTipInfo( tbItemToolTipBonusSpirit, toolTipPage.ItemToolTip.BonusSpiritText );
				ShowToolTipInfo( tbItemToolTipEnchant, toolTipPage.ItemToolTip.Enchant );
				ShowToolTipInfo( tbItemToolTipDurability, toolTipPage.ItemToolTip.DurabilityText );
				ShowToolTipInfo( tbItemToolTipAllowedClasses, toolTipPage.ItemToolTip.AllowableClassesText );
				ShowToolTipInfo( tbItemToolTipRequiredLevel, toolTipPage.ItemToolTip.RequiredLevelText );
				ShowToolTipInfo( tbItemToolTipItemLevel, toolTipPage.ItemToolTip.ItemLevelText );
				if ( !String.IsNullOrEmpty( toolTipPage.ItemToolTip.ItemSourceText ) )
				{
					ShowToolTipInfo( tbItemToolTipSource, toolTipPage.ItemToolTip.ItemSourceText );
					spItemToolTipSource.Visibility = Visibility.Visible;
				}
				if ( !String.IsNullOrEmpty( toolTipPage.ItemToolTip.ItemSourceBossName ) )
				{
					ShowToolTipInfo( tbItemToolTipSourceBoss, toolTipPage.ItemToolTip.ItemSourceBossName );
					spItemToolTipSourceBoss.Visibility = Visibility.Visible;
				}
				if ( !String.IsNullOrEmpty( toolTipPage.ItemToolTip.ItemSourceDropRateText ) )
				{
					ShowToolTipInfo( tbItemToolTipSourceDropRate, toolTipPage.ItemToolTip.ItemSourceDropRateText );
					spItemToolTipSourceDropRate.Visibility = Visibility.Visible;
				}
				if ( toolTipPage.ItemToolTip.SocketData != null )
				{
					foreach ( var socket in toolTipPage.ItemToolTip.SocketData.Sockets )
					{
						var socketGrid = new Grid();
						socketGrid.ColumnDefinitions.Add( new ColumnDefinition { Width = GridLength.Auto } );
						socketGrid.ColumnDefinitions.Add( new ColumnDefinition() );

						var socketImage = new Image();
						socketImage.Source = socket.IconImage;
						socketImage.Margin = new Thickness( 0, 2, 4, 0 );
						socketImage.VerticalAlignment = VerticalAlignment.Top;
						Grid.SetRow( socketImage, 0 );
						Grid.SetColumn( socketImage, 0 );

						var socketTextBlock = new TextBlock();
						socketTextBlock.Text = socket.EnchantText;
						socketTextBlock.TextWrapping = TextWrapping.Wrap;
						if ( !socket.IsEmpty )
						{
							socketTextBlock.Style = (Style)Resources[ "ItemInfoTextStyle" ];
						}
						else
						{
							socketTextBlock.Style = (Style)Resources[ "ItemInfoSetBonusInactiveStyle" ];
						}
						Grid.SetRow( socketTextBlock, 0 );
						Grid.SetColumn( socketTextBlock, 1 );

						socketGrid.Children.Add( socketImage );
						socketGrid.Children.Add( socketTextBlock );

						spItemToolTipSockets.Children.Add( socketGrid );
					}

					if ( spItemToolTipSockets.Children.Count > 0 )
					{
						spItemToolTipSockets.Visibility = Visibility.Visible;
					}

					ShowToolTipInfo( tbItemToolTipSocketMatchEnchant, toolTipPage.ItemToolTip.SocketData.MatchEnchantText );
					if ( toolTipPage.ItemToolTip.SocketData.IsMatching )
					{
						tbItemToolTipSocketMatchEnchant.Style = (Style)Resources[ "ItemInfoSetBonusActiveStyle" ];
					}
					else
					{
						tbItemToolTipSocketMatchEnchant.Style = (Style)Resources[ "ItemInfoSetBonusInactiveStyle" ];
					}
				}
				if ( toolTipPage.ItemToolTip.SetData != null )
				{
					ShowToolTipInfo( tbItemToolTipSetName, toolTipPage.ItemToolTip.SetData.SetNameText );
					foreach ( var item in toolTipPage.ItemToolTip.SetData.Items )
					{
						var itemTextBlock = new TextBlock();
						itemTextBlock.Text = item.Name;
						if ( item.IsEquipped )
						{
							itemTextBlock.Style = (Style)Resources[ "ItemInfoSetActiveStyle" ];
						}
						else
						{
							itemTextBlock.Style = (Style)Resources[ "ItemInfoSetInactiveStyle" ];
						}
						spItemToolTipSetItems.Children.Add( itemTextBlock );
					}
					if ( spItemToolTipSetItems.Children.Count > 0 )
					{
						spItemToolTipSetItems.Visibility = Visibility.Visible;
					}
					foreach ( var bonus in toolTipPage.ItemToolTip.SetData.Bonus )
					{
						var bonusTextBlock = new TextBlock();
						bonusTextBlock.Text = bonus.DescriptionText;
						if ( spItemToolTipSetBonus.Children.Count > 0 )
						{
							bonusTextBlock.Margin = new Thickness( 0, 6, 0, 0 );
						}
						if ( bonus.Threshold <= toolTipPage.ItemToolTip.SetData.SetEquippedItems )
						{
							bonusTextBlock.Style = (Style)Resources[ "ItemInfoSetBonusActiveStyle" ];
						}
						else
						{
							bonusTextBlock.Style = (Style)Resources[ "ItemInfoSetBonusInactiveStyle" ];
						}
						spItemToolTipSetBonus.Children.Add( bonusTextBlock );
					}
					if ( spItemToolTipSetBonus.Children.Count > 0 )
					{
						spItemToolTipSetBonus.Visibility = Visibility.Visible;
					}
					spItemToolTipSetData.Visibility = Visibility.Visible;
				}
				ShowToolTipInfo( tbItemToolTipBonusDefenseRating, toolTipPage.ItemToolTip.BonusDefenseSkillRatingText );
				ShowToolTipInfo( tbItemToolTipBonusDodgeRating, toolTipPage.ItemToolTip.BonusDodgeRatingText );
				ShowToolTipInfo( tbItemToolTipBonusParryRating, toolTipPage.ItemToolTip.BonusParryRatingText );
				ShowToolTipInfo( tbItemToolTipBonusBlockRating, toolTipPage.ItemToolTip.BonusBlockRatingText );
				ShowToolTipInfo( tbItemToolTipBonusHitSpellRating, toolTipPage.ItemToolTip.BonusHitSpellRatingText );
				ShowToolTipInfo( tbItemToolTipBonusCritSpellRating, toolTipPage.ItemToolTip.BonusCritSpellRatingText );
				ShowToolTipInfo( tbItemToolTipBonusHasteSpellRating, toolTipPage.ItemToolTip.BonusHasteSpellRatingText );
				ShowToolTipInfo( tbItemToolTipBonusHitRating, toolTipPage.ItemToolTip.BonusHitRatingText );
				ShowToolTipInfo( tbItemToolTipBonusCritRating, toolTipPage.ItemToolTip.BonusCritRatingText );
				ShowToolTipInfo( tbItemToolTipBonusHitTakenRating, toolTipPage.ItemToolTip.BonusHitTakenRatingText );
				ShowToolTipInfo( tbItemToolTipBonusResilienceRating, toolTipPage.ItemToolTip.BonusResilienceRatingText );
				ShowToolTipInfo( tbItemToolTipBonusHasteRating, toolTipPage.ItemToolTip.BonusHasteRatingText );
				ShowToolTipInfo( tbItemToolTipBonusSpellPower, toolTipPage.ItemToolTip.BonusSpellPowerText );
				ShowToolTipInfo( tbItemToolTipBonusAttackPower, toolTipPage.ItemToolTip.BonusAttackPowerText );
				ShowToolTipInfo( tbItemToolTipBonusRangedAttackPower, toolTipPage.ItemToolTip.BonusRangedAttackPowerText );
				ShowToolTipInfo( tbItemToolTipBonusFeralAttackPower, toolTipPage.ItemToolTip.BonusFeralAttackPowerText );
				ShowToolTipInfo( tbItemToolTipBonusManaRegen, toolTipPage.ItemToolTip.BonusManaRegenText );
				ShowToolTipInfo( tbItemToolTipBonusArmorPenetration, toolTipPage.ItemToolTip.BonusArmorPenetrationText );
				ShowToolTipInfo( tbItemToolTipBonusBlockValue, toolTipPage.ItemToolTip.BonusBlockValueText );
				ShowToolTipInfo( tbItemToolTipBonusHealthRegen, toolTipPage.ItemToolTip.BonusHealthRegenText );
				ShowToolTipInfo( tbItemToolTipBonusSpellPenetration, toolTipPage.ItemToolTip.BonusSpellPenetrationText );
				ShowToolTipInfo( tbItemToolTipBonusExpertiseRating, toolTipPage.ItemToolTip.BonusExpertiseRatingText );
				ShowToolTipInfo( tbItemToolTipDamage, toolTipPage.ItemToolTip.DamageData.DamageText );
				ShowToolTipInfo( tbItemToolTipSpeed, toolTipPage.ItemToolTip.DamageData.SpeedText );
				ShowToolTipInfo( tbItemToolTipDamagePerSecond, toolTipPage.ItemToolTip.DamageData.DamagePerSecondText );

				brdItemToolTip.Visibility = Visibility.Visible;
			}
			catch ( Exception ex )
			{

			}
		}

		private void ShowToolTipInfo( FrameworkElement element, string text )
		{
			( (TextBlock)element ).Text = text;
			element.Visibility = !String.IsNullOrEmpty( text ) ? Visibility.Visible : Visibility.Collapsed;
		}

		private void ClearToolTip()
		{
			ShowToolTipInfo( tbItemToolTipLoading, "" );
			ShowToolTipInfo( tbItemToolTipName, "" );
			ShowToolTipInfo( tbItemToolTipHeroic, "" );
			ShowToolTipInfo( tbItemToolTipBonding, "" );
			ShowToolTipInfo( tbItemToolTipMaxCount, "" );
			ShowToolTipInfo( tbItemToolTipEquipDataInventoryType, "" );
			ShowToolTipInfo( tbItemToolTipEquipDataSubclassName, "" );
			ShowToolTipInfo( tbItemToolTipArmor, "" );
			ShowToolTipInfo( tbItemToolTipBonusStrength, "" );
			ShowToolTipInfo( tbItemToolTipBonusAgility, "" );
			ShowToolTipInfo( tbItemToolTipBonusStamina, "" );
			ShowToolTipInfo( tbItemToolTipBonusIntellect, "" );
			ShowToolTipInfo( tbItemToolTipBonusSpirit, "" );
			ShowToolTipInfo( tbItemToolTipEnchant, "" );
			ShowToolTipInfo( tbItemToolTipDurability, "" );
			ShowToolTipInfo( tbItemToolTipAllowedClasses, "" );
			ShowToolTipInfo( tbItemToolTipRequiredLevel, "" );
			ShowToolTipInfo( tbItemToolTipItemLevel, "" );
			ShowToolTipInfo( tbItemToolTipSocketMatchEnchant, "" );
			ShowToolTipInfo( tbItemToolTipSource, "" );
			ShowToolTipInfo( tbItemToolTipSourceBoss, "" );
			ShowToolTipInfo( tbItemToolTipSourceDropRate, "" );
			ShowToolTipInfo( tbItemToolTipBonusDefenseRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusDodgeRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusParryRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusBlockRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusHitSpellRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusCritSpellRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusHasteSpellRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusHitRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusCritRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusHitTakenRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusResilienceRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusHasteRating, "" );
			ShowToolTipInfo( tbItemToolTipBonusSpellPower, "" );
			ShowToolTipInfo( tbItemToolTipBonusAttackPower, "" );
			ShowToolTipInfo( tbItemToolTipBonusRangedAttackPower, "" );
			ShowToolTipInfo( tbItemToolTipBonusFeralAttackPower, "" );
			ShowToolTipInfo( tbItemToolTipBonusManaRegen, "" );
			ShowToolTipInfo( tbItemToolTipBonusArmorPenetration, "" );
			ShowToolTipInfo( tbItemToolTipBonusBlockValue, "" );
			ShowToolTipInfo( tbItemToolTipBonusHealthRegen, "" );
			ShowToolTipInfo( tbItemToolTipBonusSpellPenetration, "" );
			ShowToolTipInfo( tbItemToolTipBonusExpertiseRating, "" );
			ShowToolTipInfo( tbItemToolTipSetName, "" );
			ShowToolTipInfo( tbItemToolTipDamage, "" );
			ShowToolTipInfo( tbItemToolTipSpeed, "" );
			ShowToolTipInfo( tbItemToolTipDamagePerSecond, "" );
			spItemToolTipSockets.Children.Clear();
			spItemToolTipSockets.Visibility = Visibility.Collapsed;
			spItemToolTipSetData.Visibility = Visibility.Collapsed;
			spItemToolTipSetItems.Children.Clear();
			spItemToolTipSetItems.Visibility = Visibility.Collapsed;
			spItemToolTipSetBonus.Children.Clear();
			spItemToolTipSetBonus.Visibility = Visibility.Collapsed;
			spItemToolTipSource.Visibility = Visibility.Collapsed;
			spItemToolTipSourceBoss.Visibility = Visibility.Collapsed;
			spItemToolTipSourceDropRate.Visibility = Visibility.Collapsed;
			pbLoading.Visibility = Visibility.Collapsed;
			pbLoading.IsIndeterminate = false;
			spItemToolTip.InvalidateArrange();
			spItemToolTip.InvalidateMeasure();
			brdItemToolTip.Visibility = Visibility.Collapsed;
		}

		private void ToggleBookmark( object sender, RoutedEventArgs e )
		{
			if ( StorageManager.IsCharacterStored( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Realm, ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Name ) )
			{
				StorageManager.DeleteCharacter( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Realm, ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Name );
				//ViewModel.SelectedCharacter.IsBookmarked = false;
			}
			else
			{
				StorageManager.StoreCharacter( ViewModel.SelectedCharacter );
				//ViewModel.SelectedCharacter.IsBookmarked = true;
			}

			imgBookmarkButton.Source = ViewModel.BookmarkImage;
		}

		private void BuildReputation()
		{
			spReputation.Children.Clear();

			if ( ViewModel.SelectedCharacter.CharacterReputationPage != null &&
				ViewModel.SelectedCharacter.CharacterReputationPage.Factions != null &&
				ViewModel.SelectedCharacter.CharacterReputationPage.Factions.Count > 0 )
			{
				foreach ( var faction in ViewModel.SelectedCharacter.CharacterReputationPage.Factions )
				{
					AddReputationFaction( faction, 0 );
				}
			}
		}

		private void BuildProfession()
		{
			spProfession.Children.Clear();

			if ( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Professions != null &&
				( ( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Professions.Primary != null &&
					ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Professions.Primary.Count > 0 ) ||
				( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Professions.Secondary != null &&
					ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Professions.Secondary.Count > 0 ) ) )
			{
				var headerTextBlock = new TextBlock();
				headerTextBlock.Text = AppResources.UI_CharacterDetails_Professions_Primary;
				headerTextBlock.Style = (Style)Resources[ "ProfessionHeaderTextStyle" ];
				spProfession.Children.Add( headerTextBlock );

				if ( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Professions.Primary.Count > 0 )
				{
					foreach ( var profession in ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Professions.Primary )
					{
						AddProfession( profession );
					}
				}
				else
				{
					var infoTextBlock = new TextBlock();
					infoTextBlock.Text = AppResources.UI_CharacterDetails_Professions_Primary_None;
					infoTextBlock.Style = (Style)Resources[ "ProfessionTextStyle" ];
					spProfession.Children.Add( infoTextBlock );
				}

				headerTextBlock = new TextBlock();
				headerTextBlock.Text = AppResources.UI_CharacterDetails_Professions_Secondary;
				headerTextBlock.Style = (Style)Resources[ "ProfessionHeaderTextStyle" ];
				headerTextBlock.Margin = new Thickness( 0, 12, 0, 0 );
				spProfession.Children.Add( headerTextBlock );

				if ( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Professions.Secondary.Count > 0 )
				{
					foreach ( var profession in ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.CharacterTab.Professions.Secondary )
					{
						AddProfession( profession );
					}
				}
				else
				{
					var infoTextBlock = new TextBlock();
					infoTextBlock.Text = AppResources.UI_CharacterDetails_Professions_Secondary_None;
					infoTextBlock.Style = (Style)Resources[ "ProfessionTextStyle" ];
					spProfession.Children.Add( infoTextBlock );
				}
			}
			else
			{
				var infoTextBlock = new TextBlock();
				infoTextBlock.Text = AppResources.UI_CharacterDetails_Professions_None;
				infoTextBlock.Style = (Style)Resources[ "ProfessionTextStyle" ];
				spProfession.Children.Add( infoTextBlock );
			}
		}

		private void AddReputationFaction( ReputationFaction reputationFaction, int nestLevel )
		{
			if ( reputationFaction.IsHeader )
			{
				var nameTextBlock = new TextBlock();
				nameTextBlock.Text = reputationFaction.Name;
				nameTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
				nameTextBlock.VerticalAlignment = VerticalAlignment.Center;
				nameTextBlock.TextWrapping = TextWrapping.Wrap;
				if ( nestLevel == 0 )
				{
					nameTextBlock.Margin = new Thickness( 0, 24, 0, 0 );
					nameTextBlock.Style = (Style)Resources[ "ReputationHeaderTextStyle" ];
				}
				else
				{
					nameTextBlock.Margin = new Thickness( 0, 12, 0, 0 );
					nameTextBlock.Style = (Style)Resources[ "ReputationSubHeaderTextStyle" ];
				}

				if ( spReputation.Children.Count == 0 )
				{
					nameTextBlock.Margin = new Thickness( 0 );
				}

				spReputation.Children.Add( nameTextBlock );

				if ( reputationFaction.Factions != null && reputationFaction.Factions.Count > 0 )
				{
					foreach ( var faction in reputationFaction.Factions.Where( f => !f.IsHeader ) )
					{
						AddReputationFaction( faction, ( nestLevel + 1 ) );
					}

					foreach ( var faction in reputationFaction.Factions.Where( f => f.IsHeader ) )
					{
						AddReputationFaction( faction, ( nestLevel + 1 ) );
					}
				}
			}
			else
			{
				var grid = new Grid();
				grid.RowDefinitions.Add( new RowDefinition() );
				grid.RowDefinitions.Add( new RowDefinition() );
				grid.ColumnDefinitions.Add( new ColumnDefinition() );
				grid.ColumnDefinitions.Add( new ColumnDefinition { Width = GridLength.Auto } );
				grid.Margin = new Thickness( ( 24 * ( nestLevel - 1 ) ), 6, 0, 0 );

				string reputationText;
				int reputationAdjusted;
				int reputationCap;
				int reputationBarId;

				if ( reputationFaction.Reputation < -6000 )
				{
					reputationText = AppResources.UI_Reputation_0;
					reputationAdjusted = 42000 + reputationFaction.Reputation;
					reputationCap = 36000;
					reputationBarId = 0;
				}
				else if ( reputationFaction.Reputation < -3000 )
				{
					reputationText = AppResources.UI_Reputation_1;
					reputationAdjusted = 6000 + reputationFaction.Reputation;
					reputationCap = 3000;
					reputationBarId = 1;
				}
				else if ( reputationFaction.Reputation < 0 )
				{
					reputationText = AppResources.UI_Reputation_2;
					reputationAdjusted = 3000 + reputationFaction.Reputation;
					reputationCap = 3000;
					reputationBarId = 2;
				}
				else if ( reputationFaction.Reputation < 3000 )
				{
					reputationText = AppResources.UI_Reputation_3;
					reputationAdjusted = reputationFaction.Reputation;
					reputationCap = 3000;
					reputationBarId = 3;
				}
				else if ( reputationFaction.Reputation < 9000 )
				{
					reputationText = AppResources.UI_Reputation_4;
					reputationAdjusted = reputationFaction.Reputation - 3000;
					reputationCap = 6000;
					reputationBarId = 4;
				}
				else if ( reputationFaction.Reputation < 21000 )
				{
					reputationText = AppResources.UI_Reputation_5;
					reputationAdjusted = reputationFaction.Reputation - 9000;
					reputationCap = 12000;
					reputationBarId = 5;
				}
				else if ( reputationFaction.Reputation < 42000 )
				{
					reputationText = AppResources.UI_Reputation_6;
					reputationAdjusted = reputationFaction.Reputation - 21000;
					reputationCap = 21000;
					reputationBarId = 6;
				}
				else
				{
					reputationText = AppResources.UI_Reputation_7;
					reputationAdjusted = reputationFaction.Reputation - 42000;
					reputationCap = 1000;
					reputationBarId = 7;
				}

				var nameTextBlock = new TextBlock();
				Grid.SetRow( nameTextBlock, 0 );
				Grid.SetColumn( nameTextBlock, 0 );
				nameTextBlock.Text = reputationFaction.Name;
				nameTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
				nameTextBlock.VerticalAlignment = VerticalAlignment.Center;
				nameTextBlock.TextWrapping = TextWrapping.Wrap;
				nameTextBlock.Style = (Style)Resources[ "ReputationFactionNameTextStyle" ];
				nameTextBlock.Margin = new Thickness( 6, 0, 0, 0 );

				var reputationTextBlock = new TextBlock();
				Grid.SetRow( reputationTextBlock, 0 );
				Grid.SetColumn( reputationTextBlock, 1 );
				reputationTextBlock.Text = reputationText;
				reputationTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
				reputationTextBlock.VerticalAlignment = VerticalAlignment.Center;
				reputationTextBlock.TextWrapping = TextWrapping.Wrap;
				reputationTextBlock.Style = (Style)Resources[ "ReputationTextStyle" ];
				reputationTextBlock.Margin = new Thickness( 0, 0, 6, 0 );

				int rectangleWidth = Convert.ToInt32( Math.Round( 456.0 * ( Convert.ToDouble( reputationAdjusted ) / Convert.ToDouble( reputationCap ) ) ) );
				if ( rectangleWidth == 0 ) rectangleWidth = 1;
				var rectangle = new Rectangle();
				Grid.SetRow( rectangle, 0 );
				Grid.SetRowSpan( rectangle, 2 );
				Grid.SetColumn( rectangle, 0 );
				Grid.SetColumnSpan( rectangle, 2 );
				rectangle.HorizontalAlignment = HorizontalAlignment.Left;
				rectangle.Width = rectangleWidth;
				rectangle.Height = Double.NaN;
				rectangle.Fill = (LinearGradientBrush)Resources[ String.Format( "ReputationBar_{0}", reputationBarId ) ];

				var reputationValueTextBlock = new TextBlock();
				Grid.SetRow( reputationValueTextBlock, 1 );
				Grid.SetColumn( reputationValueTextBlock, 0 );
				Grid.SetColumnSpan( reputationValueTextBlock, 2 );
				reputationValueTextBlock.Text = String.Format( "{0} / {1}", reputationAdjusted, reputationCap );
				reputationValueTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
				reputationValueTextBlock.VerticalAlignment = VerticalAlignment.Center;
				reputationValueTextBlock.TextWrapping = TextWrapping.Wrap;
				reputationValueTextBlock.Style = (Style)Resources[ "ReputationTextStyle" ];

				grid.Children.Add( rectangle );
				grid.Children.Add( nameTextBlock );
				grid.Children.Add( reputationTextBlock );
				grid.Children.Add( reputationValueTextBlock );

				spReputation.Children.Add( grid );
			}
		}

		private void AddProfession( CharacterProfession profession )
		{
			var grid = new Grid();
			grid.RowDefinitions.Add( new RowDefinition() );
			grid.ColumnDefinitions.Add( new ColumnDefinition { Width = GridLength.Auto } );
			grid.ColumnDefinitions.Add( new ColumnDefinition() );
			grid.ColumnDefinitions.Add( new ColumnDefinition { Width = GridLength.Auto } );
			grid.Margin = new Thickness( 0, 0, 0, 4 );

			int rectangleWidth = Convert.ToInt32( Math.Round( 456.0 * ( Convert.ToDouble( profession.Value ) / Convert.ToDouble( profession.Max ) ) ) );
			if ( rectangleWidth == 0 ) rectangleWidth = 1;
			var rectangle = new Rectangle();
			Grid.SetRow( rectangle, 0 );
			Grid.SetColumn( rectangle, 0 );
			Grid.SetColumnSpan( rectangle, 3 );
			rectangle.HorizontalAlignment = HorizontalAlignment.Left;
			rectangle.Width = rectangleWidth;
			rectangle.Height = Double.NaN;
			rectangle.Fill = (LinearGradientBrush)Resources[ "ProfessionBar" ];

			var image = new Image();
			Grid.SetRow( image, 0 );
			Grid.SetColumn( image, 0 );
			image.Source = profession.IconImage;
			image.Width = 32;
			image.Height = 32;
			image.HorizontalAlignment = HorizontalAlignment.Left;
			image.VerticalAlignment = VerticalAlignment.Center;
			image.Margin = new Thickness( 4 );

			var nameTextBlock = new TextBlock();
			Grid.SetRow( nameTextBlock, 0 );
			Grid.SetColumn( nameTextBlock, 1 );
			nameTextBlock.Text = profession.Name;
			nameTextBlock.Style = (Style)Resources[ "ProfessionTextStyle" ];
			nameTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
			nameTextBlock.VerticalAlignment = VerticalAlignment.Center;
			nameTextBlock.Margin = new Thickness( 6, 0, 0, 0 );

			var valueTextBlock = new TextBlock();
			Grid.SetRow( valueTextBlock, 0 );
			Grid.SetColumn( valueTextBlock, 2 );
			valueTextBlock.Text = String.Format( "{0} / {1}", profession.Value, profession.Max );
			valueTextBlock.Style = (Style)Resources[ "ProfessionTextStyle" ];
			valueTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
			valueTextBlock.VerticalAlignment = VerticalAlignment.Center;
			valueTextBlock.Margin = new Thickness( 0, 0, 6, 0 );

			grid.Children.Add( rectangle );
			grid.Children.Add( image );
			grid.Children.Add( nameTextBlock );
			grid.Children.Add( valueTextBlock );

			spProfession.Children.Add( grid );
		}

		protected override void OnBackKeyPress( System.ComponentModel.CancelEventArgs e )
		{
			if ( IsItemTooltipShown() )
			{
				HideItemTooltip();
				ClearToolTip();
				e.Cancel = true;
			}
		}

		private void CharacterPivot_SelectionChanged( object sender, SelectionChangedEventArgs e )
		{
			foreach ( var addedItem in e.AddedItems )
			{
				if ( addedItem is PivotItem )
				{
					var pivotItem = (PivotItem)addedItem;
					if ( pivotItem.Tag != null && !String.IsNullOrEmpty( pivotItem.Tag.ToString() ) && pivotItem.Tag.ToString().Equals( "ActivityFeed", StringComparison.CurrentCultureIgnoreCase ) )
					{
						ApplicationBar = new ApplicationBar();
						var refreshItem = new ApplicationBarIconButton();
						refreshItem.IconUri = new Uri( "/Images/ApplicationBar/CharacterList/refresh.png", UriKind.Relative );
						refreshItem.Text = AppResources.UI_Favorites_ContextMenuItem_Refresh;
						refreshItem.IsEnabled = true;
						refreshItem.Click += RefreshCharacterActivityFeed;
						ApplicationBar.Buttons.Add( refreshItem );
					}
				}
			}

			foreach ( var removedItem in e.RemovedItems )
			{
				if ( removedItem is PivotItem )
				{
					var pivotItem = (PivotItem)removedItem;
					if ( pivotItem.Tag != null && !String.IsNullOrEmpty( pivotItem.Tag.ToString() ) && pivotItem.Tag.ToString().Equals( "ActivityFeed", StringComparison.CurrentCultureIgnoreCase ) )
					{
						ApplicationBar = null;
					}
				}
			}
		}

		private void RefreshCharacterActivityFeed( object sender, EventArgs e )
		{
			CharacterPivot.IsEnabled = true;
			spRefreshLoadingIndicator.Visibility = Visibility.Visible;
			pbRefreshLoading.IsIndeterminate = true;

			_prevRegion = Armory.Current.Region;
			Armory.Current.Region = ViewModel.SelectedCharacter.CharacterSheetPage.Region;
			Armory.Current.GetCharacterActivityFeedPageAsync( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Name,
				ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Realm,
				GotCharacterActivityFeed );
		}

		private void GotCharacterActivityFeed( CharacterActivityFeedPage characterActivityFeedPage )
		{
			CharacterPivot.IsEnabled = true;
			spRefreshLoadingIndicator.Visibility = Visibility.Collapsed;
			pbRefreshLoading.IsIndeterminate = false;

			if ( characterActivityFeedPage == null )
			{
				MessageBox.Show( AppResources.UI_Search_CouldNotRetrieveInformation, AppResources.UI_Search_CouldNotRetrieveInformation_Caption, MessageBoxButton.OK );
				return;
			}

			ViewModel.SelectedCharacter.CharacterActivityFeedPage = characterActivityFeedPage;

			if ( ViewModel.SelectedCharacter.IsBookmarked )
			{
				StorageManager.DeleteCharacter( ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Realm, ViewModel.SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Name );
				StorageManager.StoreCharacter( ViewModel.SelectedCharacter );
			}

			BuildActivityFeed();
		}

		private void BuildActivityFeed()
		{
			ViewModel.ActivityFeedEvents = ViewModel.SelectedCharacter.CharacterActivityFeedPage.Events;
		}

		private void ActivityFeedEvents_SelectionChanged( object sender, SelectionChangedEventArgs e )
		{
			ActivityFeedEvents.SelectedIndex = -1;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}