using System.ComponentModel;
using System.Runtime.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public enum ApiResponseReason
	{
		[EnumMember]
		[Description("")]
		Unknown,
		[EnumMember]
		[Description("Character not found.")]
		CharacterNotFound,
		[EnumMember]
		[Description("Guild not found.")]
		GuildNotFound,
		[EnumMember]
		[Description("Realm not found.")]
		RealmNotFound
	}
}
