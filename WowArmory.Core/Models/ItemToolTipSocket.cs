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
