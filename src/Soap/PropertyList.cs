using System.ComponentModel;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Soap
{
    [XmlRoot("PropertyList")]
    public class PropertyList
    {
        public PropertyList()
        {
            Format = "Tabular";
            Catalog = "";
            ShowHiddenCubes = true;
            Content = "SchemaData";
        }

        [XmlElement("DataSourceInfo")]
        [DefaultValue("")]
        public string DataSourceInfo { get; set; }

        [XmlElement("Catalog")]
        [DefaultValue("")]
        public string Catalog { get; set; }

        [XmlElement("AxisFormat")]
        [DefaultValue("")]
        public string AxisFormat { get; set; }

        [XmlElement("Format")]
        [DefaultValue("")]
        public string Format { get; set; }

        [XmlElement("ShowHiddenCubes")]
        public bool ShowHiddenCubes { get; set; }

        [XmlElement("Content")]
        [DefaultValue("")]
        public string Content { get; set; }
    }
}