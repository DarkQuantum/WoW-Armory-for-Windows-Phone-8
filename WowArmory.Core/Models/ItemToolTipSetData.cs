using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ItemToolTipSetData
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public List<ItemToolTipSetItem> Items { get; set; }
		[DataMember]
		public List<ItemToolTipSetBonus> Bonus { get; set; }

		public int SetItems
		{
			get { return Items.Count; }
		}

		public int SetEquippedItems
		{
			get { return Items.Where( i => i.IsEquipped ).Count(); }
		}

		public string SetNameText
		{
			get { return String.Format( "{0} ({1}/{2})", Name, SetEquippedItems, SetItems ); }
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemToolTipSetData"/> class.
		/// </summary>
		public ItemToolTipSetData()
		{
		}

		public ItemToolTipSetData( XElement root )
		{
			Name = root.Element( "name" ).Value;
			Items = root.Elements( "item" ).Select( e => new ItemToolTipSetItem( e ) ).ToList();
			Bonus = root.Elements( "setBonus" ).Select( e => new ItemToolTipSetBonus( e, SetEquippedItems ) ).ToList();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
