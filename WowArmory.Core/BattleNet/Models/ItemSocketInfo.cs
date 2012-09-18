using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class ItemSocketInfo
	{
		[DataMember]
		[XmlArray("sockets")]
		public List<ItemSocket> Sockets { get; set; }
        [DataMember]
        [XmlElement("socketBonus")]
        public string SocketBonus { get; set; }
	}
}