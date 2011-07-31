using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using System.Xml.Linq;
using WowArmory.Core.Enumerations;

namespace WowArmory.Core.Helpers
{
	public static class Tools
	{
		//---------------------------------------------------------------------------
		#region --- Methods ---
		//---------------------------------------------------------------------------
		public static ObservableCollection<T> ToObservableCollection<T> ( this IEnumerable<T> source )
		{
			if ( source == null )
			{
				throw new ArgumentNullException( "source" );
			}

			var result = new ObservableCollection<T>();

			foreach ( var item in source )
			{
				result.Add( item );
			}

			return result;
		}

		public static void Upsert<TKey, TValue>( this Dictionary<TKey, TValue> dictionary, TKey key, TValue value )
		{
			if ( dictionary.ContainsKey( key ) )
			{
				dictionary[ key ] = value;
			}
			else
			{
				dictionary.Add( key, value );
			}
		}

		public static string GetAttributeValue( this XElement element, string attributeName )
		{
			string result = null;
			var attribute = element.Attributes( attributeName ).FirstOrDefault();
			if ( attribute != null )
			{
				result = attribute.Value;
			}

			return result;
		}

		public static object GetAttributeValue( this XElement element, string attributeName, ConvertToType toType )
		{
			object result = GetAttributeValue( element, attributeName );

			switch ( toType )
			{
				case ConvertToType.Int:
					{
						if ( result == null || String.IsNullOrEmpty( result.ToString() ) ) return 0;
						result = Int32.Parse( result.ToString(), CultureInfo.InvariantCulture );
					} break;
				case ConvertToType.Double:
					{
						if ( result == null || String.IsNullOrEmpty( result.ToString() ) ) return 0.0;
						result = Double.Parse( result.ToString(), CultureInfo.InvariantCulture );
					} break;
			}

			return result;
		}

		public static Color ColorFromHex( string hex )
		{
			return Color.FromArgb( Convert.ToByte( hex.Substring( 1, 2 ), 16 ),
			                       Convert.ToByte( hex.Substring( 3, 2 ), 16 ),
			                       Convert.ToByte( hex.Substring( 5, 2 ), 16 ),
			                       Convert.ToByte( hex.Substring( 7, 2 ), 16 ) );
		}
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
