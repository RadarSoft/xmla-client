using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("row", Namespace = Namespace.ComMicrosoftSchemasXmlaRowset)]
    public class Member : IXmlaBaseObject
    {
        public Member()
        {

        }

        private MemberPropertyCollection _MemberProperties;

        private PropertyCollection _Properties;

        [XmlElement("MEMBER_CAPTION")]
        public string Caption { get; set; }

        [XmlElement("MEMBER_NAME")]
        public string Name { get; set; }

        [XmlElement("MEMBER_UNIQUE_NAME")]
        public string UniqueName { get; set; }

        [XmlElement("MEMBER_TYPE")]
        public MemberTypeEnum Type { get; set; }

        [XmlElement("LEVEL_UNIQUE_NAME")]
        public string LevelName { get; set; }

        public long ChildCount { get; }
        public string Description { get; }
        public bool DrilledDown { get; }
        public int LevelDepth { get; }
        public Member Parent { get; }
        public bool ParentSameAsPrevious { get; }

        [XmlIgnore]
        public Level ParentLevel { get; internal set; }

        [XmlIgnore]
        public MemberPropertyCollection MemberProperties
        {
            get
            {
                if (_MemberProperties == null)
                {
                    _MemberProperties = new MemberPropertyCollection();
                    _MemberProperties.SoapRow = SoapRow;
                }
                return _MemberProperties;
            }
        }

        [XmlIgnore]
        public PropertyCollection Properties
        {
            get
            {
                if (_Properties == null)
                {
                    _Properties = new PropertyCollection();
                    _Properties.SoapRow = SoapRow;
                }
                return _Properties;
            }
        }

        [XmlIgnore]
        public XmlaConnection Connection { get; internal set; }

        string IXmlaBaseObject.CubeName
        {
            get { return CubeName; }
            set { CubeName = value; }
        }

        XmlaConnection IXmlaBaseObject.Connection
        {
            get { return Connection; }
            set { Connection = value; }
        }

        [XmlIgnore]
        public string CubeName { get; internal set; }

        [XmlIgnore]
        public bool IsMetadata
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        [XmlIgnore]
        public IXmlaBaseObject ParentObject { get; set; }
        [XmlIgnore]
        public SchemaObjectType SchemaObjectType { get; set; }

        [XmlIgnore]
        public object MetadataData
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        [XmlIgnore]
        public XElement SoapRow { get; set; }

        public void FetchAllProperties()
        {
            throw new NotImplementedException();
        }

        public MemberCollection GetChildren()
        {
            throw new NotImplementedException();
        }

        public MemberCollection GetChildren(long start, long count)
        {
            throw new NotImplementedException();
        }

        public MemberCollection GetChildren(long start, long count, params MemberFilter[] filters)
        {
            throw new NotImplementedException();
        }

        public MemberCollection GetChildren(long start, long count, string[] properties, params MemberFilter[] filters)
        {
            throw new NotImplementedException();
        }
    }
}