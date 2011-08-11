using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WowArmory.Core.Extensions
{
	public static class EnumerableExtensions
	{
		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
		{
			if (source == null) return null;

			var result = new ObservableCollection<T>();
			foreach (var item in source)
			{
				result.Add(item);
			}
			return result;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
