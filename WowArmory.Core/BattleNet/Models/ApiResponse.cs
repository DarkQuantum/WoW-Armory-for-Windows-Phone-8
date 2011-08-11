using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using WowArmory.Core.Extensions;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ApiResponse
	{
		[DataMember]
		[XmlElement("status", IsNullable = true)]
		public string Status { get; set; }

		[DataMember]
		[XmlElement("reason", IsNullable = true)]
		public string Reason { get; set; }

		public ApiResponseReason ReasonType
		{
			get
			{
				foreach (ApiResponseReason enumValue in (new ApiResponseReason()).GetValues())
				{
					if (enumValue.GetEnumDescription().Equals(Reason, StringComparison.CurrentCultureIgnoreCase))
					{
						return enumValue;
					}
				}

				return ApiResponseReason.Unknown;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this response is valid.
		/// </summary>
		/// <value>
		///   <c>true</c> if this response is valid; otherwise, <c>false</c>.
		/// </value>
		public bool IsValid
		{
			get { return String.IsNullOrEmpty(Status); }
		}
	}
}
