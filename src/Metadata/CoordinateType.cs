using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    public enum CoordinateType
    {
        [XmlEnum("0")] None = 0,
        [XmlEnum("1")] Cube = 1,
        [XmlEnum("2")] Dimension = 2,
        [XmlEnum("3")] Level = 3,
        [XmlEnum("4")] Member = 4,
        [XmlEnum("5")] Set = 5,
        [XmlEnum("6")] Cell = 6
    }
}