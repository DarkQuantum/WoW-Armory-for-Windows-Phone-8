using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using WowArmory.Controllers;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Extensions;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;
using WowArmory.Models;

namespace WowArmory.ViewModels
{
	public class RealmListViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private ObservableCollection<RealmItem> _realms;
		private bool _isProgressBarVisible = false;
		private bool _isProgressBarIndeterminate = false;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the realms.
		/// </summary>
		/// <value>
		/// The realms.
		/// </value>
		public ObservableCollection<RealmItem> Realms
		{
			get { return _realms; }
			set
			{
				_realms = value;
				RaisePropertyChanged("Realms");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the progress bar is visible.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the progress bar is visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsProgressBarVisible
		{
			get { return _isProgressBarVisible; }
			set
			{
				if (_isProgressBarVisible == value) return;

				_isProgressBarVisible = value;
				RaisePropertyChanged("IsProgressBarVisible");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether ththe progress bar is indeterminate.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the progress bar is indeterminate; otherwise, <c>false</c>.
		/// </value>
		public bool IsProgressBarIndeterminate
		{
			get { return _isProgressBarIndeterminate; }
			set
			{
				if (_isProgressBarIndeterminate == value) return;

				_isProgressBarIndeterminate = value;
				RaisePropertyChanged("IsProgressBarIndeterminate");
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Commands ---
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		

		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="RealmListViewModel"/> class.
		/// </summary>
		public RealmListViewModel()
		{
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Loads the realms.
		/// </summary>
		public void LoadRealms(bool forceRefresh = false)
		{
			IsProgressBarVisible = true;
			IsProgressBarIndeterminate = true;

			var region = AppSettingsManager.Region;
			BattleNetClient.Current.Region = region;

			if (forceRefresh || !CacheManager.CachedRealmLists.ContainsKey(region))
			{
				Realms = new ObservableCollection<RealmItem>();
				BattleNetClient.Current.GetRealmListAsync(region, realmList =>
				{
					CacheManager.CachedRealmLists[region] = realmList;
					ConstructBindableRealmList(CacheManager.CachedRealmLists[region]);
				});
			}
			else
			{
				ConstructBindableRealmList(CacheManager.CachedRealmLists[region]);
			}
		}

		/// <summary>
		/// Constructs the bindable realm list.
		/// </summary>
		/// <param name="realmList">The realm list.</param>
		private void ConstructBindableRealmList(RealmList realmList)
		{
			if (realmList == null)
			{
				//MessageBox.Show(AppResources.UI_Common_Error_NoData_Text, AppResources.UI_Common_Error_NoData_Caption, MessageBoxButton.OK);
				ApplicationController.Current.NavigateBack();
				return;
			}

			Realms = realmList.Realms.Select(realm => new RealmItem(realm)).ToObservableCollection();

			IsProgressBarVisible = false;
			IsProgressBarIndeterminate = false;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
