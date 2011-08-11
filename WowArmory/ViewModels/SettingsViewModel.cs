using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using WowArmory.Core.BattleNet;
using WowArmory.Core.Extensions;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;

namespace WowArmory.ViewModels
{
	public class SettingsViewModel : ViewModelBase
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private ObservableCollection<KeyValuePair<Region, string>> _regions;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets all available regions.
		/// </summary>
		public ObservableCollection<KeyValuePair<Region, string>> Regions
		{
			get
			{
				if (_regions == null)
				{
					_regions = new ObservableCollection<KeyValuePair<Region, string>>();
					foreach (Region region in (new Region()).GetValues())
					{
						_regions.Add(new KeyValuePair<Region, string>(region, AppResources.ResourceManager.GetString(String.Format("BattleNet_Region_{0}", region))));
					}
				}

				return _regions;
			}
		}

		/// <summary>
		/// Gets or sets the currently selected region.
		/// </summary>
		/// <value>
		/// The currently selected region.
		/// </value>
		public KeyValuePair<Region, string> SelectedRegion { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the app is used for the first time.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the app is used for the first time; otherwise, <c>false</c>.
		/// </value>
		public bool IsFirstTimeUsage { get; set; }
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
		/// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
		/// </summary>
		public SettingsViewModel()
		{
			ResetSettings();
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Reset/Load settings from isolated storage into the view model.
		/// </summary>
		public void ResetSettings()
		{
			SelectedRegion = Regions.Where(r => r.Key == AppSettingsManager.Region).FirstOrDefault();
			IsFirstTimeUsage = IsolatedStorageManager.GetValue("Temp_Setting_IsFirstTimeUsage", false);
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
