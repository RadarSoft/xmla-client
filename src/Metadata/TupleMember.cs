using System.Xml.Linq;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("Member", Namespace = Namespace.ComMicrosoftSchemasXmlaMddataset)]
    public class TypleMember : IXmlaBaseObject
    {
        [XmlElement("Caption")]
        public string Caption { get; set; }

        [XmlElement("UName")]
        public string UniqueName { get; set; }

        [XmlElement("LName")]
        public string LevelName { get; set; }

        [XmlAttribute("Hierarchy")]
        public string Hierarchy { get; set; }

        [XmlIgnore]
        public XmlaConnection Connection { get; set; }
        [XmlIgnore]
        public string CubeName { get; set; }
        [XmlIgnore]
        public bool IsMetadata { get; set; }
        [XmlIgnore]
        public IXmlaBaseObject ParentObject { get; set; }
        [XmlIgnore]
        public SchemaObjectType SchemaObjectType { get; set; }
        [XmlIgnore]
        public XElement SoapRow { get; set; }
    }
}