using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ReputationFaction
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public bool IsHeader { get; set; }
		[DataMember]
		public string IconKey { get; set; }
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string Key { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public int Reputation { get; set; }
		[DataMember]
		public ObservableCollection<ReputationFaction> Factions { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ReputationFaction"/> class.
		/// </summary>
		public ReputationFaction()
		{
		}

		public ReputationFaction( XElement root )
		{
			var header = root.GetAttributeValue( "header" );
			IsHeader = false;
			if ( !String.IsNullOrEmpty( header ) )
			{
				if ( header.Equals( "1" ) )
				{
					IsHeader = true;
				}
			}

			IconKey = root.GetAttributeValue( "iconKey" );
			Id = (int)root.GetAttributeValue( "id", ConvertToType.Int );
			Key = root.GetAttributeValue( "key" );
			Name = root.GetAttributeValue( "name" );
			Reputation = (int)root.GetAttributeValue( "reputation", ConvertToType.Int );
			Factions = root.Elements( "faction" ).Select( e => new ReputationFaction( e ) ).ToObservableCollection();
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
