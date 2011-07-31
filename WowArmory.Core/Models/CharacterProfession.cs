// 
// Copyright (c) 2010 Christian Krueger <christian.krueger@krueger-c.com>
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge and for non-commercial usage
// only, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify,
// merge, publish, and/or distribute copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

using System;
using System.Runtime.Serialization;
using System.Windows.Media;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Storage;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class CharacterProfession
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string Key { get; set; }
		[DataMember]
		public int Max { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public int Value { get; set; }

		public ImageSource IconImage
		{
			get
			{
				var iconKey = "unknown";

				switch ( Id )
				{
					case 171: // Alchemy
						iconKey = "alchemy"; break;
					case 164: // Blacksmithing
						iconKey = "blacksmithing"; break;
					case 333: // Enchanting
						iconKey = "enchanting"; break;
					case 202: // Engineering
						iconKey = "engineering"; break;
					case 182: // Herbalism
						iconKey = "herbalism"; break;
					case 773: // Inscription
						iconKey = "inscription"; break;
					case 755: // Jewelcrafting
						iconKey = "jewelcrafting"; break;
					case 165: // Leatherworking
						iconKey = "leatherworking"; break;
					case 186: // Mining
						iconKey = "mining"; break;
					case 393: // Skinning
						iconKey = "skinning"; break;
					case 197: // Tailoring
						iconKey = "tailoring"; break;
					case 794: // Archaeology
						iconKey = "archaeology"; break;
					case 185: // Cooking
						iconKey = "cooking"; break;
					case 129: // First Aid
						iconKey = "firstaid"; break;
					case 356: // Fishing
						iconKey = "fishing"; break;
					case 762: // Riding
						iconKey = "riding"; break;
				}

				return StorageManager.GetImageSourceFromCache( String.Format( "/WowArmory.Core;component/Images/Icons/Professions/{0}.png", iconKey ), UriKind.Relative );
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterProfession"/> class.
		/// </summary>
		public CharacterProfession()
		{
		}

		public CharacterProfession( XElement root )
		{
			Id = (int)root.GetAttributeValue( "id", ConvertToType.Int );
			Key = root.GetAttributeValue( "key" );
			Max = (int)root.GetAttributeValue( "max", ConvertToType.Int );
			Name = root.GetAttributeValue( "name" );
			Value = (int)root.GetAttributeValue( "value", ConvertToType.Int );
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
