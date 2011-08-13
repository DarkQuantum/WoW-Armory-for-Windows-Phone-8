using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemSpell
	{
		[DataMember]
		[XmlElement("spellId")]
		public int SpellId { get; set; }
		[DataMember]
		[XmlElement("spell")]
		public ItemSpellDetails Spell { get; set; }
		[DataMember]
		[XmlElement("nCharges")]
		public int NCharges { get; set; }
		[DataMember]
		[XmlElement("consumable")]
		public bool Consumable { get; set; }
		[DataMember]
		[XmlElement("categoryId")]
		public int CategoryId { get; set; }
	}
}