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
using WowArmory.Core.Enumerations;
using WowArmory.Core.Pages;

namespace WowArmory.Core.Models
{
	[DataContract]
	public class ArmoryCharacter
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public CharacterSheetPage CharacterSheetPage { get; set; }
		[DataMember]
		public CharacterReputationPage CharacterReputationPage { get; set; }
		[DataMember]
		public CharacterTalentsPage CharacterTalentsPage { get; set; }
		[DataMember]
		public CharacterActivityFeedPage CharacterActivityFeedPage { get; set; }
		[DataMember]
		public Region Region { get; set; }
		[DataMember]
		public string Locale { get; set; }
		[DataMember]
		public bool IsBookmarked { get; set; }
		[DataMember]
		public bool IsDirty { get; set; }
		[DataMember]
		public DateTime LastUpdate { get; set; }

		public bool IsValid
		{
			get
			{
				if ( CharacterSheetPage == null ) return false;
				if ( !CharacterSheetPage.IsValid ) return false;
				if ( CharacterReputationPage == null) return false;
				if ( CharacterTalentsPage == null ) return false;
				if ( CharacterActivityFeedPage == null ) return false;

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
		/// Initializes a new instance of the <see cref="ArmoryCharacter"/> class.
		/// </summary>
		public ArmoryCharacter()
		{
			IsBookmarked = false;
			IsDirty = false;
			LastUpdate = DateTime.MinValue;
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
