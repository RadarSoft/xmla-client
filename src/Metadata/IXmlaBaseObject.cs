using System.Xml.Linq;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    public interface IXmlaBaseObject
    {
        XmlaConnection Connection { get; set; }
        string CubeName { get; set; }
        bool IsMetadata { get; set; }
        IXmlaBaseObject ParentObject { get; set; }
        SchemaObjectType SchemaObjectType { get; set; }
        XElement SoapRow { get; set; }
    }
}