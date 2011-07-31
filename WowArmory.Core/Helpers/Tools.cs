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
