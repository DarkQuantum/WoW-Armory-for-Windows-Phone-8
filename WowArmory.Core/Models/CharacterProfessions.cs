using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using WowArmory.Core.Helpers;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class CharacterProfessions
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public ObservableCollection<CharacterProfession> Primary { get; set; }
		[DataMember]
		public ObservableCollection<CharacterProfession> Secondary { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterProfessions"/> class.
		/// </summary>
		public CharacterProfessions()
		{
		}

		public CharacterProfessions( XElement root )
		{
			var professions = root.Element( "professions" );
			if ( professions != null )
			{
				Primary = professions.Elements( "skill" ).Select( s => new CharacterProfession( s ) ).ToObservableCollection();
			}
			
			var secondaryProfessions = root.Element( "secondaryProfessions" );
			if ( secondaryProfessions != null )
			{
				Secondary = secondaryProfessions.Elements( "skill" ).Select( s => new CharacterProfession( s ) ).ToObservableCollection();
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
