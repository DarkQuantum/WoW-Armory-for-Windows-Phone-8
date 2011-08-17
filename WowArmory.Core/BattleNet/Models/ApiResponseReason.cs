using System.ComponentModel;

namespace WowArmory.Core.BattleNet.Models
{
	public enum ApiResponseReason
	{
		[Description("")]
		Unknown,
		[Description("Character not found.")]
		CharacterNotFound,
		[Description("Guild not found.")]
		GuildNotFound,
		[Description("Realm not found.")]
		RealmNotFound
	}
}
