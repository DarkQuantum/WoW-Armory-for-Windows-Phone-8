using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WowArmory.Core.BattleNet.Models
{
	[DataContract]
	public class RealmList : ApiResponse
	{
		[DataMember]
		[XmlArray("realms")]
		public List<Realm> Realms { get; set; }
	}
}