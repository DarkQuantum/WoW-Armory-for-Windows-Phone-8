using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using WowArmory.Core.BattleNet;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Extensions;
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
		public ObservableCollection<RealmItem> Realms
		{
			get { return _realms; }
			set
			{
				_realms = value;
				RaisePropertyChanged("Realms");
			}
		}

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
		public RealmListViewModel()
		{
			LoadRealms();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		private void LoadRealms()
		{
			IsProgressBarVisible = true;
			IsProgressBarIndeterminate = true;

			if (CacheManager.RealmList == null)
			{
				BattleNetClient.Current.GetRealmListAsync(BattleNetClient.Current.Region, realmList =>
				{
				    CacheManager.RealmList = realmList;
					ConstructBindableRealmList(CacheManager.RealmList);
				});
			}
			else
			{
				ConstructBindableRealmList(CacheManager.RealmList);
			}
		}

		private void ConstructBindableRealmList(RealmList realmList)
		{
			Realms = realmList.Realms.Select(realm => new RealmItem(realm)).ToObservableCollection();

			IsProgressBarVisible = false;
			IsProgressBarIndeterminate = false;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
