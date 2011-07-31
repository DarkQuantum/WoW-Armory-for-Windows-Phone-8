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
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Languages;
using WowArmory.Core.Storage;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ItemToolTipSocket
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public string Color { get; set; }
		[DataMember]
		public string Enchant { get; set; }
		[DataMember]
		public string IconName { get; set; }
		[DataMember]
		public bool IsMatch { get; set; }
		[DataMember]
		public Region Region { get; set; }

		public bool IsEmpty
		{
			get { return String.IsNullOrEmpty( Enchant ); }
		}

		public string EnchantText
		{
			get
			{
				var result = Enchant;
				if ( String.IsNullOrEmpty( Enchant ) )
				{
					result = String.Format( "{0} {1}", AppResources.ResourceManager.GetString( String.Format( "Item_Socket_{0}", Color ) ), AppResources.Item_Socket );
				}
				return result;
			}
		}

		public ImageSource IconImage
		{
			get
			{
				return StorageManager.GetImageSourceFromCache( IconUrl, UriKind.RelativeOrAbsolute );
			}
		}

		public string IconUrl
		{
			get
			{
				var result = String.Format( "{0}wow-icons/_images/21x21/{1}.png", Armory.Current.GetArmoryUriByRegion( Region ), IconName );
				if ( String.IsNullOrEmpty( IconName ) )
				{
					result = String.Format( "/WowArmory.Core;component/Images/socket_{0}.png", Color.ToLower() );
				}
				return result;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemToolTipSocket"/> class.
		/// </summary>
		public ItemToolTipSocket()
		{
		}

		public ItemToolTipSocket( XElement root, Region region )
		{
			Region = region;
			Color = root.GetAttributeValue( "color" );
			Enchant = root.GetAttributeValue( "enchant" );
			IconName = root.GetAttributeValue( "icon" );
			IsMatch = false;
			if ( !String.IsNullOrEmpty( root.GetAttributeValue( "match" ) ) )
			{
				IsMatch = root.GetAttributeValue( "match" ).Equals( "1" );
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
