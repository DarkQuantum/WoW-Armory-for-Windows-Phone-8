using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Languages;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class CharacterDetailsPage : PhoneApplicationPage
	{
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

			BuildReputation();
			BuildProfessions();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		
		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Handles the SelectionChanged event of the CharacterPivot control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void CharacterPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
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
			rectangle.Fill = (Brush)Resources["ProfessionBar"];

			var image = new Image();
			Grid.SetRow(image, 0);
			Grid.SetColumn(image, 0);
			image.Source = BattleNetClient.Current.GetIcon(profession.Icon);
			image.Width = 56;
			image.Height = 56;
			image.HorizontalAlignment = HorizontalAlignment.Left;
			image.VerticalAlignment = VerticalAlignment.Center;
			image.Margin = new Thickness(4);

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
			grid.Children.Add(image);
			grid.Children.Add(nameTextBlock);
			grid.Children.Add(valueTextBlock);

			spProfessions.Children.Add(grid);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}