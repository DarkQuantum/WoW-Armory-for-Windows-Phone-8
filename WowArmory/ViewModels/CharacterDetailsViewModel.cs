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
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using WowArmory.Core.Enumerations;
using WowArmory.Core.Helpers;
using WowArmory.Core.Models;
using WowArmory.Core.Pages;
using WowArmory.Core.Storage;

namespace WowArmory.ViewModels
{
	public class CharacterDetailsViewModel : ViewModelBase
	{
		//---------------------------------------------------------------------------
		#region --- Fields ---
		//---------------------------------------------------------------------------
		private ArmoryCharacter _selectedCharacter;
		private ObservableCollection<ActivityFeedEvent> _activityFeedEvents;
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public ArmoryCharacter SelectedCharacter
		{
			get { return _selectedCharacter; }
			set
			{
				if ( _selectedCharacter == value ) return;

				_selectedCharacter = value;
				RaisePropertyChanged( "SelectedCharacter" );
				//RaisePropertyChanged( "FactionBackgroundGradientBrush" );
				RaisePropertyChanged( "FactionLevelEllipseGradientBrush" );
			}
		}

		public ObservableCollection<ActivityFeedEvent> ActivityFeedEvents
		{
			get { return _activityFeedEvents; }
			set
			{
				if ( _activityFeedEvents == value ) return;

				_activityFeedEvents = value;
				RaisePropertyChanged( "ActivityFeedEvents" );
			}
		}

		public ImageSource BookmarkImage
		{
			get
			{
				var key = String.Format( "/WowArmory.Core;component/Images/bookmark_{0}.png", StorageManager.IsCharacterStored( SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Realm, SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Name ) ? "on" : "off" );
				return StorageManager.GetImageSourceFromCache( key, UriKind.Relative );
			}
		}

		//public LinearGradientBrush FactionBackgroundGradientBrush
		//{
		//    get
		//    {
		//        if ( SelectedCharacter == null ) return null;

		//        var gradient = new LinearGradientBrush();
		//        gradient.StartPoint = new Point( 0, 0 );
		//        gradient.EndPoint = new Point( 0, 1 );

		//        switch ( SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Faction )
		//        {
		//            case CharacterFaction.Alliance:
		//                {
		//                    gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff01152b" ), Offset = 0 } );
		//                    gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff08468b" ), Offset = 0.5 } );
		//                    gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff01152b" ), Offset = 1 } );
		//                } break;
		//            case CharacterFaction.Horde:
		//                {
		//                    gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff310200" ), Offset = 0 } );
		//                    gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff8b0e08" ), Offset = 0.5 } );
		//                    gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff310200" ), Offset = 1 } );
		//                } break;
		//        }

		//        return gradient;
		//    }
		//}

		public RadialGradientBrush FactionLevelEllipseGradientBrush
		{
			get
			{
				var gradient = new RadialGradientBrush();

				if ( !IsInDesignMode )
				{
					if ( SelectedCharacter == null ) return null;

					switch ( SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Faction )
					{
						case CharacterFaction.Alliance:
							{
								gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff08468b" ), Offset = 0 } );
								gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff01152b" ), Offset = 1 } );
							} break;
						case CharacterFaction.Horde:
							{
								gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff8b0e08" ), Offset = 0 } );
								gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff310200" ), Offset = 1 } );
							} break;
					}
				}
				else
				{
					gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff08468b" ), Offset = 0 } );
					gradient.GradientStops.Add( new GradientStop { Color = Tools.ColorFromHex( "#ff01152b" ), Offset = 1 } );
				}

				return gradient;
			}
		}

		public ImageSource FactionBackgroundImage
		{
			get
			{
				var key = String.Format( "/WowArmory.Core;component/Images/bg_faction_{0}.png", (int)SelectedCharacter.CharacterSheetPage.CharacterInfo.Character.Faction );
				return StorageManager.GetImageSourceFromCache( key, UriKind.Relative );
			}
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Constructor ---
		//---------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterDetailsViewModel"/> class.
		/// </summary>
		public CharacterDetailsViewModel()
		{
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------


		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}