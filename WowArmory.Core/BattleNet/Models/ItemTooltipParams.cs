using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemTooltipParams
	{
		[DataMember]
		[XmlElement("gem0")]
		public int Gem0 { get; set; }
		[DataMember]
		[XmlElement("gem1")]
		public int Gem1 { get; set; }
		[DataMember]
		[XmlElement("gem2")]
		public int Gem2 { get; set; }
		[DataMember]
		[XmlElement("gem3")]
		public int Gem3 { get; set; }
		[DataMember]
		[XmlElement("extraSocket")]
		public bool ExtraSocket { get; set; }
		[DataMember]
		[XmlElement("enchant")]
		public int Enchant { get; set; }
		[DataMember]
		[XmlElement("reforge")]
		public int Reforge { get; set; }
		[DataMember]
		[XmlElement("tinker")]
		public int Tinker { get; set; }
		[DataMember]
		[XmlArray("set")]
		public List<int> Set { get; set; }
	}
}