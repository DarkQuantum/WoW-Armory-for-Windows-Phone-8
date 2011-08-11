using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WowArmory.Core.Extensions
{
	public static class EnumExtentions
	{
		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Returns all names defined for the specified enum object.
		/// </summary>
		/// <param name="e">The enum object to retrieve the names from.</param>
		/// <returns>
		/// All names defined for the specified enum object.
		/// </returns>
		public static string[] GetNames(this Enum e)
		{
			return e.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fieldInfo => fieldInfo.Name).ToArray();
		}

		/// <summary>
		/// Returns all values defined for the specified enum object.
		/// </summary>
		/// <param name="e">The enum object to retrieve the values from.</param>
		/// <returns>
		/// All values defined for the specified enum object.
		/// </returns>
		public static Array GetValues(this Enum e)
		{
			return e.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fieldInfo => (int) Enum.Parse(e.GetType(), fieldInfo.Name, false)).ToArray();
		}

		/// <summary>
		/// Gets the description attribute of the specified enumerator value.
		/// </summary>
		/// <param name="value">The value of the enumerator.</param>
		/// <returns></returns>
		public static string GetEnumDescription(this Enum value)
		{
			var fieldInfo = value.GetType().GetField(value.ToString());
			var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
			
			if (descriptionAttribute == null)
			{
				return String.Empty;
			}

			return String.IsNullOrEmpty(descriptionAttribute.Description) ? String.Empty : descriptionAttribute.Description;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}