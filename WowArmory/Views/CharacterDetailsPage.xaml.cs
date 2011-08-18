using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using WowArmory.Controllers;
using WowArmory.Controls;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Helpers;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Extensions;
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
		private WriteableBitmap _writeableBitmap = null;
		private double _currentFrame = 0;
		private int _maxFrames = 0;
		private int _frameWidth = 0;
		private int _imageWidth = 0;
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
			if (ViewModel.CachedGems == null)
			{
				ViewModel.CachedGems = new Dictionary<int, Item>();
			}
			if (ViewModel.CachedItems == null)
			{
				ViewModel.CachedItems = new Dictionary<int, Item>();
			}

			LoadView();
			BuildTalents();
			BuildReputation();
			BuildProfessions();
			BuildTitles();
		}

		/// <summary>
		/// Handles the Unloaded event of the PhoneApplicationPage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
		{
			ViewModel.CachedItems = null;
			ViewModel.CachedGems = null;
			ViewModel.IsItemToolTipOpen = false;
			ViewModel.ItemContainerForToolTip = null;
			ViewModel.ItemForToolTip = null;
			ViewModel.ItemContainerControl = String.Empty;

			ApplicationExtensions.SaveToPhoneState("CharacterDetails_ItemToolTip_IsOpen", ViewModel.IsItemToolTipOpen);
			ApplicationExtensions.SaveToPhoneState("CharacterDetails_ItemToolTip_ItemContainerControl", ViewModel.ItemContainerControl);
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
			tbPowerType.Foreground = (Brush)Resources[String.Format("{0}TextBrush", (powerType.Substring(0, 1).ToUpper() + powerType.Substring(1)).Replace("-", ""))];

			var showToolTip = ViewModel.IsItemToolTipOpen;
			HideAllItemContainerSelections();
			HideToolTip();
			if (!String.IsNullOrEmpty(ViewModel.ItemContainerControl))
			{
				var control = UIHelper.FindChild<CharacterItemContainer>(CharacterPivotItem, ViewModel.ItemContainerControl);
				if (control != null)
				{
					control.SelectionVisibility = Visibility.Visible;
					ViewModel.ItemContainerForToolTip = control;
				}
			}
			if (showToolTip)
			{
				ShowToolTip(ViewModel.ItemContainerForToolTip);
			}
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
				if (ViewModel.Character.Talents[0] != null && !String.IsNullOrEmpty(ViewModel.Character.Talents[0].Name))
				{
					var talentOne = BuildTalentInformation(ViewModel.Character.Talents[0]);
					Grid.SetColumn(talentOne, 0);
					talentOne.HorizontalAlignment = HorizontalAlignment.Left;
					talentOne.VerticalAlignment = VerticalAlignment.Top;
					gdCharacterTalents.Children.Add(talentOne);
				}
				if (ViewModel.Character.Talents[1] != null && !String.IsNullOrEmpty(ViewModel.Character.Talents[1].Name))
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
			talentImageGrid.Margin = new Thickness(0, 0, 4, 0);
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
			valueTextBlock.Text = String.Format("{0} / {1}", profession.Rank, Convert.ToInt32(Math.Floor(max)));
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
		/// Builds the user interface for the titles pivot.
		/// </summary>
		private void BuildTitles()
		{
			if (ViewModel.Character != null)
			{
				if (ViewModel.Character.Titles != null &&
					ViewModel.Character.Titles.Count > 0)
				{
					foreach (var title in ViewModel.Character.Titles.OrderBy(t => t.Name))
					{
						var titleGrid = new Grid();
						titleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
						titleGrid.ColumnDefinitions.Add(new ColumnDefinition());
						titleGrid.Margin = new Thickness(8, 0, 8, 0);
						spCharacterTitles.Children.Add(titleGrid);

						var titleIcon = new Rectangle();
						titleIcon.Fill = (Brush)Resources["PhoneSubtleBrush"];
						titleIcon.Width = 16;
						titleIcon.Height = 16;
						titleIcon.OpacityMask = new ImageBrush { ImageSource = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/CharacterDetails/Character_Title.png"), Stretch = Stretch.Fill };
						titleIcon.Opacity = 0.5;
						titleIcon.HorizontalAlignment = HorizontalAlignment.Left;
						titleIcon.VerticalAlignment = VerticalAlignment.Center;
						titleIcon.Margin = new Thickness(0, 0, 6, 0);
						Grid.SetColumn(titleIcon, 0);
						titleGrid.Children.Add(titleIcon);

						var titleText = new TextBlock();
						titleText.Text = title.Name.Replace("%s", ViewModel.Character.Name);
						titleText.Style = (Style)Resources["CharacterDetailsTitleTextStyle"];
						Grid.SetColumn(titleText, 1);
						titleGrid.Children.Add(titleText);
					}
				}
				else
				{
					var noTitlesText = new TextBlock();
					noTitlesText.Text = AppResources.UI_CharacterDetails_Character_NoTitles;
					noTitlesText.Style = (Style)Resources["CharacterDetailsNoTitlesTextStyle"];
					spCharacterTitles.Children.Add(noTitlesText);
				}
			}
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
				ViewModel.ItemContainerControl = itemContainer.Name;

				if (itemContainer.SelectionVisibility == Visibility.Visible)
				{
					isSelected = true;
					ViewModel.ItemContainerControl = String.Empty;
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
			ViewModel.IsItemToolTipOpen = false;
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
			ViewModel.ItemContainerForToolTip = itemContainer;
			itemContainer.SelectionVisibility = Visibility.Visible;
			svCharacterStats.IsEnabled = false;
			svCharacterStats.Opacity = 0.25;
			pbItemToolTip.IsIndeterminate = true;
			pbItemToolTip.Visibility = Visibility.Visible;
			ShowToolTipText(tbItemToolTipLoading, AppResources.UI_Common_LoadingData);
			brdItemToolTip.Visibility = Visibility.Visible;
			ViewModel.IsItemToolTipOpen = true;

			if (ViewModel.CachedItems.ContainsKey(((CharacterItem)itemContainer.DataContext).Id))
			{
				OnItemReceived(ViewModel.CachedItems[((CharacterItem)itemContainer.DataContext).Id]);
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
			ViewModel.ItemForToolTip = item;

			if (item == null)
			{
				ClearToolTip();
				ShowToolTipText(tbItemToolTipLoading, AppResources.UI_Common_Error_NoData_Text);
				return;
			}

			if (!ViewModel.CachedItems.ContainsKey(((CharacterItem)ViewModel.ItemContainerForToolTip.DataContext).Id))
			{
				ViewModel.CachedItems.Add(((CharacterItem)ViewModel.ItemContainerForToolTip.DataContext).Id, ViewModel.ItemForToolTip);
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
			ShowToolTipText(tbItemToolTipName, ViewModel.ItemForToolTip.Name, (Brush)Resources[String.Format("ItemQuality{0}", ViewModel.ItemForToolTip.Quality)]);
			// binding
			ShowToolTipText(tbItemToolTipBinding, AppResources.ResourceManager.GetString(String.Format("Item_Binding_{0}", ViewModel.ItemForToolTip.ItemBind)));
			// unique equippable
			if (ViewModel.ItemForToolTip.MaxCount == 1)
			{
				ShowToolTipText(tbItemToolTipMaxCount, AppResources.Item_MaxCount_UniqueEquipped);
			}
			// inventory type
			ShowToolTipText(tbItemToolTipInventoryType, AppResources.ResourceManager.GetString(String.Format("Item_InventoryType_{0}", (InventoryType)ViewModel.ItemForToolTip.InventoryType)));
			// sub class
			ShowToolTipText(tbItemToolTipSubClass, AppResources.ResourceManager.GetString(String.Format("Item_ItemSubClass_{0}_{1}", ViewModel.ItemForToolTip.ItemClass, ViewModel.ItemForToolTip.ItemSubClass)));
			// weapon information
			if (ViewModel.ItemForToolTip.WeaponInfo != null)
			{
				// damage
				if (ViewModel.ItemForToolTip.WeaponInfo.Damage != null && ViewModel.ItemForToolTip.WeaponInfo.Damage.Count > 0)
				{
					if (ViewModel.ItemForToolTip.WeaponInfo.Damage[0].MinDamage > 0 && ViewModel.ItemForToolTip.WeaponInfo.Damage[0].MaxDamage > 0)
					{
						ShowToolTipText(tbItemToolTipWeaponInfoDamage, String.Format(AppResources.Item_WeaponInfo_Damage, ViewModel.ItemForToolTip.WeaponInfo.Damage[0].MinDamage, ViewModel.ItemForToolTip.WeaponInfo.Damage[0].MaxDamage));
					}
				}
				// speed
				ShowToolTipText(tbItemToolTipWeaponInfoSpeed, String.Format(AppResources.Item_WeaponInfo_Speed, ViewModel.ItemForToolTip.WeaponInfo.WeaponSpeed));
				// dps
				ShowToolTipText(tbItemToolTipWeaponInfoDps, String.Format(AppResources.Item_WeaponInfo_Dps, ViewModel.ItemForToolTip.WeaponInfo.Dps));
			}
			// sockets
			if (ViewModel.ItemForToolTip.SocketInfo != null)
			{
				var index = 0;
				foreach (var socket in ViewModel.ItemForToolTip.SocketInfo.Sockets)
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
					Dispatcher.BeginInvoke(() => FetchSocketDetails(ViewModel.ItemContainerForToolTip, dispatcherIndex));

					index++;
				}

				spItemToolTipSockets.Visibility = Visibility.Visible;
			}
			// durability
			if (ViewModel.ItemForToolTip.MaxDurability > 0)
			{
				ShowToolTipText(tbItemToolTipDurability, String.Format(AppResources.Item_Durability, ViewModel.ItemForToolTip.MaxDurability));
			}
			// allowable classes
			if (ViewModel.ItemForToolTip.AllowableClasses != null)
			{
				var labelText = new TextBlock();
				labelText.Text = AppResources.Item_AllowableClasses;
				labelText.Style = normalStyle;
				spItemToolTipAllowableClasses.Children.Add(labelText);

				foreach (var allowableClass in ViewModel.ItemForToolTip.AllowableClasses)
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
			if (ViewModel.ItemForToolTip.RequiredLevel > 0)
			{
				ShowToolTipText(tbItemToolTipRequiredLevel, String.Format(AppResources.Item_RequiredLevel, ViewModel.ItemForToolTip.RequiredLevel));
			}
			// required faction
			if (ViewModel.ItemForToolTip.MinFactionId > 0)
			{
				var reputationFaction = ViewModel.Character.Reputation.Where(r => r.Id == ViewModel.ItemForToolTip.MinFactionId).FirstOrDefault();
				if (reputationFaction != null)
				{
					var reputation = AppResources.ResourceManager.GetString(String.Format("BattleNet_Reputation_{0}", (ReputationStanding)ViewModel.ItemForToolTip.MinReputation));
					ShowToolTipText(tbItemToolTipRequiredFaction, String.Format(AppResources.Item_RequiredFaction, reputationFaction.Name, reputation));
				}
			}
			// item level
			if (ViewModel.ItemForToolTip.ItemLevel > 0)
			{
				ShowToolTipText(tbItemToolTipItemLevel, String.Format(AppResources.Item_ItemLevel, ViewModel.ItemForToolTip.ItemLevel));
			}
			// armor
			if (ViewModel.ItemForToolTip.BaseArmor > 0)
			{
				ShowToolTipText(tbItemToolTipArmor, String.Format("{0} {1}", ViewModel.ItemForToolTip.BaseArmor, AppResources.UI_CharacterDetails_Character_Description_Armor));
			}
			// stats
			if (ViewModel.ItemForToolTip.BonusStats != null && ViewModel.ItemForToolTip.BonusStats.Count > 0)
			{
				var reforge = Reforge.None;
				var reforgeFrom = ItemBonusStatType.None;
				var reforgeTo = ItemBonusStatType.None;
				var reforgedStatAdded = false;

				if (((CharacterItem)ViewModel.ItemContainerForToolTip.DataContext).TooltipParams != null && ((CharacterItem)ViewModel.ItemContainerForToolTip.DataContext).TooltipParams.Reforge != 0)
				{
					reforge = (Reforge)((CharacterItem)ViewModel.ItemContainerForToolTip.DataContext).TooltipParams.Reforge;
					reforgeFrom = ReforgeHelper.ReforgeMapping[reforge].From;
					reforgeTo = ReforgeHelper.ReforgeMapping[reforge].To;
				}

				foreach (var stat in ViewModel.ItemForToolTip.BonusStats.OrderBy(s => s.Stat))
				{
					var statAmount = stat.Amount;

					if (reforge != Reforge.None)
					{
						if (reforgeFrom == stat.Stat)
						{
							var reforgeAmount = Convert.ToInt32(Math.Floor(statAmount * 0.4));
							statAmount = statAmount - reforgeAmount;

							tbItemToolTipReforgeFrom.Text = String.Format("({0} {1}", reforgeAmount, AppResources.ResourceManager.GetString(String.Format("Item_ReforgedStat_{0}", reforgeFrom)));
							tbItemToolTipReforgeTo.Text = String.Format("{0} {1})", reforgeAmount, AppResources.ResourceManager.GetString(String.Format("Item_ReforgedStat_{0}", reforgeTo)));
							spItemToolTipReforge.Visibility = Visibility.Visible;
						}

						if (reforgeTo == stat.Stat)
						{
							var reforgeFromStat = ViewModel.ItemForToolTip.BonusStats.Where(s => s.Stat == reforgeFrom).FirstOrDefault();
							if (reforgeFromStat != null)
							{
								var reforgeFromAmount = reforgeFromStat.Amount;
								statAmount = statAmount + Convert.ToInt32(Math.Floor(reforgeFromAmount * 0.4));
								reforgedStatAdded = true;
							}
						}
					}

					var text = String.Format(AppResources.ResourceManager.GetString(String.Format("Item_BonusStat_{0}", stat.Stat)) ?? "??? {0} ???", statAmount);
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

				if (reforge != Reforge.None && !reforgedStatAdded)
				{
					var reforgeFromStat = ViewModel.ItemForToolTip.BonusStats.Where(s => s.Stat == reforgeFrom).FirstOrDefault();
					if (reforgeFromStat != null)
					{
						var textBlock = new TextBlock();
						textBlock.Text = String.Format(AppResources.ResourceManager.GetString(String.Format("Item_BonusStat_{0}", reforgeTo)) ?? "??? {0} ???", Convert.ToInt32(Math.Floor(reforgeFromStat.Amount * 0.4)));
						textBlock.Style = bonusStatStyle;
						spItemToolTipBonusStats.Children.Add(textBlock);
						spItemToolTipBonusStats.Visibility = Visibility.Visible;
					}
				}
			}
			// spells
			if (ViewModel.ItemForToolTip.ItemSpells != null)
			{
				foreach (var itemSpell in ViewModel.ItemForToolTip.ItemSpells)
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
			if (!String.IsNullOrEmpty(ViewModel.ItemForToolTip.Description))
			{
				ShowToolTipText(tbItemToolTipDescription, String.Format("\"{0}\"", ViewModel.ItemForToolTip.Description));
			}
			// sell price
			if (ViewModel.ItemForToolTip.SellPrice > 0)
			{
				var sellPriceText = new TextBlock();
				sellPriceText.Text = AppResources.Item_SellPrice;
				sellPriceText.Style = normalStyle;
				sellPriceText.Margin = new Thickness(0, 0, 4, 0);
				sellPriceText.VerticalAlignment = VerticalAlignment.Center;
				spItemToolTipSellPrice.Children.Add(sellPriceText);

				if (ViewModel.ItemForToolTip.SellPriceObject.Gold > 0)
				{
					var goldText = new TextBlock();
					goldText.Text = ViewModel.ItemForToolTip.SellPriceObject.Gold.ToString();
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
				if (ViewModel.ItemForToolTip.SellPriceObject.Silver > 0)
				{
					var silverText = new TextBlock();
					silverText.Text = ViewModel.ItemForToolTip.SellPriceObject.Silver.ToString();
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
				if (ViewModel.ItemForToolTip.SellPriceObject.Copper > 0)
				{
					var copperText = new TextBlock();
					copperText.Text = ViewModel.ItemForToolTip.SellPriceObject.Copper.ToString();
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
			if (ViewModel.ItemForToolTip.ItemSource != null)
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
				valueText.Text = AppResources.ResourceManager.GetString(String.Format("Item_ItemSourceType_{0}", ViewModel.ItemForToolTip.ItemSource.SourceType));
				valueText.Style = normalStyle;
				valueText.Margin = new Thickness(6, 0, 0, 0);
				stackPanel.Children.Add(valueText);

				if (ViewModel.ItemForToolTip.ItemSource.SourceType.Equals("REWARD_FOR_QUEST", StringComparison.CurrentCultureIgnoreCase))
				{
					BattleNetClient.Current.GetQuestAsync(ViewModel.ItemForToolTip.ItemSource.SourceId, quest =>
					{
						if (quest == null)
						{
							return;
						}

						var stackPanelQuestName = new StackPanel();
						stackPanelQuestName.Orientation = System.Windows.Controls.Orientation.Horizontal;
						spItemToolTipSource.Children.Add(stackPanelQuestName);

						var questNameDescriptionText = new TextBlock();
						questNameDescriptionText.Text = AppResources.Item_Source_QuestName;
						questNameDescriptionText.Style = descriptionStyle;
						stackPanelQuestName.Children.Add(questNameDescriptionText);

						var questNameValueText = new TextBlock();
						questNameValueText.Text = quest.Title;
						questNameValueText.Style = normalStyle;
						questNameValueText.Margin = new Thickness(6, 0, 0, 0);
						stackPanelQuestName.Children.Add(questNameValueText);

						var stackPanelQuestCategory = new StackPanel();
						stackPanelQuestCategory.Orientation = System.Windows.Controls.Orientation.Horizontal;
						spItemToolTipSource.Children.Add(stackPanelQuestCategory);

						var questCategoryDescriptionText = new TextBlock();
						questCategoryDescriptionText.Text = AppResources.Item_Source_QuestCategory;
						questCategoryDescriptionText.Style = descriptionStyle;
						stackPanelQuestCategory.Children.Add(questCategoryDescriptionText);

						var questCategoryValueText = new TextBlock();
						questCategoryValueText.Text = quest.Category;
						questCategoryValueText.Style = normalStyle;
						questCategoryValueText.Margin = new Thickness(6, 0, 0, 0);
						stackPanelQuestCategory.Children.Add(questCategoryValueText);

						var stackPanelQuestLevel = new StackPanel();
						stackPanelQuestLevel.Orientation = System.Windows.Controls.Orientation.Horizontal;
						spItemToolTipSource.Children.Add(stackPanelQuestLevel);

						var questLevelDescriptionText = new TextBlock();
						questLevelDescriptionText.Text = AppResources.Item_Source_QuestLevel;
						questLevelDescriptionText.Style = descriptionStyle;
						stackPanelQuestLevel.Children.Add(questLevelDescriptionText);

						var questLevelValueText = new TextBlock();
						questLevelValueText.Text = quest.Level.ToString();
						questLevelValueText.Style = normalStyle;
						questLevelValueText.Margin = new Thickness(6, 0, 0, 0);
						stackPanelQuestLevel.Children.Add(questLevelValueText);

						var stackPanelQuestRequiredLevel = new StackPanel();
						stackPanelQuestRequiredLevel.Orientation = System.Windows.Controls.Orientation.Horizontal;
						spItemToolTipSource.Children.Add(stackPanelQuestRequiredLevel);

						var questRequiredLevelDescriptionText = new TextBlock();
						questRequiredLevelDescriptionText.Text = AppResources.Item_Source_QuestRequiredLevel;
						questRequiredLevelDescriptionText.Style = descriptionStyle;
						stackPanelQuestRequiredLevel.Children.Add(questRequiredLevelDescriptionText);

						var questRequiredLevelCategoryValueText = new TextBlock();
						questRequiredLevelCategoryValueText.Text = quest.ReqLevel.ToString();
						questRequiredLevelCategoryValueText.Style = normalStyle;
						questRequiredLevelCategoryValueText.Margin = new Thickness(6, 0, 0, 0);
						stackPanelQuestRequiredLevel.Children.Add(questRequiredLevelCategoryValueText);
					});
				}

				spItemToolTipSource.Visibility = Visibility.Visible;
			}
			// 3d item viewer
			if (AppSettingsManager.Is3DItemViewerEnabled)
			{
				var webClient = new WebClient();
				webClient.OpenReadCompleted += delegate(object senderInternal, OpenReadCompletedEventArgs openReadCompletedEventArgs)
				{
					try
					{
						_imageWidth = 6720;
						_frameWidth = 280;
						_maxFrames = _imageWidth / _frameWidth;
						_writeableBitmap = new WriteableBitmap(_imageWidth, _frameWidth);
						_writeableBitmap.LoadJpeg(openReadCompletedEventArgs.Result);
						cvItemViewerSpriteStrip.Width = _frameWidth;
						cvItemViewerSpriteStrip.Height = _frameWidth;
						rgItemViewerSpriteStrip.Rect = new Rect(0, 0, _frameWidth, _frameWidth);
						imgItemViewerSpriteStrip.Width = _imageWidth;
						imgItemViewerSpriteStrip.Height = _frameWidth;
						imgItemViewerSpriteStrip.Source = _writeableBitmap;
						Canvas.SetLeft(imgItemViewerSpriteStrip, 0);
						Canvas.SetTop(imgItemViewerSpriteStrip, 0);
						brdItemViewer.Visibility = Visibility.Visible;
					}
					catch (Exception ex)
					{
						// do nothing
					}
				};
				webClient.OpenReadAsync(new Uri(BattleNetClient.Current.GetItemRenderUrl(ViewModel.ItemForToolTip.Id)));
			}
			// external links
			spItemToolTipExternalLinks.Visibility = Visibility.Visible;
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
				if (!ViewModel.CachedGems.ContainsKey(itemId))
				{
					BattleNetClient.Current.GetItemAsync(itemId, item => SocketDetailsRetrieved(item, index));
				}
				else
				{
					SocketDetailsRetrieved(ViewModel.CachedGems[itemId], index);
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
			if (!ViewModel.CachedGems.ContainsKey(item.Id))
			{
				ViewModel.CachedGems.Add(item.Id, item);
			}

			var imageElement = UIHelper.FindChild<Image>(spItemToolTipSockets, String.Format("imgItemToolTipSocketImage{0}", index));
			var textElement = UIHelper.FindChild<TextBlock>(spItemToolTipSockets, String.Format("tbItemToolTipSocket{0}", index));
			var nameElement = UIHelper.FindChild<TextBlock>(spItemToolTipSockets, String.Format("tbItemToolTipSocketName{0}", index));

			imageElement.Source = BattleNetClient.Current.GetIcon(item.Icon, IconSize.Small);
			if (AppSettingsManager.ShowGemName)
			{
				ShowToolTipText(nameElement, item.Name);
			}

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
			spItemToolTipReforge.Visibility = Visibility.Collapsed;
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
			brdItemViewer.Visibility = Visibility.Collapsed;
			spItemToolTipExternalLinks.Visibility = Visibility.Collapsed;
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

			if (gdItemViewer.Visibility == Visibility.Visible)
			{
				gdItemViewer.Visibility = Visibility.Collapsed;
				e.Cancel = true;
				return;
			}

			if (brdItemToolTip.Visibility == Visibility.Visible)
			{
				HideAllItemContainerSelections();
				HideToolTip();
				e.Cancel = true;
				return;
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
				IsolatedStorageManager.Unstore(ViewModel.Character);
				ViewModel.ToggleCharacterFavorite();
			}
			else
			{
				pbStoreCharacter.IsIndeterminate = true;
				gdStoreCharacterOverlay.Visibility = Visibility.Visible;
				Dispatcher.BeginInvoke(() =>
				{
					IsolatedStorageManager.Store(ViewModel.Character);
					gdStoreCharacterOverlay.Visibility = Visibility.Collapsed;
					pbStoreCharacter.IsIndeterminate = false;
					ViewModel.ToggleCharacterFavorite();
				});
			}
		}

		/// <summary>
		/// Opens the wowhead website for the current item.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
		private void OpenWowheadForItem(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var webBrowserTask = new WebBrowserTask();
			webBrowserTask.URL = String.Format(AppResources.Item_ExternalLink_Wowhead_Url, ViewModel.ItemForToolTip.Id);
			webBrowserTask.Show();
		}

		/// <summary>
		/// Opens the item viewer.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
		private void OpenItemViewer(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			gdItemViewer.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Handles the ManipulationDelta event of the gdItemViewer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.ManipulationDeltaEventArgs"/> instance containing the event data.</param>
		private void gdItemViewer_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
		{
			var amount = Math.Abs(e.DeltaManipulation.Translation.X) / 10.0;

			if (e.DeltaManipulation.Translation.X >= 1)
			{
				_currentFrame = _currentFrame - amount;
			}
			else if (e.DeltaManipulation.Translation.X <= -1)
			{
				_currentFrame = _currentFrame + amount;
			}

			if (_currentFrame < 0)
			{
				_currentFrame = _maxFrames - 1;
			}
			else if (_currentFrame >= _maxFrames)
			{
				_currentFrame = 1;
			}

			Canvas.SetLeft(imgItemViewerSpriteStrip, (Math.Floor(_currentFrame) * _frameWidth) * -1);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}