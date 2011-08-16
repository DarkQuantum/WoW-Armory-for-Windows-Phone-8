using System;
using WowArmory.Core.BattleNet.Helpers;

namespace WowArmory.Core.BattleNet.Models
{
	[Flags]
	public enum CharacterFields
	{
		[ApiUrlField(Name = "")]
		Basic = 1,
		[ApiUrlField(Name = "guild")]
		Guild = 2,
		[ApiUrlField(Name = "stats")]
		Stats = 4,
		[ApiUrlField(Name = "talents")]
		Talents = 8,
		[ApiUrlField(Name = "items")]
		Items = 16,
		[ApiUrlField(Name = "reputation")]
		Reputation = 32,
		[ApiUrlField(Name = "titles")]
		Titles = 64,
		[ApiUrlField(Name = "professions")]
		Professions = 128,
		[ApiUrlField(Name = "appearance")]
		Appearance = 256,
		[ApiUrlField(Name = "companions")]
		Companions = 512,
		[ApiUrlField(Name = "mounts")]
		Mounts = 1024,
		[ApiUrlField(Name = "pets")]
		Pets = 2048,
		[ApiUrlField(Name = "achievements")]
		Achievements = 4096,
		[ApiUrlField(Name = "progression")]
		Progression = 8192,
		[ApiUrlField(Name = "pvp")]
		Pvp = 16384,
		[ApiUrlField(Name = "quests")]
		Quests = 32768,
		All = Basic | Guild | Stats | Talents | Items | Reputation | Titles | Professions | Appearance | Companions | Mounts | Pets | Achievements | Progression | Pvp | Quests
	}
}