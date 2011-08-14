using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class CharacterSearchPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the view model associated to this page.
		/// </summary>
		public CharacterSearchViewModel ViewModel
		{
			get
			{
				return (CharacterSearchViewModel)DataContext;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterSearchPage"/> class.
		/// </summary>
		public CharacterSearchPage()
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
			ViewModel.LoadRealms();
		}

		/// <summary>
		/// Handles the KeyDown event of the txtCharacter control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
		private void txtCharacter_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				ViewModel.Name = txtCharacter.Text;
				ViewModel.SearchCharacter();
			}
		}

		///// <summary>
		///// Handles the KeyDown event of the txtRealm control.
		///// </summary>
		///// <param name="sender">The source of the event.</param>
		///// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
		//private void txtRealm_KeyDown(object sender, KeyEventArgs e)
		//{
		//    if (e.Key == Key.Enter)
		//    {
		//        ViewModel.Name = txtRealm.Text;
		//        ViewModel.SearchCharacter();
		//    }
		//}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}