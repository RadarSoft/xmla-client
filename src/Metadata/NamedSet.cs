using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("row", Namespace = Namespace.ComMicrosoftSchemasXmlaRowset)]
    public class NamedSet : IXmlaBaseObject
    {
        private PropertyCollection _Properties;

        [XmlElement("SET_CAPTION")]
        public string Caption { get; set; }

        [XmlElement("DESCRIPTION")]
        public string Description { get; set; }

        [XmlElement("SET_DISPLAY_FOLDER")]
        public string DisplayFolder { get; set; }

        [XmlElement("EXPRESSION")]
        public string Expression { get; set; }

        [XmlElement("SET_NAME")]
        public string Name { get; set; }

        public CubeDef ParentCube { get; }

        [XmlIgnore]
        public PropertyCollection Properties
        {
            get
            {
                if (_Properties == null)
                {
                    _Properties = new PropertyCollection();
                    _Properties.SoapRow = SoapRow;
                }
                return _Properties;
            }
        }

        [XmlIgnore]
        public XmlaConnection Connection { get; set; }

        [XmlIgnore]
        public bool IsMetadata { get; set; }
        [XmlIgnore]
        public object MetadataData { get; set; }
        [XmlIgnore]
        public IXmlaBaseObject ParentObject { get; set; }
        [XmlIgnore]
        public SchemaObjectType SchemaObjectType { get; set; }
        [XmlIgnore]
        public XElement SoapRow { get; set; }

        [XmlIgnore]
        public string CubeName { get; set; }
    }
}