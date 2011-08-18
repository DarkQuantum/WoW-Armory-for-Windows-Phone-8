using System.ComponentModel;
using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum GuildSide
	{
		[EnumMember]
		[Description("alliance")]
		Alliance = 0,
		[EnumMember]
		[Description("horde")]
		Horde = 1
	}
}