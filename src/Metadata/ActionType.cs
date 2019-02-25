namespace RadarSoft.XmlaClient.Metadata
{
    public enum ActionType
    {
        Url = 0x01,
        Html = 0x02,
        Statement = 0x04,
        DataSet = 0x08,
        Rowset = 0x10,
        Commandline = 0x20,
        Proprietary = 0x40,
        Report = 0x80,
        Drillthrough = 0x100
    }
}