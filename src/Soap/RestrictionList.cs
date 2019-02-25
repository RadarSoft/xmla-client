using System.ComponentModel;
using System.Xml.Serialization;
using RadarSoft.XmlaClient.Metadata;

namespace RadarSoft.XmlaClient.Soap
{
    [XmlRoot("RestrictionList")]
    public class RestrictionList
    {
        public RestrictionList()
        {
            SchemaName = "";
            CubeName = "";
            DimensionUniqueName = "";
            HierarchyUniqueName = "";
            LevelUniqueName = "";
            PropertyName = "";
            Coordinate = "";
            CoordinateType = CoordinateType.None;
            PropertyTypeSerializer = 0;
        }

        [XmlElement("SCHEMA_NAME")]
        [DefaultValue("")]
        public string SchemaName { get; set; }

        [XmlElement("CUBE_NAME")]
        [DefaultValue("")]
        public string CubeName { get; set; }

        [XmlElement("MEASURE_UNIQUE_NAME")]
        [DefaultValue("")]
        public string MeasureUniqueName { get; set; }

        [XmlElement("DIMENSION_UNIQUE_NAME")]
        [DefaultValue("")]
        public string DimensionUniqueName { get; set; }

        [XmlElement("HIERARCHY_UNIQUE_NAME")]
        [DefaultValue("")]
        public string HierarchyUniqueName { get; set; }

        [XmlElement("LEVEL_UNIQUE_NAME")]
        [DefaultValue("")]
        public string LevelUniqueName { get; set; }

        [XmlElement("PROPERTY_NAME")]
        [DefaultValue("")]
        public string PropertyName { get; set; }

        [XmlElement("COORDINATE")]
        [DefaultValue("")]
        public string Coordinate { get; set; }

        [XmlElement("COORDINATE_TYPE")]
        [DefaultValue(CoordinateType.None)]
        public CoordinateType CoordinateType { get; set; }

        [XmlElement("PROPERTY_TYPE")]
        [DefaultValue(0)]
        public int PropertyTypeSerializer
        {
            get => (int) PropertyType;
            set => PropertyType = (PropertyType) value;
        }

        [XmlIgnore]
        public PropertyType PropertyType { get; set; }
    }
}