using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("Cube", Namespace = Namespace.ComMicrosoftSchemasXmlaMddataset)]
    public class OlapInfoCube : IXmlaBaseObject
    {
        [XmlIgnore]
        public XmlaConnection Connection { get; set; }

        [XmlElement("CubeName")]
        public string CubeName { get; set; }

        [XmlIgnore]
        public bool IsMetadata { get; set; }
        [XmlIgnore]
        public IXmlaBaseObject ParentObject { get; set; }
        [XmlIgnore]
        public SchemaObjectType SchemaObjectType { get; set; }
        [XmlIgnore]
        public XElement SoapRow { get; set; }

        [XmlElement("LastDataUpdate", Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
        public DateTime LastDataUpdate { get; set; }

        [XmlElement("LastSchemaUpdate", Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
        public DateTime LastSchemaUpdate { get; set; }
    }
}