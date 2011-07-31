using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Models;

namespace WowArmory.Core.Pages
{
	[DataContract]
	public class ItemToolTipPage : ArmoryPage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public ItemToolTip ItemToolTip { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		public ItemToolTipPage()
		{
		}

		public ItemToolTipPage( ArmoryPage armoryPage )
			: base( armoryPage.Document )
		{
			FetchData();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public static ItemToolTipPage FromArmoryPage( ArmoryPage armoryPage )
		{
			var derivedPage = armoryPage as ItemToolTipPage;
			if ( derivedPage == null )
			{
				derivedPage = new ItemToolTipPage( armoryPage );
			}
			return derivedPage;
		}

		private void FetchData()
		{
			ItemToolTip = Root.Elements( "itemTooltips" ).FirstOrDefault().Elements( "itemTooltip" ).Select( e => new ItemToolTip( e, Region ) ).FirstOrDefault();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
