using System;

namespace RadarSoft.XmlaClient.Metadata
{
    [Flags]
    public enum PropertyType
    {
        MDPROP_MEMBER = 1,
        MDPROP_CELL = 2,
        MDPROP_SYSTEM = 4,
        MDPROP_BLOB = 8
    }
}