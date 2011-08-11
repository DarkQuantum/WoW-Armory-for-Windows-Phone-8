using System;
using System.Runtime.Serialization;
using System.Windows.Media;
using WowArmory.Core.BattleNet.Models;
using WowArmory.Core.Languages;
using WowArmory.Core.Managers;

namespace WowArmory.Models
{
	public class RealmItem : Realm
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		public string LocalizedPopulation
		{
			get { return AppResources.ResourceManager.GetString(String.Format("BattleNet_Realm_Population_{0}", Population)); }
		}

		public string LocalizedType
		{
			get { return AppResources.ResourceManager.GetString(String.Format("BattleNet_Realm_Type_{0}", Type)); }
		}

		public ImageSource StatusImageSource
		{
			get { return CacheManager.GetImageSourceFromCache(String.Format("/WowArmory.Core;Component/Images/RealmList_Status_{0}.png", Status ? "Online" : "Offline")); }
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		public RealmItem(Realm realm)
		{
			base.Name = realm.Name;
			base.Population = realm.Population;
			base.Queue = realm.Queue;
			base.Slug = realm.Slug;
			base.Status = realm.Status;
			base.Type = realm.Type;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
