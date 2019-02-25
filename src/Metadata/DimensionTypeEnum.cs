using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    public enum DimensionTypeEnum
    {
        [XmlEnum("0")] Unknown = 0,
        [XmlEnum("1")] Time = 1,
        [XmlEnum("2")] Measure = 2,
        [XmlEnum("3")] Other = 3,
        [XmlEnum("5")] Quantitative = 5,
        [XmlEnum("6")] Accounts = 6,
        [XmlEnum("7")] Customers = 7,
        [XmlEnum("8")] Products = 8,
        [XmlEnum("9")] Scenario = 9,
        [XmlEnum("10")] Utility = 10,
        [XmlEnum("11")] Currency = 11,
        [XmlEnum("12")] Rates = 12,
        [XmlEnum("13")] Channel = 13,
        [XmlEnum("14")] Promotion = 14,
        [XmlEnum("15")] Organization = 15,
        [XmlEnum("16")] BillOfMaterials = 16,
        [XmlEnum("17")] Geography = 17
    }
}