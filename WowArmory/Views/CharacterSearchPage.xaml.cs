using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using WowArmory.ViewModels;

namespace WowArmory.Views
{
	public partial class CharacterSearchPage : PhoneApplicationPage
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private bool _isRealmControlTouched = false;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


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
			acbRealms.IsDropDownOpen = false;
		}

		/// <summary>
		/// Handles the KeyDown event of the Character control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
		private void Character_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				ViewModel.Name = txtCharacter.Text;
				ViewModel.SearchCharacter();
			}
		}

		/// <summary>
		/// Handles the KeyDown event of the Realms control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
		private void Realms_KeyDown(object sender, KeyEventArgs e)
		{
			_isRealmControlTouched = true;

			if (e.Key == Key.Enter)
			{
				ViewModel.Realm = acbRealms.Text;
				ViewModel.SearchCharacter();
			}
		}

		/// <summary>
		/// Handles the DropDownOpening event of the Realms control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="Microsoft.Phone.Controls.RoutedPropertyChangingEventArgs&lt;System.Boolean&gt;"/> instance containing the event data.</param>
		private void Realms_DropDownOpening(object sender, RoutedPropertyChangingEventArgs<bool> e)
		{
			if (!_isRealmControlTouched)
			{
				e.Cancel = true;
				acbRealms.IsDropDownOpen = false;
			}
		}

		/// <summary>
		/// Handles the GotFocus event of the Realms control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void Realms_GotFocus(object sender, RoutedEventArgs e)
		{
			_isRealmControlTouched = true;
		}

		/// <summary>
		/// Handles the Click event of the SearchButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.Realm = acbRealms.Text;
			ViewModel.SearchCharacter();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}