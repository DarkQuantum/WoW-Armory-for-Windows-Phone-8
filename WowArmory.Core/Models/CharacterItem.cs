using System;
using System.Runtime.Serialization;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Storage;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class CharacterItem
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int DisplayInfoId { get; set; }
		[DataMember]
		public int Durability { get; set; }
		[DataMember]
		public string IconName { get; set; }
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public int Level { get; set; }
		[DataMember]
		public int MaxDurability { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public CharacterItemRarity Rarity { get; set; }
		[DataMember]
		public CharacterItemSlot Slot { get; set; }
		[DataMember]
		public Region Region { get; set; }

		public ImageSource IconImage
		{
			get { return StorageManager.GetImageSourceFromCache( IconUrl ); }
		}

		public string IconUrl
		{
			get { return String.Format( "{0}wow-icons/_images/64x64/{1}.jpg", Armory.Current.GetArmoryUriByRegion( Region ), IconName ); }
		}

		public ImageSource RarityImage
		{
			get
			{
				var key = String.Format( "/WowArmory.Core;component/Images/item_rarity_{0}.png", (int)Rarity );
				return StorageManager.GetImageSourceFromCache( key, UriKind.Relative );
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterItem"/> class.
		/// </summary>
		public CharacterItem()
		{
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
