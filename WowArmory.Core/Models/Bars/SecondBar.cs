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
