using System.ComponentModel;

namespace WowArmory.Core.BattleNet.Models
{
	public enum GuildSide
	{
		[Description("alliance")]
		Alliance = 0,
		[Description("horde")]
		Horde = 1
	}
}