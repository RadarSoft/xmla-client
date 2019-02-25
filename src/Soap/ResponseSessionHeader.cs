using System.Xml.Serialization;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Soap
{
    [XmlRoot("Session", Namespace = Namespace.ComMicrosoftSchemasXmla)]
    public class ResponseSessionHeader : SoapHeader
    {
        [XmlAttribute("SessionId")]
        public string SessionId { get; set; }
    }
}