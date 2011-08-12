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
		public Item Head { get; set; }
		[DataMember]
		[XmlElement("neck", IsNullable = true)]
		public Item Neck { get; set; }
		[DataMember]
		[XmlElement("shoulder", IsNullable = true)]
		public Item Shoulder { get; set; }
		[DataMember]
		[XmlElement("back", IsNullable = true)]
		public Item Back { get; set; }
		[DataMember]
		[XmlElement("chest", IsNullable = true)]
		public Item Chest { get; set; }
		[DataMember]
		[XmlElement("shirt", IsNullable = true)]
		public Item Shirt { get; set; }
		[DataMember]
		[XmlElement("tabard", IsNullable = true)]
		public Item Tabard { get; set; }
		[DataMember]
		[XmlElement("wrist", IsNullable = true)]
		public Item Wrist { get; set; }
		[DataMember]
		[XmlElement("hands", IsNullable = true)]
		public Item Hands { get; set; }
		[DataMember]
		[XmlElement("waist", IsNullable = true)]
		public Item Waist { get; set; }
		[DataMember]
		[XmlElement("legs", IsNullable = true)]
		public Item Legs { get; set; }
		[DataMember]
		[XmlElement("feet", IsNullable = true)]
		public Item Feet { get; set; }
		[DataMember]
		[XmlElement("finger1", IsNullable = true)]
		public Item Finger1 { get; set; }
		[DataMember]
		[XmlElement("finger2", IsNullable = true)]
		public Item Finger2 { get; set; }
		[DataMember]
		[XmlElement("trinket1", IsNullable = true)]
		public Item Trinket1 { get; set; }
		[DataMember]
		[XmlElement("trinket2", IsNullable = true)]
		public Item Trinket2 { get; set; }
		[DataMember]
		[XmlElement("mainHand", IsNullable = true)]
		public Item MainHand { get; set; }
		[DataMember]
		[XmlElement("offHand", IsNullable = true)]
		public Item OffHand { get; set; }
		[DataMember]
		[XmlElement("ranged", IsNullable = true)]
		public Item Ranged { get; set; }
	}
}