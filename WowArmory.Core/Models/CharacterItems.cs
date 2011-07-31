// 
// Copyright (c) 2010 Christian Krueger <christian.krueger@krueger-c.com>
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge and for non-commercial usage
// only, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify,
// merge, publish, and/or distribute copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

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
