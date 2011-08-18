using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet
{
	[DataContract]
	public enum Region
	{
		[EnumMember]
		Europe,
		[EnumMember]
		USA
	}
}