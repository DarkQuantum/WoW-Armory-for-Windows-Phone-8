using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class Item
	{
		[DataMember]
		[XmlElement("id")]
		public int Id { get; set; }
		[DataMember]
		[XmlElement("description")]
		public string Description { get; set; }
		[DataMember]
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("icon")]
		public string Icon { get; set; }
		[DataMember]
		[XmlElement("stackable")]
		public int Stackable { get; set; }
		[DataMember]
		[XmlArray("allowableClasses", IsNullable = true)]
		public List<CharacterClass> AllowableClasses { get; set; }
		[DataMember]
		[XmlElement("itemBind")]
		public ItemBinding ItemBind { get; set; }
		[DataMember]
		[XmlArray("bonusStats", IsNullable = true)]
		public List<ItemBonusStat> BonusStats { get; set; }
		[DataMember]
		[XmlArray("itemSpells", IsNullable = true)]
		public List<ItemSpell> ItemSpells { get; set; }
		[DataMember]
		[XmlElement("buyPrice")]
		public int BuyPrice { get; set; }
		public Price BuyPriceObject { get { return BuyPrice; } }
		[DataMember]
		[XmlElement("itemClass")]
		public int ItemClass { get; set; }
		[DataMember]
		[XmlElement("itemSubClass")]
		public int ItemSubClass { get; set; }
		[DataMember]
		[XmlElement("containerSlots")]
		public int ContainerSlots { get; set; }
		[DataMember]
		[XmlElement("weaponInfo", IsNullable = true)]
		public ItemWeaponInfo WeaponInfo { get; set; }
		[DataMember]
		[XmlElement("inventoryType")]
		public int InventoryType { get; set; }
		[DataMember]
		[XmlElement("Equippable")]
		public bool Equippable { get; set; }
		[DataMember]
		[XmlElement("itemLevel")]
		public int ItemLevel { get; set; }
		[DataMember]
		[XmlElement("itemSet", IsNullable = true)]
		public string ItemSet { get; set; }
		[DataMember]
		[XmlElement("maxCount")]
		public int MaxCount { get; set; }
		[DataMember]
		[XmlElement("maxDurability")]
		public int MaxDurability { get; set; }
		[DataMember]
		[XmlElement("minFactionId")]
		public int MinFactionId { get; set; }
		[DataMember]
		[XmlElement("minReputation")]
		public int MinReputation { get; set; }
		[DataMember]
		[XmlElement("quality")]
		public ItemQuality Quality { get; set; }
		[DataMember]
		[XmlElement("sellPrice")]
		public int SellPrice { get; set; }
		public Price SellPriceObject { get { return SellPrice; } }
		[DataMember]
		[XmlElement("requiredSkill")]
		public int RequiredSkill { get; set; }
		[DataMember]
		[XmlElement("requiredLevel")]
		public int RequiredLevel { get; set; }
		[DataMember]
		[XmlElement("requiredSkillRank")]
		public int RequiredSkillRank { get; set; }
		[DataMember]
		[XmlElement("socketInfo", IsNullable = true)]
		public ItemSocketInfo SocketInfo { get; set; }
		[DataMember]
		[XmlElement("itemSource", IsNullable = true)]
		public ItemSource ItemSource { get; set; }
		[DataMember]
		[XmlElement("baseArmor")]
		public int BaseArmor { get; set; }
		[DataMember]
		[XmlElement("hasSockets")]
		public bool HasSockets { get; set; }
		[DataMember]
		[XmlElement("isAuctionable")]
		public bool IsAuctionable { get; set; }
	}
}