using System;

namespace RadarSoft.XmlaClient.Metadata
{
    [Flags]
    public enum HierarchyOrigin
    {
        UserHierarchy = 0x1,
        AttributeHierarchy = 0x2,
        ParentChildHierarchy = 0x4
    }
}