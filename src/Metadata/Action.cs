using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("row", Namespace = Namespace.ComMicrosoftSchemasXmlaRowset)]
    public class Action : IXmlaBaseObject
    {
        [XmlElement("ACTION_NAME")]
        public string Name { get; set; }

        [XmlElement("ACTION_CAPTION")]
        public string Caption { get; set; }

        [XmlElement("DESCRIPTION")]
        public string Description { get; set; }

        [XmlElement("CONTENT")]
        public string Content { get; set; }

        [XmlElement("APPLICATION")]
        public string Application { get; set; }

        [XmlIgnore]
        public ActionType ActionType { get; set; }

        [XmlElement("ACTION_TYPE")]
        public int ActionTypeSerializer
        {
            get => (int) ActionType;
            set => ActionType = (ActionType) value;
        }

        [XmlIgnore]
        public object Value { get; set; }

        [XmlIgnore]
        public CellPropertyCollection CellProperties { get; }
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