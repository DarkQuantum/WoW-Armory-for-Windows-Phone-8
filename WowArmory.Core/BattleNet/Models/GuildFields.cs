using System;
using WowArmory.Core.BattleNet.Helpers;

namespace WowArmory.Core.BattleNet.Models
{
	[Flags]
	public enum GuildFields
	{
		[ApiUrlField(Name = "")]
		Basic = 1,
		[ApiUrlField(Name = "members")]
		Members = 2,
		[ApiUrlField(Name = "achievements")]
		Achievements = 4,
		All = Basic | Members | Achievements
	}
}