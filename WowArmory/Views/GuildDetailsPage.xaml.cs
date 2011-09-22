using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Helper;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class GuildDetailsPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private bool _perksListBuilt = false;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the view model associated to this page.
		/// </summary>
		public GuildDetailsViewModel ViewModel
		{
			get
			{
				return (GuildDetailsViewModel)DataContext;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public GuildDetailsPage()
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
			Dispatcher.BeginInvoke(() => ViewModel.RefreshView());
		}

		/// <summary>
		/// Handles the MouseLeftButtonDown event of the FavoriteToggle control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
		private void FavoriteToggle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (IsolatedStorageManager.IsGuildStored(ViewModel.Guild.Region, ViewModel.Guild.Realm, ViewModel.Guild.Name))
			{
				IsolatedStorageManager.Unstore(ViewModel.Guild);
				ViewModel.ToggleGuildFavorite();
			}
			else
			{
				pbStoreGuild.IsIndeterminate = true;
				gdStoreGuildOverlay.Visibility = Visibility.Visible;
				Dispatcher.BeginInvoke(() =>
				{
					IsolatedStorageManager.Store(ViewModel.Guild);
					gdStoreGuildOverlay.Visibility = Visibility.Collapsed;
					pbStoreGuild.IsIndeterminate = false;
					ViewModel.ToggleGuildFavorite();
				});
			}
		}

		/// <summary>
		/// Handles the SelectionChanged event of the GuildPivot control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void GuildPivot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (e.AddedItems != null && e.AddedItems[0] != null && e.AddedItems[0] is PivotItem &&
				((PivotItem)e.AddedItems[0]).Name.Equals("piPerks", StringComparison.CurrentCultureIgnoreCase) && !_perksListBuilt)
			{
				BuildPerksList();
			}
		}

		/// <summary>
		/// Handles the SelectionChanged event of the ListBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (ViewModel.SelectedMember == null)
			{
				return;
			}

			ViewModel.ShowSelectedMember();
		}

		/// <summary>
		/// Builds the perks list.
		/// </summary>
		private void BuildPerksList()
		{
			ViewModel.IsProgressBarIndeterminate = true;
			ViewModel.IsProgressBarVisible = true;
			spPerks.Children.Clear();

			Dispatcher.BeginInvoke(() =>
			{
				if (CacheManager.CachedGuildPerks == null || !CacheManager.CachedGuildPerks.IsValid)
				{
					var text = new TextBlock();
					text.Text = AppResources.UI_GuildDetails_Perks_NotRetrieved;
					text.Style = (Style)Resources["PhoneTextNormalStyle"];
					spPerks.Children.Add(text);
					return;
				}

				foreach (var perk in CacheManager.CachedGuildPerks.Perks)
				{
					var nameStyle = (Style)Resources["PerkNameStyle"];
					var levelStyle = (Style)Resources["PerkLevelStyle"];
					var textStyle = (Style)Resources["PerkTextStyle"];
					var spellStyle = (Style)Resources["PerkSpellStyle"];
					var perkRetrieved = true;

					if (perk.GuildLevel > ViewModel.Guild.Level)
					{
						perkRetrieved = false;
						nameStyle = (Style)Resources["PerkNameInactiveStyle"];
						levelStyle = (Style)Resources["PerkLevelInactiveStyle"];
						textStyle = (Style)Resources["PerkTextInactiveStyle"];
						spellStyle = (Style)Resources["PerkSpellInactiveStyle"];
					}

					var grid = new Grid();
					grid.Margin = new Thickness(0, 0, 0, 6);
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
					grid.ColumnDefinitions.Add(new ColumnDefinition());
					spPerks.Children.Add(grid);

					var imageGrid = new Grid();
					imageGrid.Width = 68;
					imageGrid.Height = 68;
					imageGrid.Margin = new Thickness(0, 0, 6, 0);
					imageGrid.HorizontalAlignment = HorizontalAlignment.Center;
					imageGrid.VerticalAlignment = VerticalAlignment.Top;
					Grid.SetRow(imageGrid, 0);
					Grid.SetColumn(imageGrid, 0);
					grid.Children.Add(imageGrid);
					if (!perkRetrieved)
					{
						imageGrid.Opacity = 0.25;
					}

					var imageBackground = new Image();
					imageBackground.Source = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/Icons/Icon_Background.png");
					imageBackground.HorizontalAlignment = HorizontalAlignment.Center;
					imageBackground.VerticalAlignment = VerticalAlignment.Center;
					imageGrid.Children.Add(imageBackground);

					var image = new Image();
					image.Width = 56;
					image.Height = 56;
					image.OpacityMask = new ImageBrush { ImageSource = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/Icons/Icon_Mask.png") };
					image.HorizontalAlignment = HorizontalAlignment.Center;
					image.VerticalAlignment = VerticalAlignment.Center;
					imageGrid.Children.Add(image);

					var webClient = new WebClient();
					var localPerk = perk;
					webClient.OpenReadCompleted += delegate(object sender, OpenReadCompletedEventArgs e)
					{
						try
						{
							var webImage = new BitmapImage();
							webImage.SetSource(e.Result);
							image.Source = CacheManager.GetImageSourceFromCache(BattleNetClient.Current.GetIconUrl(localPerk.Spell.Icon), webImage);
							if (!perkRetrieved)
							{
								image.Source = ImageHelper.ToGrayscale(webImage);
							}
						}
						catch (Exception ex)
						{
							image.Source = null;
						}
					};
					webClient.OpenReadAsync(new Uri(BattleNetClient.Current.GetIconUrl(perk.Spell.Icon)));

					var imageBorder = new Image();
					imageBorder.Source = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/Icons/Icon_Border.png");
					imageBorder.HorizontalAlignment = HorizontalAlignment.Center;
					imageBorder.VerticalAlignment = VerticalAlignment.Center;
					imageGrid.Children.Add(imageBorder);

					var imageLocked = new Image();
					imageLocked.Width = 21;
					imageLocked.Height = 28;
					imageLocked.Source = CacheManager.GetImageSourceFromCache("/WowArmory.Core;Component/Images/Icons/Locked.png");
					imageLocked.HorizontalAlignment = HorizontalAlignment.Right;
					imageLocked.VerticalAlignment = VerticalAlignment.Bottom;
					imageLocked.Visibility = perkRetrieved ? Visibility.Collapsed : Visibility.Visible;
					imageGrid.Children.Add(imageLocked);

					var dataStackPanel = new StackPanel();
					dataStackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
					dataStackPanel.VerticalAlignment = VerticalAlignment.Top;
					dataStackPanel.Orientation = System.Windows.Controls.Orientation.Vertical;
					Grid.SetRow(dataStackPanel, 0);
					Grid.SetColumn(dataStackPanel, 1);
					grid.Children.Add(dataStackPanel);

					var nameGrid = new Grid();
					nameGrid.ColumnDefinitions.Add(new ColumnDefinition());
					nameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
					dataStackPanel.Children.Add(nameGrid);

					var name = new TextBlock();
					name.Text = !String.IsNullOrEmpty(perk.Spell.Subtext) ? String.Format("{0} ({1})", perk.Spell.Name, perk.Spell.Subtext) : perk.Spell.Name;
					name.Style = nameStyle;
					Grid.SetColumn(name, 0);
					nameGrid.Children.Add(name);

					var level = new TextBlock();
					level.Text = String.Format(AppResources.UI_GuildDetails_Perks_Level, perk.GuildLevel).ToUpper();
					level.Style = levelStyle;
					Grid.SetColumn(level, 1);
					nameGrid.Children.Add(level);

					var description = new TextBlock();
					description.Text = perk.Spell.Description;
					description.Style = textStyle;
					dataStackPanel.Children.Add(description);

					if (!String.IsNullOrEmpty(perk.Spell.CastTime) || !String.IsNullOrEmpty(perk.Spell.Cooldown))
					{
						var spellGrid = new Grid();
						spellGrid.ColumnDefinitions.Add(new ColumnDefinition());
						spellGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
						dataStackPanel.Children.Add(spellGrid);

						if (!String.IsNullOrEmpty(perk.Spell.CastTime))
						{
							var castTime = new TextBlock();
							castTime.Text = perk.Spell.CastTime;
							castTime.Style = spellStyle;
							Grid.SetColumn(castTime, 0);
							spellGrid.Children.Add(castTime);
						}

						if (!String.IsNullOrEmpty(perk.Spell.Cooldown))
						{
							var cooldown = new TextBlock();
							cooldown.Text = perk.Spell.Cooldown;
							cooldown.Style = spellStyle;
							Grid.SetColumn(cooldown, 1);
							spellGrid.Children.Add(cooldown);
						}
					}

					if (!String.IsNullOrEmpty(perk.Spell.Range))
					{
						var range = new TextBlock();
						range.Text = perk.Spell.Range;
						range.Style = spellStyle;
						dataStackPanel.Children.Add(range);
					}
				}
			});

			ViewModel.IsProgressBarIndeterminate = false;
			ViewModel.IsProgressBarVisible = false;
		}

		/// <summary>
		/// Handles the MouseLeftButtonDown event of the GuildEmblemImage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
		private void GuildEmblemImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			gdEmblemViewer.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// This method is called when the hardware back key is pressed.
		/// </summary>
		/// <param name="e">Set e.Cancel to true to indicate that the request was handled by the application.</param>
		protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
		{
			base.OnBackKeyPress(e);

			if (gdEmblemViewer.Visibility == Visibility.Visible)
			{
				gdEmblemViewer.Visibility = Visibility.Collapsed;
				e.Cancel = true;
				return;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}