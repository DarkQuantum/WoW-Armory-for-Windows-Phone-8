using System.ComponentModel;

namespace WowArmory.Core.BattleNet.Models
{
	public enum ApiResponseReason
	{
		[Description("")]
		Unknown,
		[Description("Character not found.")]
		CharacterNotFound,
		[Description("Realm not found.")]
		RealmNotFound
	}
}
