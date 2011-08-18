using System;
using System.Runtime.Serialization;
using WowArmory.Core.BattleNet.Helpers;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	[Flags]
	public enum GuildFields
	{
		[EnumMember]
		[ApiUrlField(Name = "")]
		Basic = 1,
		[EnumMember]
		[ApiUrlField(Name = "members")]
		Members = 2,
		[EnumMember]
		[ApiUrlField(Name = "achievements")]
		Achievements = 4,
		[EnumMember]
		All = Basic | Members | Achievements
	}
}