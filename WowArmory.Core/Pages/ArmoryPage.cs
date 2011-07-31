using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;

namespace WowArmory.Core.Pages
{
	[DataContract]
	public class ArmoryPage
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public Region Region { get; set; }
		[DataMember]
		public string Locale { get; set; }
		public XDocument Document { get; set; }

		public XElement Root
		{
			get { return Document != null ? Document.Root : null; }
		}

		public bool IsPageValid
		{
			get
			{
				if ( Document == null ) return false;
				if ( Root == null ) return false;

				var errorhtml = Root.Element( "errorhtml" );
				if ( errorhtml != null ) return false;

				return true;
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ArmoryPage"/> class.
		/// </summary>
		public ArmoryPage()
		{
		}

		public ArmoryPage( XDocument document )
		{
			Document = document;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
