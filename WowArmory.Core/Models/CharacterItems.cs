using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class CharacterItems
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public ObservableCollection<CharacterItem> Items { get; set; }

		public CharacterItem Head
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Head ).FirstOrDefault();
			}
		}

		public CharacterItem Neck
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Neck ).FirstOrDefault();
			}
		}

		public CharacterItem Shoulder
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Shoulder ).FirstOrDefault();
			}
		}

		public CharacterItem Cape
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Cape ).FirstOrDefault();
			}
		}

		public CharacterItem Chest
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Chest ).FirstOrDefault();
			}
		}

		public CharacterItem Shirt
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Shirt ).FirstOrDefault();
			}
		}

		public CharacterItem Tabard
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Tabard ).FirstOrDefault();
			}
		}

		public CharacterItem Wrist
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Wrist ).FirstOrDefault();
			}
		}

		public CharacterItem Hands
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Hands ).FirstOrDefault();
			}
		}

		public CharacterItem Waist
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Waist ).FirstOrDefault();
			}
		}

		public CharacterItem Legs
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Legs ).FirstOrDefault();
			}
		}

		public CharacterItem Feet
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Feet ).FirstOrDefault();
			}
		}

		public CharacterItem FingerLeft
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.FingerLeft ).FirstOrDefault();
			}
		}

		public CharacterItem FingerRight
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.FingerRight ).FirstOrDefault();
			}
		}

		public CharacterItem TrinketLeft
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.TrinketLeft ).FirstOrDefault();
			}
		}

		public CharacterItem TrinketRight
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.TrinketRight ).FirstOrDefault();
			}
		}

		public CharacterItem MainHand
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.MainHand ).FirstOrDefault();
			}
		}

		public CharacterItem SecondaryHand
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.SecondaryHand ).FirstOrDefault();
			}
		}

		public CharacterItem Ranged
		{
			get
			{
				return Items.Where( i => i.Slot == CharacterItemSlot.Ranged ).FirstOrDefault();
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterItems"/> class.
		/// </summary>
		public CharacterItems()
		{
		}

		public CharacterItems( XElement root, Region region )
		{
			Items = new ObservableCollection<CharacterItem>();

			if ( root.HasElements )
			{
				foreach ( var element in root.Elements() )
				{
					var item = new CharacterItem
					{
						DisplayInfoId = (int)element.GetAttributeValue( "displayInfoId", ConvertToType.Int ),
						Durability = (int)element.GetAttributeValue( "durability", ConvertToType.Int ),
						IconName = element.GetAttributeValue( "icon" ),
						Id = (int)element.GetAttributeValue( "id", ConvertToType.Int ),
						Level = (int)element.GetAttributeValue( "level", ConvertToType.Int ),
						MaxDurability = (int)element.GetAttributeValue( "maxDurability", ConvertToType.Int ),
						Name = element.GetAttributeValue( "name" ),
						Rarity = (CharacterItemRarity)(int)element.GetAttributeValue( "rarity", ConvertToType.Int ),
						Slot = (CharacterItemSlot)(int)element.GetAttributeValue( "slot", ConvertToType.Int ),
						Region = region
					};

					Items.Add( item );
				}
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
