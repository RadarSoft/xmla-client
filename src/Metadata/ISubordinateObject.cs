using System;

namespace RadarSoft.XmlaClient.Metadata
{
    internal interface ISubordinateObject
    {
        int Ordinal { get; }
        object Parent { get; }
        Type Type { get; }
    }
}