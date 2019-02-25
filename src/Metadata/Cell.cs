using System;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("Cell", Namespace = Namespace.ComMicrosoftSchemasXmlaMddataset)]
    public class Cell : IXmlaBaseObject
    {
        private CellPropertyCollection _CellProperties;

        [XmlElement("FmtValue")]
        public string FormattedValue { get; set; }

        [XmlIgnore]
        public object Value { get; set; }

        [XmlIgnore]
        public CellPropertyCollection CellProperties
        {
            get
            {
                if (_CellProperties == null)
                    _CellProperties = new CellPropertyCollection();
                return _CellProperties;
            }
        }

        internal void InitCell(XElement xcell)
        {
            var xval = xcell.Element(XName.Get("Value", Namespace.ComMicrosoftSchemasXmlaMddataset));
            var val = xval?.Value;
            var type = xval?.Attribute(XName.Get("type", "http://www.w3.org/2001/XMLSchema-instance"))?.Value;

            try
            {
                switch (type?.ToLower())
                {
                    case "xsd:int":
                        Value = int.Parse(val);
                        break;
                    case "xsd:double":
                    case "xsd:float":
                        Value = Decimal.Parse(val, NumberStyles.Float | NumberStyles.AllowExponent, CultureInfo.InvariantCulture);
                        break;
                    case "xsd:string":
                        Value = val;
                        break;
                    case "xsd:datetime":
                        Value = DateTime.Parse(val);
                        break;
                    case "xsd:boolean":
                        Value = bool.Parse(val);
                        break;
                    default:
                        Value = int.Parse(val);
                        break;
                }
            }
            catch
            {
                Value = val;
            }
        }

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