using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class CharacterStats
	{
		[DataMember]
		[XmlElement("health")]
		public int Health { get; set; }
		[DataMember]
		[XmlElement("powerType")]
		public string PowerType { get; set; }
		[DataMember]
		[XmlElement("power")]
		public int Power { get; set; }
		[DataMember]
		[XmlElement("str")]
		public int Str { get; set; }
		[DataMember]
		[XmlElement("agi")]
		public int Agi { get; set; }
		[DataMember]
		[XmlElement("sta")]
		public int Sta { get; set; }
		[DataMember]
		[XmlElement("int")]
		public int Int { get; set; }
		[DataMember]
		[XmlElement("spr")]
		public int Spr { get; set; }
		[DataMember]
		[XmlElement("attackPower")]
		public int AttackPower { get; set; }
		[DataMember]
		[XmlElement("rangedAttackPower")]
		public int RangedAttackPower { get; set; }
		[DataMember]
		[XmlElement("mastery")]
		public float Mastery { get; set; }
		[DataMember]
		[XmlElement("masteryRating")]
		public int MasteryRating { get; set; }
		[DataMember]
		[XmlElement("crit")]
		public float Crit { get; set; }
		[DataMember]
		[XmlElement("critRating")]
		public int CritRating { get; set; }
		[DataMember]
		[XmlElement("hitPercent")]
		public float HitPercent { get; set; }
		[DataMember]
		[XmlElement("hitRating")]
		public int HitRating { get; set; }
		[DataMember]
		[XmlElement("hasteRating")]
		public int HasteRating { get; set; }
		[DataMember]
		[XmlElement("expertiseRating")]
		public int ExpertiseRating { get; set; }
		[DataMember]
		[XmlElement("spellPower")]
		public int SpellPower { get; set; }
		[DataMember]
		[XmlElement("spellPen")]
		public int SpellPen { get; set; }
		[DataMember]
		[XmlElement("spellCrit")]
		public float SpellCrit { get; set; }
		[DataMember]
		[XmlElement("spellCritRating")]
		public int SpellCritRating { get; set; }
		[DataMember]
		[XmlElement("spellHitPercent")]
		public float SpellHitPercent { get; set; }
		[DataMember]
		[XmlElement("spellHitRating")]
		public int SpellHitRating { get; set; }
		[DataMember]
		[XmlElement("mana5")]
		public int Mana5 { get; set; }
		[DataMember]
		[XmlElement("mana5Combat")]
		public int Mana5Combat { get; set; }
		[DataMember]
		[XmlElement("armor")]
		public int Armor { get; set; }
		[DataMember]
		[XmlElement("dodge")]
		public float Dodge { get; set; }
		[DataMember]
		[XmlElement("dodgeRating")]
		public int DodgeRating { get; set; }
		[DataMember]
		[XmlElement("parry")]
		public float Parry { get; set; }
		[DataMember]
		[XmlElement("parryRating")]
		public int ParryRating { get; set; }
		[DataMember]
		[XmlElement("block")]
		public float Block { get; set; }
		[DataMember]
		[XmlElement("blockRating")]
		public int BlockRating { get; set; }
		[DataMember]
		[XmlElement("resil")]
		public int Resil { get; set; }
		[DataMember]
		[XmlElement("mainHandDmgMin")]
		public int MainHandDmgMin { get; set; }
		[DataMember]
		[XmlElement("mainHandDmgMax")]
		public int MainHandDmgMax { get; set; }
		[DataMember]
		[XmlElement("mainHandSpeed")]
		public float MainHandSpeed { get; set; }
		[DataMember]
		[XmlElement("mainHandDps")]
		public float MainHandDps { get; set; }
		[DataMember]
		[XmlElement("mainHandExpertise")]
		public int MainHandExpertise { get; set; }
		[DataMember]
		[XmlElement("offHandDmgMin")]
		public int OffHandDmgMin { get; set; }
		[DataMember]
		[XmlElement("offHandDmgMax")]
		public int OffHandDmgMax { get; set; }
		[DataMember]
		[XmlElement("offHandSpeed")]
		public float OffHandSpeed { get; set; }
		[DataMember]
		[XmlElement("offHandDps")]
		public float OffHandDps { get; set; }
		[DataMember]
		[XmlElement("offHandExpertise")]
		public int OffHandExpertise { get; set; }
		[DataMember]
		[XmlElement("rangedDmgMin")]
		public int RangedDmgMin { get; set; }
		[DataMember]
		[XmlElement("rangedDmgMax")]
		public int RangedDmgMax { get; set; }
		[DataMember]
		[XmlElement("rangedSpeed")]
		public float RangedSpeed { get; set; }
		[DataMember]
		[XmlElement("rangedDps")]
		public float RangedDps { get; set; }
		[DataMember]
		[XmlElement("RangedCrit")]
		public float RangedCrit { get; set; }
		[DataMember]
		[XmlElement("rangedCritRating")]
		public int RangedCritRating { get; set; }
		[DataMember]
		[XmlElement("rangedHitPercent")]
		public float RangedHitPercent { get; set; }
		[DataMember]
		[XmlElement("rangedHitRating")]
		public int RangedHitRating { get; set; }
	}
}