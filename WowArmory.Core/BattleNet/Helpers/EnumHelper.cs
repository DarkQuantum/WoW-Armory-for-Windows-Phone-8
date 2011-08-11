using System;

namespace WowArmory.Core.BattleNet.Helpers
{
	public static class EnumHelper
	{
		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets the api url field name for the specified value.
		/// </summary>
		/// <param name="value">The value to get the api url field name for.</param>
		/// <returns>
		/// The api url field name.
		/// </returns>
		public static string GetApiUrlFieldName(Enum value)
		{
			var result = value.ToString();
			var fieldInfo = value.GetType().GetField(value.ToString());
			var attributes = (ApiUrlField[])fieldInfo.GetCustomAttributes(typeof(ApiUrlField), false);
			if (attributes.Length > 0)
			{
				result = attributes[0].Name;
			}

			return result;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}