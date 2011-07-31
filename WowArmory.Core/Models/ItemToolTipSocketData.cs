using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Languages;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ItemToolTipSocketData
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public List<ItemToolTipSocket> Sockets { get; set; }
		[DataMember]
		public string MatchEnchant { get; set; }
		[DataMember]
		public Region Region { get; set; }

		public string MatchEnchantText
		{
			get { return !String.IsNullOrEmpty( MatchEnchant ) ? String.Format( "{0}: {1}", AppResources.Item_Socket_Bonus, MatchEnchant ) : ""; }
		}

		public bool IsMatching
		{
			get
			{
				var notMatching = Sockets.Where( s => !s.IsMatch ).FirstOrDefault();
				return notMatching == null ? true : false;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemToolTipSocketData"/> class.
		/// </summary>
		public ItemToolTipSocketData()
		{
		}

		public ItemToolTipSocketData( XElement root, Region region )
		{
			Region = region;
			Sockets = root.Elements( "socket" ).Select( e => new ItemToolTipSocket( e, Region ) ).ToList();
			var matchEnchant = root.Element( "socketMatchEnchant" );
			MatchEnchant = matchEnchant != null ? matchEnchant.Value : "";
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
