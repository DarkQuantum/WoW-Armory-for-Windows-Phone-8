using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
    [DataContract]
    public class ItemSetDetails
    {
        [DataMember]
        [XmlElement("id")]
        public int id { get; set; }
        [DataMember]
        [XmlElement("name")]
        public string Name { get; set; }
        [DataMember]
        [XmlArray("setBonuses", IsNullable = true)]
        public List<SetBonus> SetBonuses { get; set; }
    }
}