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
using System.Windows;
using System.Windows.Media;
using WowArmory.Core.Helpers;
using WowArmory.Core.Languages;

namespace WowArmory.Core.Models.Bars
{
	[DataContract]
	public class SecondBar
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		[DataMember]
		public int Casting { get; set; }
		[DataMember]
		public int Effective { get; set; }
		[DataMember]
		public int NotCasting { get; set; }
		[DataMember]
		public string Type { get; set; }

		public string Description
		{
			get { return AppResources.ResourceManager.GetString( String.Format( "UI_CharacterDetails_SecondBarType_{0}", Type ) ); }
		}

		public SolidColorBrush ForegroundBrush
		{
			get
			{
				var result = (SolidColorBrush)Application.Current.Resources[ "PhoneForegroundBrush" ];

				switch ( Type )
				{
					case "e":
						{
							result = new SolidColorBrush( Tools.ColorFromHex( "#ffddc819" ) );
						} break;
					case "m":
					case "p":
						{
							result = new SolidColorBrush( Tools.ColorFromHex( "#ff0f7eac" ) );
						} break;
					case "r":
						{
							result = new SolidColorBrush( Tools.ColorFromHex( "#ffce8106" ) );
						} break;
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
		public SecondBar()
		{
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
