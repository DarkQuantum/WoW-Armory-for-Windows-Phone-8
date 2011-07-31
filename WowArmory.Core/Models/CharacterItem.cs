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
