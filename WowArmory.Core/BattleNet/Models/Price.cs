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

namespace WowArmory.Core.BattleNet.Models
{
	public class Price
	{
		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets the integer value of the price.
		/// </summary>
		/// <value>
		/// The integer value of the price.
		/// </value>
		public int IntValue { get; set; }

		/// <summary>
		/// Gets the copper value of the price.
		/// </summary>
		public int Copper
		{
			get
			{
				if (IntValue >= 100)
				{
					var complete = IntValue.ToString();
					var part = complete.Substring(complete.Length - 2);
					return Convert.ToInt32(part);
				}

				return IntValue;
			}
		}
		
		/// <summary>
		/// Gets the silver value of the price.
		/// </summary>
		public int Silver
		{
			get
			{
				if (IntValue >= 100)
				{
					var complete = IntValue.ToString();
					var part = complete.Substring(0, complete.Length - 2);
					if (part.Length > 2)
					{
						part = complete.Substring(part.Length - 2, 2);
					}
					return Convert.ToInt32(part);
				}

				return 0;
			}
		}

		/// <summary>
		/// Gets the gold value of the price.
		/// </summary>
		public int Gold
		{
			get
			{
				if (IntValue >= 10000)
				{
					var complete = IntValue.ToString();
					var part = complete.Substring(0, complete.Length - 4);
					return Convert.ToInt32(part);
				}

				return 0;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Constructor ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="Price"/> class.
		/// </summary>
		/// <param name="intValue">The integer value of the price.</param>
		public Price(int intValue)
		{
			IntValue = intValue;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Operators ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="WowArmory.Core.BattleNet.Models.Price"/>.
		/// </summary>
		/// <param name="intValue">The integer value of the price.</param>
		/// <returns>
		/// The price object.
		/// </returns>
		public static implicit operator Price(int intValue)
		{
			return new Price(intValue);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="WowArmory.Core.BattleNet.Models.Price"/> to <see cref="System.Int32"/>.
		/// </summary>
		/// <param name="price">The price object.</param>
		/// <returns>
		/// The integer value of the price.
		/// </returns>
		public static explicit operator int(Price price)
		{
			return price.IntValue;
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}
