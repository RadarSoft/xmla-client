using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("row", Namespace = Namespace.ComMicrosoftSchemasXmlaRowset)]
    public class LevelProperty : IXmlaBaseObject
    {
        [XmlElement("PROPERTY_NAME")]
        public string Caption { get; set; }

        [XmlElement("DESCRIPTION")]
        public string Description { get; set; }

        [XmlElement("PROPERTY_ATTRIBUTE_HIERARCHY_NAME")]
        public string Name { get; set; }

        public Level ParentLevel => ParentObject as Level;

        public string UniqueName => string.Format(ParentLevel.UniqueName + ".[{0}]", Name);

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