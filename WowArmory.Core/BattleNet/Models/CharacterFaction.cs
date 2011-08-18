using System.ComponentModel;
using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum CharacterFaction
	{
		[EnumMember]
		[Description("alliance")]
		Alliance,
		[EnumMember]
		[Description("horde")]
		Horde
	}
}