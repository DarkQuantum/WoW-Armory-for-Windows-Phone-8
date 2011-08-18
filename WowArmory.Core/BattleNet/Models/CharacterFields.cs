using System;
using System.Runtime.Serialization;
using WowArmory.Core.BattleNet.Helpers;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	[Flags]
	public enum CharacterFields
	{
		[EnumMember]
		[ApiUrlField(Name = "")]
		Basic = 1,
		[EnumMember]
		[ApiUrlField(Name = "guild")]
		Guild = 2,
		[EnumMember]
		[ApiUrlField(Name = "stats")]
		Stats = 4,
		[EnumMember]
		[ApiUrlField(Name = "talents")]
		Talents = 8,
		[EnumMember]
		[ApiUrlField(Name = "items")]
		Items = 16,
		[EnumMember]
		[ApiUrlField(Name = "reputation")]
		Reputation = 32,
		[EnumMember]
		[ApiUrlField(Name = "titles")]
		Titles = 64,
		[EnumMember]
		[ApiUrlField(Name = "professions")]
		Professions = 128,
		[EnumMember]
		[ApiUrlField(Name = "appearance")]
		Appearance = 256,
		[EnumMember]
		[ApiUrlField(Name = "companions")]
		Companions = 512,
		[EnumMember]
		[ApiUrlField(Name = "mounts")]
		Mounts = 1024,
		[EnumMember]
		[ApiUrlField(Name = "pets")]
		Pets = 2048,
		[EnumMember]
		[ApiUrlField(Name = "achievements")]
		Achievements = 4096,
		[EnumMember]
		[ApiUrlField(Name = "progression")]
		Progression = 8192,
		[EnumMember]
		[ApiUrlField(Name = "pvp")]
		Pvp = 16384,
		[EnumMember]
		[ApiUrlField(Name = "quests")]
		Quests = 32768,
		[EnumMember]
		All = Basic | Guild | Stats | Talents | Items | Reputation | Titles | Professions | Appearance | Companions | Mounts | Pets | Achievements | Progression | Pvp | Quests
	}
}