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
		[XmlElement("name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement("icon")]
		public string Icon { get; set; }
		[DataMember]
		[XmlElement("quality")]
		public int Quality { get; set; }
		[DataMember]
		[XmlElement("tooltipParams", IsNullable = true)]
		public ItemTooltipParams TooltipParams { get; set; }
	}
}