using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    public enum CubeType
    {
        [XmlEnum("UNKNOWN")] Unknown = 0,
        [XmlEnum("CUBE")] Cube = 1,
        [XmlEnum("DIMENSION")] Dimension = 2
    }
}