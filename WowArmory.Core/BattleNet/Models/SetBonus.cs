using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
    [DataContract]
    public class SetBonus
    {
        [DataMember]
        [XmlElement("description")]
        public string Description { get; set; }
        [DataMember]
        [XmlElement("threshold")]
        public int Threshold { get; set; }
    }
}