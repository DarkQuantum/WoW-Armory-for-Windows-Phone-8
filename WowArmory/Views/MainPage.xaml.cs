using System.Windows;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;

namespace WowArmory.Views
{
	public partial class MainPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public MainPage()
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
		/// Called when one of the buttons was clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void MainPageButtonClicked(object sender, RoutedEventArgs e)
		{
			SetValue(RadTileAnimation.ContainerToAnimateProperty, TileContainer);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}