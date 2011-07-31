using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Storage;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class CharacterTalentSpec
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Icon { get; set; }
		[DataMember]
		public bool IsActive { get; set; }
		[DataMember]
		public int TreeOne { get; set; }
		[DataMember]
		public int TreeTwo { get; set; }
		[DataMember]
		public int TreeThree { get; set; }
		[DataMember]
		public Region Region { get; set; }

		public ImageSource IconImage
		{
			get { return StorageManager.GetImageSourceFromCache( String.Format( "{0}wow-icons/_images/43x43/{1}.png", Armory.Current.GetArmoryUriByRegion( Region ), Icon ) ); }
		}

		public double TreeOneFontSize
		{
			get
			{
				var result = (double)Application.Current.Resources[ "PhoneFontSizeNormal" ];
				if ( TreeOne > TreeTwo && TreeOne > TreeThree )
				{
					result = (double)Application.Current.Resources[ "PhoneFontSizeMediumLarge" ];
				}
				return result;
			}
		}

		public double TreeTwoFontSize
		{
			get
			{
				var result = (double)Application.Current.Resources[ "PhoneFontSizeNormal" ];
				if ( TreeTwo > TreeOne && TreeTwo > TreeThree )
				{
					result = (double)Application.Current.Resources[ "PhoneFontSizeMediumLarge" ];
				}
				return result;
			}
		}

		public double TreeThreeFontSize
		{
			get
			{
				var result = (double)Application.Current.Resources[ "PhoneFontSizeNormal" ];
				if ( TreeThree > TreeOne && TreeThree > TreeTwo )
				{
					result = (double)Application.Current.Resources[ "PhoneFontSizeMediumLarge" ];
				}
				return result;
			}
		}

		public bool HasSpec
		{
			get { return !String.IsNullOrEmpty( Name ); }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterTalentSpec"/> class.
		/// </summary>
		public CharacterTalentSpec()
		{
		}

		public CharacterTalentSpec( XElement root, Region region )
		{
			Region = region;
			Name = root.GetAttributeValue( "prim" );
			Icon = root.GetAttributeValue( "icon" );
			TreeOne = (int)root.GetAttributeValue( "treeOne", ConvertToType.Int );
			TreeTwo = (int)root.GetAttributeValue( "treeTwo", ConvertToType.Int );
			TreeThree = (int)root.GetAttributeValue( "treeThree", ConvertToType.Int );
			IsActive = false;
			var activeString = root.GetAttributeValue( "active" );
			if ( !String.IsNullOrEmpty( activeString ) && activeString.Equals( "1" ) )
			{
				IsActive = true;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
