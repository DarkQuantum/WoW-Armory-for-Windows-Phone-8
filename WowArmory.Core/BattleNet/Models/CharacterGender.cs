using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum CharacterGender
	{
		[EnumMember]
		Male = 0,
		[EnumMember]
		Female = 1
	}
}