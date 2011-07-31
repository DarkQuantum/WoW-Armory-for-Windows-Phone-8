using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Helpers;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ItemToolTipSetItem
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public bool IsEquipped { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemToolTipSetItem"/> class.
		/// </summary>
		public ItemToolTipSetItem()
		{
		}

		public ItemToolTipSetItem( XElement root )
		{
			Name = root.GetAttributeValue( "name" );
			var equipped = root.GetAttributeValue( "equipped" );
			IsEquipped = false;
			if ( equipped != null )
			{
				IsEquipped = equipped.Equals( "1" );
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
