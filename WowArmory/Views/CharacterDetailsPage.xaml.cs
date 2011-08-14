using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WowArmory.Controls;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Helper;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class CharacterDetailsPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private bool _isToolTipLoading = false;
		private int _toolTipCancel = 0;
		private Dictionary<CharacterItemContainer, Item> _cachedItems = new Dictionary<CharacterItemContainer,Item>();
		private CharacterItemContainer _itemContainerForToolTip;
		private Item _itemForToolTip;
		private Dictionary<int, Item> _cachedGems = new Dictionary<int, Item>();
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		public CharacterDetailsViewModel ViewModel
		{
			get { return (CharacterDetailsViewModel)DataContext; }
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public CharacterDetailsPage()
		{
			InitializeComponent();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		
		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Handles the Loaded event of the PhoneApplicationPage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
			LoadView();
			BuildTalents();
			BuildReputation();
			BuildProfessions();
		}

		/// <summary>
		/// Handles the SelectionChanged event of the CharacterPivot control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void CharacterPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		/// <summary>
		/// Loads the view and initializes the content.
		/// </summary>
		private void LoadView()
		{
			if (AppSettingsManager.ShowCharacterBackground && ViewModel.Character != null)
			{
				var backgroundName = ((CharacterRace)ViewModel.Character.Race).ToString();
				if (((CharacterClass)ViewModel.Character.Class) == CharacterClass.DeathKnight)
				{
					backgroundName = "DeathKnight";
				}

				backgroundName = String.Format("{0}{1}.png", ((SolidColorBrush)Resources["PhoneBackgroundBrush"]).Color == Colors.White ? "Light" : "Dark", backgroundName);
				LayoutRoot.Background = new ImageBrush { ImageSource = CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/CharacterDetails/Backgrounds/{0}", backgroundName)) };
			}
			else
			{
				LayoutRoot.Background = new SolidColorBrush(Colors.Transparent);
			}

			var powerType = ViewModel.Character.Stats.PowerType;
			//barPowerType.Background = (Brush)Resources[String.Format("{0}BarStyle", (powerType.Substring(0, 1).ToUpper() + powerType.Substring(1)).Replace("-", ""))];
			tbPowerType.Foreground = (Brush)Resources[String.Format("{0}TextBrush", (powerType.Substring(0, 1).ToUpper() + powerType.Substring(1)).Replace("-", ""))];

			HideToolTip();
		}

		/// <summary>
		/// Builds the user interface for the talents grid.
		/// </summary>
		private void BuildTalents()
		{
			if (ViewModel.Character != null &&
				ViewModel.Character.Talents != null &&
				ViewModel.Character.Talents.Count > 0)
			{
				if (ViewModel.Character.Talents[0] != null)
				{
					var talentOne = BuildTalentInformation(ViewModel.Character.Talents[0]);
					Grid.SetColumn(talentOne, 0);
					talentOne.HorizontalAlignment = HorizontalAlignment.Left;
					talentOne.VerticalAlignment = VerticalAlignment.Top;
					gdCharacterTalents.Children.Add(talentOne);
				}
				if (ViewModel.Character.Talents[1] != null)
				{
					var talentTwo = BuildTalentInformation(ViewModel.Character.Talents[1]);
					Grid.SetColumn(talentTwo, 1);
					talentTwo.HorizontalAlignment = HorizontalAlignment.Left;
					talentTwo.VerticalAlignment = VerticalAlignment.Top;
					gdCharacterTalents.Children.Add(talentTwo);
				}

				gdCharacterTalents.Visibility = Visibility.Visible;
			}
		}

		/// <summary>
		/// Builds the talent information.
		/// </summary>
		/// <param name="talent">The talent.</param>
		/// <returns></returns>
		private Grid BuildTalentInformation(CharacterTalents talent)
		{
			var activeText = (talent.Selected != null && (bool)talent.Selected) ? "Active" : "Inactive";

			var grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			grid.ColumnDefinitions.Add(new ColumnDefinition());

			var talentImageGrid = new Grid();
			talentImageGrid.Margin = new Thickness(0,0,4,0);
			talentImageGrid.VerticalAlignment = VerticalAlignment.Top;
			Grid.SetRow(talentImageGrid, 0);
			Grid.SetRowSpan(talentImageGrid, 2);
			Grid.SetColumn(talentImageGrid, 0);
			grid.Children.Add(talentImageGrid);

			var talentImageBackground = new Image();
			talentImageBackground.Source = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/Icons/Icon_Background.png");
			talentImageBackground.Width = 44;
			talentImageBackground.Height = 44;
			talentImageBackground.HorizontalAlignment = HorizontalAlignment.Center;
			talentImageBackground.VerticalAlignment = VerticalAlignment.Center;
			talentImageGrid.Children.Add(talentImageBackground);

			var talentImage = new Image();
			talentImage.Source = BattleNetClient.Current.GetIcon(talent.Icon);
			talentImage.Width = 42;
			talentImage.Height = 42;
			talentImage.OpacityMask = new ImageBrush { ImageSource = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/Icons/Icon_Mask.png") };
			talentImage.HorizontalAlignment = HorizontalAlignment.Center;
			talentImage.VerticalAlignment = VerticalAlignment.Center;
			talentImageGrid.Children.Add(talentImage);

			var talentImageBorder = new Image();
			talentImageBorder.Source = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/Icons/Icon_Border.png");
			talentImageBorder.Width = 44;
			talentImageBorder.Height = 44;
			talentImageBorder.HorizontalAlignment = HorizontalAlignment.Center;
			talentImageBorder.VerticalAlignment = VerticalAlignment.Center;
			talentImageGrid.Children.Add(talentImageBorder);

			var buildName = new TextBlock();
			buildName.Text = talent.Name;
			buildName.Style = (Style)Resources[String.Format("CharacterDetailsTalents{0}TextStyle", activeText)];
			Grid.SetRow(buildName, 0);
			Grid.SetColumn(buildName, 1);
			grid.Children.Add(buildName);

			var stackPanel = new StackPanel();
			stackPanel.Orientation = System.Windows.Controls.Orientation.Horizontal;
			stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetRow(stackPanel, 1);
			Grid.SetColumn(stackPanel, 1);
			grid.Children.Add(stackPanel);

			var pointsOne = new TextBlock();
			pointsOne.Text = talent.Trees[0].Total.ToString();
			var maxText = "";
			if (talent.Trees[0].Total > talent.Trees[1].Total && talent.Trees[0].Total > talent.Trees[2].Total)
			{
				maxText = "Max";
			}
			pointsOne.Style = (Style)Resources[String.Format("CharacterDetailsTalents{0}Points{1}TextStyle", activeText, maxText)];
			stackPanel.Children.Add(pointsOne);

			var separatorOne = new TextBlock();
			separatorOne.Text = "/";
			separatorOne.Margin = new Thickness(6, 0, 6, 0);
			separatorOne.Style = (Style)Resources[String.Format("CharacterDetailsTalents{0}PointsTextStyle", activeText)];
			stackPanel.Children.Add(separatorOne);

			var pointsTwo = new TextBlock();
			pointsTwo.Text = talent.Trees[1].Total.ToString();
			maxText = "";
			if (talent.Trees[1].Total > talent.Trees[0].Total && talent.Trees[1].Total > talent.Trees[2].Total)
			{
				maxText = "Max";
			}
			pointsTwo.Style = (Style)Resources[String.Format("CharacterDetailsTalents{0}Points{1}TextStyle", activeText, maxText)];
			stackPanel.Children.Add(pointsTwo);

			var separatorTwo = new TextBlock();
			separatorTwo.Text = "/";
			separatorTwo.Margin = new Thickness(6, 0, 6, 0);
			separatorTwo.Style = (Style)Resources[String.Format("CharacterDetailsTalents{0}PointsTextStyle", activeText)];
			stackPanel.Children.Add(separatorTwo);

			var pointsThree = new TextBlock();
			pointsThree.Text = talent.Trees[2].Total.ToString();
			maxText = "";
			if (talent.Trees[2].Total > talent.Trees[0].Total && talent.Trees[2].Total > talent.Trees[1].Total)
			{
				maxText = "Max";
			}
			pointsThree.Style = (Style)Resources[String.Format("CharacterDetailsTalents{0}Points{1}TextStyle", activeText, maxText)];
			stackPanel.Children.Add(pointsThree);

			
			//        <TextBlock x:Name="tbCharacterTalentsPrimaryTreeOnePoints" />
			//        <TextBlock x:Name="tbCharacterTalentsPrimaryTreeOneSeparator" Text=", " />
			//        <TextBlock x:Name="tbCharacterTalentsPrimaryTreeTwoPoints" />
			//        <TextBlock x:Name="tbCharacterTalentsPrimaryTreeTwoSeparator" Text=", " />
			//        <TextBlock x:Name="tbCharacterTalentsPrimaryTreeThreePoints" />

			return grid;
		}

		/// <summary>
		/// Builds the user interface for the reputation pivot.
		/// </summary>
		private void BuildReputation()
		{
			spReputation.Children.Clear();

			if (ViewModel.Character != null &&
				ViewModel.Character.Reputation != null &&
				ViewModel.Character.Reputation.Count > 0)
			{
				foreach (var reputation in ViewModel.Character.Reputation.OrderBy(r => r.Name))
				{
					AddReputation(reputation);
				}
			}
		}

		/// <summary>
		/// Adds the specified reputation to the reputation panel.
		/// </summary>
		/// <param name="reputation">The reputation.</param>
		private void AddReputation(Reputation reputation)
		{
			var grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition());
			grid.RowDefinitions.Add(new RowDefinition());
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			grid.Margin = new Thickness(0, 6, 0, 0);
			grid.Height = 54;

			var standing = (ReputationStanding)reputation.Standing;
			var reputationText = AppResources.ResourceManager.GetString(String.Format("BattleNet_Reputation_{0}", standing));

			var nameTextBlock = new TextBlock();
			Grid.SetRow(nameTextBlock, 0);
			Grid.SetColumn(nameTextBlock, 0);
			nameTextBlock.Text = reputation.Name;
			nameTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
			nameTextBlock.VerticalAlignment = VerticalAlignment.Center;
			nameTextBlock.TextWrapping = TextWrapping.Wrap;
			nameTextBlock.Style = (Style)Resources["ReputationFactionNameTextStyle"];
			nameTextBlock.Margin = new Thickness(6, 0, 0, 0);

			var reputationTextBlock = new TextBlock();
			Grid.SetRow(reputationTextBlock, 0);
			Grid.SetColumn(reputationTextBlock, 1);
			reputationTextBlock.Text = reputationText;
			reputationTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
			reputationTextBlock.VerticalAlignment = VerticalAlignment.Center;
			reputationTextBlock.TextWrapping = TextWrapping.Wrap;
			reputationTextBlock.Style = (Style)Resources["ReputationTextStyle"];
			reputationTextBlock.Margin = new Thickness(0, 0, 6, 0);

			int rectangleWidth = Convert.ToInt32(Math.Round(456.0 * (Convert.ToDouble(reputation.Value) / Convert.ToDouble(reputation.Max))));
			if (rectangleWidth == 0) rectangleWidth = 1;
			var rectangle = new Rectangle();
			Grid.SetRow(rectangle, 0);
			Grid.SetRowSpan(rectangle, 2);
			Grid.SetColumn(rectangle, 0);
			Grid.SetColumnSpan(rectangle, 2);
			rectangle.HorizontalAlignment = HorizontalAlignment.Left;
			rectangle.Width = rectangleWidth;
			rectangle.Height = Double.NaN;
			rectangle.Fill = (Brush)Resources[String.Format("ReputationBar_{0}", reputation.Standing)];

			var reputationValueTextBlock = new TextBlock();
			Grid.SetRow(reputationValueTextBlock, 1);
			Grid.SetColumn(reputationValueTextBlock, 0);
			Grid.SetColumnSpan(reputationValueTextBlock, 2);
			reputationValueTextBlock.Text = String.Format("{0} / {1}", reputation.Value, reputation.Max);
			reputationValueTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
			reputationValueTextBlock.VerticalAlignment = VerticalAlignment.Center;
			reputationValueTextBlock.TextWrapping = TextWrapping.Wrap;
			reputationValueTextBlock.Style = (Style)Resources["ReputationTextStyle"];

			grid.Children.Add(rectangle);
			grid.Children.Add(nameTextBlock);
			grid.Children.Add(reputationTextBlock);
			grid.Children.Add(reputationValueTextBlock);

			spReputation.Children.Add(grid);
		}

		/// <summary>
		/// Builds the user interface for the professions pivot.
		/// </summary>
		private void BuildProfessions()
		{
			TextBlock headerTextBlock;
			TextBlock infoTextBlock;

			spProfessions.Children.Clear();

			if (ViewModel.Character != null &&
				ViewModel.Character.Professions != null &&
				((ViewModel.Character.Professions.Primary != null && ViewModel.Character.Professions.Primary.Count > 0) ||
				(ViewModel.Character.Professions.Secondary != null && ViewModel.Character.Professions.Secondary.Count > 0)))
			{
				headerTextBlock = new TextBlock();
				headerTextBlock.Text = AppResources.UI_CharacterDetails_Professions_Primary;
				headerTextBlock.Style = (Style)Resources["ProfessionHeaderTextStyle"];
				spProfessions.Children.Add(headerTextBlock);

				if (ViewModel.Character.Professions.Primary.Count > 0)
				{
					foreach (var profession in ViewModel.Character.Professions.Primary)
					{
						AddProfession(profession);
					}
				}
				else
				{
					infoTextBlock = new TextBlock();
					infoTextBlock.Text = AppResources.UI_CharacterDetails_Professions_Primary_None;
					infoTextBlock.Style = (Style)Resources["ProfessionTextStyle"];
					spProfessions.Children.Add(infoTextBlock);
				}

				headerTextBlock = new TextBlock();
				headerTextBlock.Text = AppResources.UI_CharacterDetails_Professions_Secondary;
				headerTextBlock.Style = (Style)Resources["ProfessionHeaderTextStyle"];
				headerTextBlock.Margin = new Thickness(0, 12, 0, 0);
				spProfessions.Children.Add(headerTextBlock);

				if (ViewModel.Character.Professions.Secondary.Count > 0)
				{
					foreach (var profession in ViewModel.Character.Professions.Secondary)
					{
						AddProfession(profession);
					}
				}
				else
				{
					infoTextBlock = new TextBlock();
					infoTextBlock.Text = AppResources.UI_CharacterDetails_Professions_Secondary_None;
					infoTextBlock.Style = (Style)Resources["ProfessionTextStyle"];
					spProfessions.Children.Add(infoTextBlock);
				}
			}
			else
			{
				infoTextBlock = new TextBlock();
				infoTextBlock.Text = AppResources.UI_CharacterDetails_Professions_None;
				infoTextBlock.Style = (Style)Resources["ProfessionTextStyle"];
				spProfessions.Children.Add(infoTextBlock);
			}
		}

		/// <summary>
		/// Adds the specified profession to the profession panel.
		/// </summary>
		/// <param name="profession">The profession.</param>
		private void AddProfession(Profession profession)
		{
			var grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition());
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			grid.Margin = new Thickness(0, 0, 0, 4);
			
			// NOTE the Battle.Net community api currently returns 0 as the maximum value - in this case use a default of 525
			var max = profession.Max > 0 ? Convert.ToDouble(profession.Max) : 525.0;
			int rectangleWidth = Convert.ToInt32(Math.Round(456.0 * (Convert.ToDouble(profession.Rank) / max)));
			if (rectangleWidth == 0) rectangleWidth = 1;
			var rectangle = new Rectangle();
			Grid.SetRow(rectangle, 0);
			Grid.SetColumn(rectangle, 0);
			Grid.SetColumnSpan(rectangle, 3);
			rectangle.HorizontalAlignment = HorizontalAlignment.Left;
			rectangle.Width = rectangleWidth;
			rectangle.Height = Double.NaN;
			rectangle.Fill = (Brush)Resources["PhoneAccentBrush"];
			var rectangleHighlight = new Rectangle();
			Grid.SetRow(rectangleHighlight, 0);
			Grid.SetColumn(rectangleHighlight, 0);
			Grid.SetColumnSpan(rectangleHighlight, 3);
			rectangleHighlight.HorizontalAlignment = HorizontalAlignment.Left;
			rectangleHighlight.Width = rectangleWidth;
			rectangleHighlight.Height = Double.NaN;
			rectangleHighlight.Fill = (Brush)Resources["ProfessionBar"];

			var backgroundImage = new Image();
			Grid.SetRow(backgroundImage, 0);
			Grid.SetColumn(backgroundImage, 0);
			backgroundImage.Source = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/CharacterDetails/Profession_Mask.png");
			backgroundImage.Width = 48;
			backgroundImage.Height = 48;
			backgroundImage.HorizontalAlignment = HorizontalAlignment.Center;
			backgroundImage.VerticalAlignment = VerticalAlignment.Center;

			var iconImage = new Image();
			Grid.SetRow(iconImage, 0);
			Grid.SetColumn(iconImage, 0);
			iconImage.Source = BattleNetClient.Current.GetIcon(profession.Icon);
			iconImage.Width = 44;
			iconImage.Height = 44;
			iconImage.HorizontalAlignment = HorizontalAlignment.Center;
			iconImage.VerticalAlignment = VerticalAlignment.Center;
			iconImage.OpacityMask = new ImageBrush { ImageSource = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/CharacterDetails/Profession_Mask.png") };

			var borderImage = new Image();
			Grid.SetRow(borderImage, 0);
			Grid.SetColumn(borderImage, 0);
			borderImage.Source = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/CharacterDetails/Profession_Border.png");
			borderImage.Width = 48;
			borderImage.Height = 48;
			borderImage.HorizontalAlignment = HorizontalAlignment.Center;
			borderImage.VerticalAlignment = VerticalAlignment.Center;
			borderImage.Margin = new Thickness(4);

			var nameTextBlock = new TextBlock();
			Grid.SetRow(nameTextBlock, 0);
			Grid.SetColumn(nameTextBlock, 1);
			nameTextBlock.Text = profession.Name;
			nameTextBlock.Style = (Style)Resources["ProfessionTextStyle"];
			nameTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
			nameTextBlock.VerticalAlignment = VerticalAlignment.Center;
			nameTextBlock.Margin = new Thickness(6, 0, 0, 0);

			var valueTextBlock = new TextBlock();
			Grid.SetRow(valueTextBlock, 0);
			Grid.SetColumn(valueTextBlock, 2);
			valueTextBlock.Text = String.Format("{0} / {1}", profession.Rank, profession.Max);
			valueTextBlock.Style = (Style)Resources["ProfessionTextStyle"];
			valueTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
			valueTextBlock.VerticalAlignment = VerticalAlignment.Center;
			valueTextBlock.Margin = new Thickness(0, 0, 6, 0);

			grid.Children.Add(rectangle);
			grid.Children.Add(rectangleHighlight);
			grid.Children.Add(backgroundImage);
			grid.Children.Add(iconImage);
			grid.Children.Add(borderImage);
			grid.Children.Add(nameTextBlock);
			grid.Children.Add(valueTextBlock);

			spProfessions.Children.Add(grid);
		}

		/// <summary>
		/// Handles the MouseLeftButtonUp event of the CharacterItemContainer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
		private void CharacterItemContainer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (sender is CharacterItemContainer)
			{
				var itemContainer = (CharacterItemContainer)sender;

				if (itemContainer.DataContext == null)
				{
					return;
				}

				var isSelected = false;

				if (itemContainer.SelectionVisibility == Visibility.Visible)
				{
					isSelected = true;
				}

				HideAllItemContainerSelections();
				HideToolTip();

				if (!isSelected)
				{
					ShowToolTip(itemContainer);
				}
			}
		}

		/// <summary>
		/// Hides all item container selection boxes.
		/// </summary>
		private void HideAllItemContainerSelections()
		{
			icHead.SelectionVisibility = Visibility.Collapsed;
			icNeck.SelectionVisibility = Visibility.Collapsed;
			icShoulder.SelectionVisibility = Visibility.Collapsed;
			icBack.SelectionVisibility = Visibility.Collapsed;
			icChest.SelectionVisibility = Visibility.Collapsed;
			icShirt.SelectionVisibility = Visibility.Collapsed;
			icTabard.SelectionVisibility = Visibility.Collapsed;
			icWrist.SelectionVisibility = Visibility.Collapsed;
			icHands.SelectionVisibility = Visibility.Collapsed;
			icWaist.SelectionVisibility = Visibility.Collapsed;
			icLegs.SelectionVisibility = Visibility.Collapsed;
			icFeet.SelectionVisibility = Visibility.Collapsed;
			icFinger1.SelectionVisibility = Visibility.Collapsed;
			icFinger2.SelectionVisibility = Visibility.Collapsed;
			icTrinket1.SelectionVisibility = Visibility.Collapsed;
			icTrinket2.SelectionVisibility = Visibility.Collapsed;
			icMainHand.SelectionVisibility = Visibility.Collapsed;
			icOffHand.SelectionVisibility = Visibility.Collapsed;
			icRanged.SelectionVisibility = Visibility.Collapsed;

			HideToolTip();
		}

		/// <summary>
		/// Hides the tool tip.
		/// </summary>
		private void HideToolTip()
		{
			brdItemToolTip.Visibility = Visibility.Collapsed;
			ClearToolTip();
			svCharacterStats.IsEnabled = true;
			svCharacterStats.Opacity = 1;

			if (_isToolTipLoading)
			{
				_isToolTipLoading = false;
				_toolTipCancel++;
			}
		}

		/// <summary>
		/// Shows the tool tip.
		/// </summary>
		/// <param name="itemContainer">The item container.</param>
		private void ShowToolTip(CharacterItemContainer itemContainer)
		{
			_isToolTipLoading = true;
			_itemContainerForToolTip = itemContainer;
			itemContainer.SelectionVisibility = Visibility.Visible;
			svCharacterStats.IsEnabled = false;
			svCharacterStats.Opacity = 0.25;
			pbItemToolTip.IsIndeterminate = true;
			pbItemToolTip.Visibility = Visibility.Visible;
			ShowToolTipText(tbItemToolTipLoading, AppResources.UI_Common_LoadingData);
			brdItemToolTip.Visibility = Visibility.Visible;

			if (_cachedItems.ContainsKey(itemContainer))
			{
				OnItemReceived(_cachedItems[itemContainer]);
			}
			else
			{
				BattleNetClient.Current.GetItemAsync(((CharacterItem)itemContainer.DataContext).Id, OnItemReceived);
			}
		}

		/// <summary>
		/// Called when the item was received.
		/// </summary>
		/// <param name="item">The item.</param>
		private void OnItemReceived(Item item)
		{
			_itemForToolTip = item;

			if (item == null)
			{
				ClearToolTip();
				ShowToolTipText(tbItemToolTipLoading, AppResources.UI_Common_Error_NoData_Text);
				return;
			}

			if (!_cachedItems.ContainsKey(_itemContainerForToolTip))
			{
				_cachedItems.Add(_itemContainerForToolTip, _itemForToolTip);
			}

			BuildItemToolTip();
		}

		/// <summary>
		/// Builds the item tool tip.
		/// </summary>
		private void BuildItemToolTip()
		{
			ClearToolTip();

			var normalStyle = (Style)Resources["CharacterDetailsItemToolTipNormalTextStyle"];
			var bonusStatStyle = (Style)Resources["CharacterDetailsItemToolTipBonusStatTextStyle"];
			var subtleStyle = (Style)Resources["CharacterDetailsItemToolTipSubtleTextStyle"];
			var socketNameStyle = (Style)Resources["CharacterDetailsItemToolTipSocketGemNameTextStyle"];
			var descriptionStyle = (Style)Resources["CharacterDetailsItemToolTipDescriptionTextStyle"];

			// item name
			ShowToolTipText(tbItemToolTipName, _itemForToolTip.Name, (Brush)Resources[String.Format("ItemQuality{0}", _itemForToolTip.Quality)]);
			// binding
			ShowToolTipText(tbItemToolTipBinding, AppResources.ResourceManager.GetString(String.Format("Item_Binding_{0}", _itemForToolTip.ItemBind)));
			// unique equippable
			if (_itemForToolTip.MaxCount == 1)
			{
				ShowToolTipText(tbItemToolTipMaxCount, AppResources.Item_MaxCount_UniqueEquipped);
			}
			// inventory type
			ShowToolTipText(tbItemToolTipInventoryType, AppResources.ResourceManager.GetString(String.Format("Item_InventoryType_{0}", (InventoryType)_itemForToolTip.InventoryType)));
			// sub class
			ShowToolTipText(tbItemToolTipSubClass, AppResources.ResourceManager.GetString(String.Format("Item_ItemSubClass_{0}_{1}", _itemForToolTip.ItemClass, _itemForToolTip.ItemSubClass)));
			// weapon information
			if (_itemForToolTip.WeaponInfo != null)
			{
				// damage
				if (_itemForToolTip.WeaponInfo.Damage != null && _itemForToolTip.WeaponInfo.Damage.Count > 0)
				{
					if (_itemForToolTip.WeaponInfo.Damage[0].MinDamage > 0 && _itemForToolTip.WeaponInfo.Damage[0].MaxDamage > 0)
					{
						ShowToolTipText(tbItemToolTipWeaponInfoDamage, String.Format(AppResources.Item_WeaponInfo_Damage, _itemForToolTip.WeaponInfo.Damage[0].MinDamage, _itemForToolTip.WeaponInfo.Damage[0].MaxDamage));
					}
				}
				// speed
				ShowToolTipText(tbItemToolTipWeaponInfoSpeed, String.Format(AppResources.Item_WeaponInfo_Speed, _itemForToolTip.WeaponInfo.WeaponSpeed));
				// dps
				ShowToolTipText(tbItemToolTipWeaponInfoDps, String.Format(AppResources.Item_WeaponInfo_Dps, _itemForToolTip.WeaponInfo.Dps));
			}
			// sockets
			if (_itemForToolTip.SocketInfo != null)
			{
				var index = 0;
				foreach (var socket in _itemForToolTip.SocketInfo.Sockets)
				{
					var socketTypeText = AppResources.ResourceManager.GetString(String.Format("Item_Socket_{0}", socket.Type));
					var socketBackgroundColorResource = String.Format("ItemSocketBackgroundBrush{0}", socket.Type);
					var socketBorderImageUri = String.Format("/WowArmory.Core;Component/Images/Item/Socket_{0}.png", socket.Type);

					var socketGrid = new Grid();
					socketGrid.Name = String.Format("gdItemToolTipSocket{0}", index);
					socketGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
					socketGrid.ColumnDefinitions.Add(new ColumnDefinition());

					var socketBackgroundColor = new Border();
					socketBackgroundColor.Name = String.Format("brdItemToolTipSocket{0}", index);
					socketBackgroundColor.Background = (Brush)Resources[socketBackgroundColorResource];
					socketBackgroundColor.Margin = new Thickness(1, 1, 1, 1);
					socketBackgroundColor.Width = 18;
					socketBackgroundColor.Height = 18;
					socketBackgroundColor.VerticalAlignment = VerticalAlignment.Top;
					Grid.SetColumn(socketBackgroundColor, 0);
					socketGrid.Children.Add(socketBackgroundColor);

					var socketImage = new Image();
					socketImage.Name = String.Format("imgItemToolTipSocketImage{0}", index);
					socketImage.Margin = new Thickness(1, 1, 1, 1);
					socketImage.Width = 18;
					socketImage.Height = 18;
					socketImage.VerticalAlignment = VerticalAlignment.Top;
					Grid.SetColumn(socketImage, 0);
					socketGrid.Children.Add(socketImage);

					var socketBorder = new Image();
					socketBorder.Name = String.Format("imgItemToolTipSocket{0}", index);
					socketBorder.Source = CacheManager.GetImageSourceFromCache(socketBorderImageUri);
					socketBorder.Margin = new Thickness(0, 0, 0, 0);
					socketBorder.Width = 20;
					socketBorder.Height = 20;
					socketBorder.VerticalAlignment = VerticalAlignment.Top;
					Grid.SetColumn(socketBorder, 0);
					socketGrid.Children.Add(socketBorder);

					var socketTextContainer = new StackPanel();
					socketTextContainer.Orientation = System.Windows.Controls.Orientation.Vertical;
					socketTextContainer.VerticalAlignment = VerticalAlignment.Center;
					Grid.SetColumn(socketTextContainer, 1);

					var socketText = new TextBlock();
					socketText.Name = String.Format("tbItemToolTipSocket{0}", index);
					socketText.Text = socketTypeText;
					socketText.Style = subtleStyle;
					socketText.Margin = new Thickness(6, 0, 0, 0);
					socketTextContainer.Children.Add(socketText);

					var socketNameText = new TextBlock();
					socketNameText.Name = String.Format("tbItemToolTipSocketName{0}", index);
					socketNameText.Text = String.Empty;
					socketNameText.Style = socketNameStyle;
					socketNameText.Margin = new Thickness(6, 0, 0, 0);
					socketNameText.Visibility = Visibility.Collapsed;
					socketTextContainer.Children.Add(socketNameText);
					socketGrid.Children.Add(socketTextContainer);

					spItemToolTipSockets.Children.Add(socketGrid);

					var dispatcherIndex = index;
					Dispatcher.BeginInvoke(() => FetchSocketDetails(_itemContainerForToolTip, dispatcherIndex));

					index++;
				}

				spItemToolTipSockets.Visibility = Visibility.Visible;
			}
			// durability
			if (_itemForToolTip.MaxDurability > 0)
			{
				ShowToolTipText(tbItemToolTipDurability, String.Format(AppResources.Item_Durability, _itemForToolTip.MaxDurability));
			}
			// allowable classes
			if (_itemForToolTip.AllowableClasses != null)
			{
				var labelText = new TextBlock();
				labelText.Text = AppResources.Item_AllowableClasses;
				labelText.Style = normalStyle;
				spItemToolTipAllowableClasses.Children.Add(labelText);

				foreach (var allowableClass in _itemForToolTip.AllowableClasses)
				{
					if (spItemToolTipAllowableClasses.Children.Count > 1)
					{
						var separatorText = new TextBlock();
						separatorText.Text = ",";
						separatorText.Style = normalStyle;
						spItemToolTipAllowableClasses.Children.Add(separatorText);
					}

					var classText = new TextBlock();
					classText.Text = AppResources.ResourceManager.GetString(String.Format("BattleNet_Classes_{0}", allowableClass));
					classText.Style = normalStyle;
					classText.Margin = new Thickness(6, 0, 0, 0);
					classText.Foreground = (Brush)Resources[String.Format("ItemClass{0}", allowableClass)];
					spItemToolTipAllowableClasses.Children.Add(classText);
				}

				spItemToolTipAllowableClasses.Visibility = Visibility.Visible;
			}
			// required level
			if (_itemForToolTip.RequiredLevel > 0)
			{
				ShowToolTipText(tbItemToolTipRequiredLevel, String.Format(AppResources.Item_RequiredLevel, _itemForToolTip.RequiredLevel));
			}
			// required faction
			if (_itemForToolTip.MinFactionId > 0)
			{
				var reputationFaction = ViewModel.Character.Reputation.Where(r => r.Id == _itemForToolTip.MinFactionId).FirstOrDefault();
				if (reputationFaction != null)
				{
					var reputation = AppResources.ResourceManager.GetString(String.Format("BattleNet_Reputation_{0}", (ReputationStanding)_itemForToolTip.MinReputation));
					ShowToolTipText(tbItemToolTipRequiredFaction, String.Format(AppResources.Item_RequiredFaction, reputationFaction.Name, reputation));
				}
			}
			// item level
			if (_itemForToolTip.ItemLevel > 0)
			{
				ShowToolTipText(tbItemToolTipItemLevel, String.Format(AppResources.Item_ItemLevel, _itemForToolTip.ItemLevel));
			}
			// armor
			if (_itemForToolTip.BaseArmor > 0)
			{
				ShowToolTipText(tbItemToolTipArmor, String.Format("{0} {1}", _itemForToolTip.BaseArmor, AppResources.UI_CharacterDetails_Character_Description_Armor));
			}
			// stats
			if (_itemForToolTip.BonusStats != null && _itemForToolTip.BonusStats.Count > 0)
			{
				var spirit = _itemForToolTip.BonusStats.Where(s => s.Stat == ItemBonusStatType.Spirit).FirstOrDefault();

				if (spirit != null)
				{
					ShowToolTipText(tbItemToolTipSpirit, String.Format(String.Format("+{0} {1}", spirit.Amount, AppResources.UI_CharacterDetails_Character_Description_Spirit), spirit.Amount));
				}

				foreach (var stat in _itemForToolTip.BonusStats.OrderBy(s => s.Stat))
				{
					var text = String.Format(AppResources.ResourceManager.GetString(String.Format("Item_BonusStat_{0}", stat.Stat)) ?? "??? {0} ???", stat.Amount);
					var element = UIHelper.FindChild<TextBlock>(spItemToolTipContent, String.Format("tbItemToolTip{0}", stat.Stat));
					if (element != null)
					{
						ShowToolTipText(element, text);
					}
					else
					{
						var textBlock = new TextBlock();
						textBlock.Text = text;
						textBlock.Style = bonusStatStyle;
						spItemToolTipBonusStats.Children.Add(textBlock);
						spItemToolTipBonusStats.Visibility = Visibility.Visible;
					}
				}
			}
			// spells
			if (_itemForToolTip.ItemSpells != null)
			{
				foreach (var itemSpell in _itemForToolTip.ItemSpells)
				{
					if (String.IsNullOrEmpty(itemSpell.Spell.Description))
					{
						continue;
					}

					var consumableString = "Consumable";
					
					if (!itemSpell.Consumable && String.IsNullOrEmpty(itemSpell.Spell.CastTime))
					{
						consumableString = String.Format("Not{0}", consumableString);
					}

					var formatText = AppResources.ResourceManager.GetString(String.Format("Item_Spell_{0}", consumableString)) ?? "???: {0}";
					var itemSpellText = new TextBlock();
					itemSpellText.Text = String.Format(formatText, itemSpell.Spell.Description);
					itemSpellText.Style = bonusStatStyle;
					spItemToolTipSpells.Children.Add(itemSpellText);
				}

				spItemToolTipSpells.Visibility = Visibility.Visible;
			}
			// description
			if (!String.IsNullOrEmpty(_itemForToolTip.Description))
			{
				ShowToolTipText(tbItemToolTipDescription, String.Format("\"{0}\"", _itemForToolTip.Description));
			}
			// sell price
			if (_itemForToolTip.SellPrice > 0)
			{
				var sellPriceText = new TextBlock();
				sellPriceText.Text = AppResources.Item_SellPrice;
				sellPriceText.Style = normalStyle;
				sellPriceText.Margin = new Thickness(0, 0, 4, 0);
				sellPriceText.VerticalAlignment = VerticalAlignment.Center;
				spItemToolTipSellPrice.Children.Add(sellPriceText);

				if (_itemForToolTip.SellPriceObject.Gold > 0)
				{
					var goldText = new TextBlock();
					goldText.Text = _itemForToolTip.SellPriceObject.Gold.ToString();
					goldText.Style = normalStyle;
					goldText.VerticalAlignment = VerticalAlignment.Center;
					spItemToolTipSellPrice.Children.Add(goldText);

					var goldImage = new Image();
					goldImage.Source = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/Coin_Gold.png");
					goldImage.Width = 24;
					goldImage.Height = 17;
					goldImage.Margin = new Thickness(6, 0, 6, 0);
					goldImage.VerticalAlignment = VerticalAlignment.Center;
					spItemToolTipSellPrice.Children.Add(goldImage);
				}
				if (_itemForToolTip.SellPriceObject.Silver > 0)
				{
					var silverText = new TextBlock();
					silverText.Text = _itemForToolTip.SellPriceObject.Silver.ToString();
					silverText.Style = normalStyle;
					silverText.VerticalAlignment = VerticalAlignment.Center;
					spItemToolTipSellPrice.Children.Add(silverText);

					var silverImage = new Image();
					silverImage.Source = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/Coin_Silver.png");
					silverImage.Width = 24;
					silverImage.Height = 17;
					silverImage.Margin = new Thickness(6, 0, 6, 0);
					silverImage.VerticalAlignment = VerticalAlignment.Center;
					spItemToolTipSellPrice.Children.Add(silverImage);
				}
				if (_itemForToolTip.SellPriceObject.Copper > 0)
				{
					var copperText = new TextBlock();
					copperText.Text = _itemForToolTip.SellPriceObject.Copper.ToString();
					copperText.Style = normalStyle;
					copperText.VerticalAlignment = VerticalAlignment.Center;
					spItemToolTipSellPrice.Children.Add(copperText);

					var copperImage = new Image();
					copperImage.Source = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/Coin_Copper.png");
					copperImage.Width = 24;
					copperImage.Height = 17;
					copperImage.Margin = new Thickness(6, 0, 0, 0);
					copperImage.VerticalAlignment = VerticalAlignment.Center;
					spItemToolTipSellPrice.Children.Add(copperImage);
				}

				spItemToolTipSellPrice.Visibility = Visibility.Visible;
			}
			// source
			if (_itemForToolTip.ItemSource != null)
			{
				ShowToolTipText(tbItemToolTipSpacer, " ");

				var stackPanel = new StackPanel();
				stackPanel.Orientation = System.Windows.Controls.Orientation.Horizontal;
				spItemToolTipSource.Children.Add(stackPanel);

				var descriptionText = new TextBlock();
				descriptionText.Text = AppResources.Item_Source;
				descriptionText.Style = descriptionStyle;
				stackPanel.Children.Add(descriptionText);

				var valueText = new TextBlock();
				valueText.Text = AppResources.ResourceManager.GetString(String.Format("Item_ItemSourceType_{0}", _itemForToolTip.ItemSource.SourceType));
				valueText.Style = normalStyle;
				valueText.Margin = new Thickness(6, 0, 0, 0);
				stackPanel.Children.Add(valueText);

				spItemToolTipSource.Visibility = Visibility.Visible;
			}
		}

		/// <summary>
		/// Fetches the socket details.
		/// </summary>
		/// <param name="itemContainer">The item container.</param>
		/// <param name="item">The item.</param>
		/// <param name="index">The index.</param>
		private void FetchSocketDetails(CharacterItemContainer itemContainer, int index)
		{
			if (((CharacterItem)itemContainer.DataContext).TooltipParams == null)
			{
				return;
			}

			int itemId = 0;
			switch (index)
			{
				case 0:
					{
						itemId = ((CharacterItem)itemContainer.DataContext).TooltipParams.Gem0;
					} break;
				case 1:
					{
						itemId = ((CharacterItem)itemContainer.DataContext).TooltipParams.Gem1;
					} break;
				case 2:
					{
						itemId = ((CharacterItem)itemContainer.DataContext).TooltipParams.Gem2;
					} break;
				case 3:
					{
						itemId = ((CharacterItem)itemContainer.DataContext).TooltipParams.Gem3;
					} break;
			}

			if (itemId != 0)
			{
				if (!_cachedGems.ContainsKey(itemId))
				{
					BattleNetClient.Current.GetItemAsync(itemId, item => SocketDetailsRetrieved(item, index));	
				}
				else
				{
					SocketDetailsRetrieved(_cachedGems[itemId], index);
				}
			}
		}

		/// <summary>
		/// Called once the socket details have been retrieved.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="index">The index.</param>
		private void SocketDetailsRetrieved(Item item, int index)
		{
			if (!_cachedGems.ContainsKey(item.Id))
			{
				_cachedGems.Add(item.Id, item);
			}

			var imageElement = UIHelper.FindChild<Image>(spItemToolTipSockets, String.Format("imgItemToolTipSocketImage{0}", index));
			var textElement = UIHelper.FindChild<TextBlock>(spItemToolTipSockets, String.Format("tbItemToolTipSocket{0}", index));
			var nameElement = UIHelper.FindChild<TextBlock>(spItemToolTipSockets, String.Format("tbItemToolTipSocketName{0}", index));

			imageElement.Source = BattleNetClient.Current.GetIcon(item.Icon, IconSize.Small);
			ShowToolTipText(nameElement, item.Name);

			if (item.GemInfo != null && item.GemInfo.Bonus != null)
			{
				textElement.Text = item.GemInfo.Bonus.Name;
				textElement.Style = (Style)Resources["CharacterDetailsItemToolTipNormalTextStyle"];
			}
		}

		/// <summary>
		/// Clears the tool tip.
		/// </summary>
		private void ClearToolTip()
		{
			ShowToolTipText(tbItemToolTipLoading, String.Empty);
			pbItemToolTip.IsIndeterminate = false;
			pbItemToolTip.Visibility = Visibility.Collapsed;
			ShowToolTipText(tbItemToolTipName, String.Empty);
			ShowToolTipText(tbItemToolTipBinding, String.Empty);
			ShowToolTipText(tbItemToolTipMaxCount, String.Empty);
			ShowToolTipText(tbItemToolTipInventoryType, String.Empty);
			ShowToolTipText(tbItemToolTipSubClass, String.Empty);
			ShowToolTipText(tbItemToolTipWeaponInfoDamage, String.Empty);
			ShowToolTipText(tbItemToolTipWeaponInfoSpeed, String.Empty);
			ShowToolTipText(tbItemToolTipWeaponInfoDps, String.Empty);
			ShowToolTipText(tbItemToolTipArmor, String.Empty);
			ShowToolTipText(tbItemToolTipStrength, String.Empty);
			ShowToolTipText(tbItemToolTipAgility, String.Empty);
			ShowToolTipText(tbItemToolTipStamina, String.Empty);
			ShowToolTipText(tbItemToolTipIntellect, String.Empty);
			ShowToolTipText(tbItemToolTipSpirit, String.Empty);
			spItemToolTipSockets.Children.Clear();
			spItemToolTipSockets.Visibility = Visibility.Collapsed;
			ShowToolTipText(tbItemToolTipDurability, String.Empty);
			spItemToolTipAllowableClasses.Children.Clear();
			spItemToolTipAllowableClasses.Visibility = Visibility.Collapsed;
			ShowToolTipText(tbItemToolTipRequiredLevel, String.Empty);
			ShowToolTipText(tbItemToolTipRequiredFaction, String.Empty);
			ShowToolTipText(tbItemToolTipItemLevel, String.Empty);
			spItemToolTipBonusStats.Children.Clear();
			spItemToolTipBonusStats.Visibility = Visibility.Collapsed;
			spItemToolTipSpells.Children.Clear();
			spItemToolTipSpells.Visibility = Visibility.Collapsed;
			ShowToolTipText(tbItemToolTipDescription, String.Empty);
			spItemToolTipSellPrice.Children.Clear();
			spItemToolTipSellPrice.Visibility = Visibility.Collapsed;
			ShowToolTipText(tbItemToolTipSpacer, String.Empty);
			spItemToolTipSource.Children.Clear();
			spItemToolTipSource.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// Shows the tool tip text.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="text">The text.</param>
		/// <param name="foregroundBrush">The foreground brush.</param>
		private void ShowToolTipText(FrameworkElement element, string text, Brush foregroundBrush = null)
		{
			((TextBlock)element).Text = text;
			if (foregroundBrush != null)
			{
				((TextBlock)element).Foreground = foregroundBrush;
			}
			element.Visibility = !String.IsNullOrEmpty(text) ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <summary>
		/// This method is called when the hardware back key is pressed.
		/// </summary>
		/// <param name="e">Set e.Cancel to true to indicate that the request was handled by the application.</param>
		protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
		{
			base.OnBackKeyPress(e);

			if (brdItemToolTip.Visibility == Visibility.Visible)
			{
				HideAllItemContainerSelections();
				HideToolTip();
				e.Cancel = true;
			}
		}

		/// <summary>
		/// Handles the MouseLeftButtonDown event of the FavoriteToggle control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
		private void FavoriteToggle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (IsolatedStorageManager.IsCharacterStored(ViewModel.Character.Region, ViewModel.Character.Realm, ViewModel.Character.Name))
			{
				IsolatedStorageManager.UnstoreCharacter(ViewModel.Character);
				ViewModel.ToggleCharacterFavorite();
			}
			else
			{
				pbStoreCharacter.IsIndeterminate = true;
				gdStoreCharacterOverlay.Visibility = Visibility.Visible;
				Dispatcher.BeginInvoke(() =>
				{
					IsolatedStorageManager.StoreCharacter(ViewModel.Character);
					gdStoreCharacterOverlay.Visibility = Visibility.Collapsed;
					pbStoreCharacter.IsIndeterminate = false;
					ViewModel.ToggleCharacterFavorite();
				});
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}