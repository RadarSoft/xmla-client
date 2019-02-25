using System;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    internal interface IMetadataObject
    {
        string Catalog { get; }
        XmlaConnection Connection { get; }
        string CubeName { get; }
        string SessionId { get; }
        Type Type { get; }
        string UniqueName { get; }
    }
}