using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("row", Namespace = Namespace.ComMicrosoftSchemasXmlaRowset)]
    public class Kpi : IXmlaBaseObject
    {
        private PropertyCollection _Properties;

        [XmlElement("KPI_CAPTION")]
        public string Caption { get; set; }

        [XmlElement("KPI_DESCRIPTION")]
        public string Description { get; set; }

        [XmlElement("KPI_DISPLAY_FOLDER")]
        public string DisplayFolder { get; set; }

        [XmlElement("KPI_NAME")]
        public string Name { get; set; }

        [XmlElement("KPI_STATUS_GRAPHIC")]
        public string StatusGraphic { get; set; }

        [XmlElement("KPI_TREND_GRAPHIC")]
        public string TrendGraphic { get; set; }

        public CubeDef ParentCube { get; }
        public Kpi ParentKpi { get; }

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