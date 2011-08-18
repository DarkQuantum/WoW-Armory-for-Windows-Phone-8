using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum ItemBinding
	{
		[EnumMember]
		None = 0,
		[EnumMember]
		PickedUp = 1,
		[EnumMember]
		Equipped = 2
	}
}