using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Languages;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ItemToolTipSetBonus
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int CurrentlyEquipped { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public int Threshold { get; set; }

		public string DescriptionText
		{
			get
			{
				var result = Description;
				if ( CurrentlyEquipped < Threshold )
				{
					result = String.Format( "({0}) {1}: {2}", Threshold, AppResources.Item_Set, result );
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
		/// Initializes a new instance of the <see cref="ItemToolTipSetBonus"/> class.
		/// </summary>
		public ItemToolTipSetBonus()
		{
		}

		public ItemToolTipSetBonus( XElement root, int currentlyEquipped )
		{
			CurrentlyEquipped = currentlyEquipped;
			Description = root.GetAttributeValue( "desc" );
			Threshold = (int)root.GetAttributeValue( "threshold", ConvertToType.Int );
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
