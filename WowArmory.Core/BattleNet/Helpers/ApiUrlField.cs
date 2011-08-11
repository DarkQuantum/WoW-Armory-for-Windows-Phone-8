using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WowArmory.Core.BattleNet.Helpers
{
	[AttributeUsage(AttributeTargets.Field)]
	public class ApiUrlField : Attribute
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the name of the field.
		/// </summary>
		/// <value>
		/// The name of the field.
		/// </value>
		public string Name { get; set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}