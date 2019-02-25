using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    public enum MemberTypeEnum
    {
        [XmlEnum("0")] Unknown = 0,
        [XmlEnum("1")] Regular = 1,
        [XmlEnum("2")] All = 2,
        [XmlEnum("3")] Measure = 3,
        [XmlEnum("4")] Formula = 4
    }
}