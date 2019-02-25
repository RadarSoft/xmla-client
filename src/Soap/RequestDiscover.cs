using System.Data.Common;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Soap
{
    [XmlRoot("Discover", Namespace = Namespace.ComMicrosoftSchemasXmla)]
    public class RequestDiscover
    {
        public RequestDiscover()
        {
        }

        public RequestDiscover(string requestType, DbConnection connection, RestrictionList restrictions,
            PropertyList properties)
        {
            RequestType = requestType;
            Properties = new Properties(properties);
            Restrictions = new Restrictions(restrictions);
        }

        [XmlElement("RequestType")]
        public string RequestType { get; set; }

        [XmlElement("Restrictions")]
        public Restrictions Restrictions { get; set; }

        [XmlElement("Properties")]
        public Properties Properties { get; set; }
    }
}