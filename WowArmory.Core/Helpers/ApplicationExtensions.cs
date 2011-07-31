using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Shell;

namespace WowArmory.Core.Helpers
{
	public static class ApplicationExtensions
	{
		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public static Dictionary<string, object> RetrieveFromPhoneState( this Application app )
		{
			var state = new Dictionary<string, object>();

			if ( PhoneApplicationService.Current.State != null )
			{
				state = (Dictionary<string, object>)PhoneApplicationService.Current.State;
			}

			return state;
		}

		public static void SaveToPhoneState( this Application app, string key, object value )
		{
			if ( PhoneApplicationService.Current.State.ContainsKey( key ) )
			{
				PhoneApplicationService.Current.State.Remove( key );
			}

			PhoneApplicationService.Current.State.Add( key, value );
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}