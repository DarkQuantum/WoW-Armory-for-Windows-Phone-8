using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class CharacterItem
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
		public ItemQuality Quality { get; set; }
		public int QualityAsInt
		{
			get
			{
				return (int)Quality;
			}
		}
		[DataMember]
		[XmlElement("tooltipParams", IsNullable = true)]
		public ItemTooltipParams TooltipParams { get; set; }
	}
}