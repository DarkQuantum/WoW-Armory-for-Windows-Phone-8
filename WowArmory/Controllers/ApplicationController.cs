using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;
using WowArmory.Enumerations;

namespace WowArmory.Controllers
{
	public class ApplicationController
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private static ApplicationController _instance;
		private Dictionary<Page, Uri> _pages;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the singleton for this instance.
		/// </summary>
		public static ApplicationController Current
		{
			get { return _instance ?? (_instance = new ApplicationController()); }
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationController"/> class.
		/// </summary>
		public ApplicationController()
		{
			_pages = new Dictionary<Page, Uri>();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Registers the specified page in the global page register.
		/// </summary>
		/// <param name="page">The page to register.</param>
		/// <param name="address">The address where this page is located.</param>
		public void Register(Page page, Uri address)
		{
			if (_pages.ContainsKey(page))
			{
				_pages[page] = address;
				return;
			}

			_pages.Add(page, address);
		}

		/// <summary>
		/// Unregisters the specified page from the global page register.
		/// </summary>
		/// <param name="page">The page to unregister.</param>
		public void Unregister(Page page)
		{
			if (_pages.ContainsKey(page))
			{
				_pages.Remove(page);
			}
		}

		/// <summary>
		/// Navigates to the specified page.
		/// </summary>
		/// <param name="page">The page to navigate to.</param>
		public void NavigateTo(Page page)
		{
			if (!_pages.ContainsKey(page)) return;

			var address = _pages[page];
			var root = (PhoneApplicationFrame)Application.Current.RootVisual;
			root.Navigate(address);
		}

		/// <summary>
		/// Navigates back to the previous page.
		/// </summary>
		public void NavigateBack()
		{
			var root = (PhoneApplicationFrame)Application.Current.RootVisual;
			if (root.CanGoBack)
			{
				root.GoBack();
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}