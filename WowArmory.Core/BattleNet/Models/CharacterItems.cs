using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class CharacterItems
	{
		[DataMember]
		[XmlElement("averageItemLevel")]
		public int AverageItemLevel { get; set; }
		[DataMember]
		[XmlElement("averageItemLevelEquipped")]
		public int AverageItemLevelEquipped { get; set; }
		[DataMember]
		[XmlElement("head", IsNullable = true)]
		public CharacterItem Head { get; set; }
		[DataMember]
		[XmlElement("neck", IsNullable = true)]
		public CharacterItem Neck { get; set; }
		[DataMember]
		[XmlElement("shoulder", IsNullable = true)]
		public CharacterItem Shoulder { get; set; }
		[DataMember]
		[XmlElement("back", IsNullable = true)]
		public CharacterItem Back { get; set; }
		[DataMember]
		[XmlElement("chest", IsNullable = true)]
		public CharacterItem Chest { get; set; }
		[DataMember]
		[XmlElement("shirt", IsNullable = true)]
		public CharacterItem Shirt { get; set; }
		[DataMember]
		[XmlElement("tabard", IsNullable = true)]
		public CharacterItem Tabard { get; set; }
		[DataMember]
		[XmlElement("wrist", IsNullable = true)]
		public CharacterItem Wrist { get; set; }
		[DataMember]
		[XmlElement("hands", IsNullable = true)]
		public CharacterItem Hands { get; set; }
		[DataMember]
		[XmlElement("waist", IsNullable = true)]
		public CharacterItem Waist { get; set; }
		[DataMember]
		[XmlElement("legs", IsNullable = true)]
		public CharacterItem Legs { get; set; }
		[DataMember]
		[XmlElement("feet", IsNullable = true)]
		public CharacterItem Feet { get; set; }
		[DataMember]
		[XmlElement("finger1", IsNullable = true)]
		public CharacterItem Finger1 { get; set; }
		[DataMember]
		[XmlElement("finger2", IsNullable = true)]
		public CharacterItem Finger2 { get; set; }
		[DataMember]
		[XmlElement("trinket1", IsNullable = true)]
		public CharacterItem Trinket1 { get; set; }
		[DataMember]
		[XmlElement("trinket2", IsNullable = true)]
		public CharacterItem Trinket2 { get; set; }
		[DataMember]
		[XmlElement("mainHand", IsNullable = true)]
		public CharacterItem MainHand { get; set; }
		[DataMember]
		[XmlElement("offHand", IsNullable = true)]
		public CharacterItem OffHand { get; set; }
		[DataMember]
		[XmlElement("ranged", IsNullable = true)]
		public CharacterItem Ranged { get; set; }
	}
}