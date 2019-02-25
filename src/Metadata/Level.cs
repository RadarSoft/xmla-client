using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("row", Namespace = Namespace.ComMicrosoftSchemasXmlaRowset)]
    public class Level : IXmlaBaseObject
    {
        private MemberCollection _AllMembers;

        private LevelPropertyCollection _LevelProperties;

        private PropertyCollection _Properties;

        [XmlElement("LEVEL_CAPTION")]
        public string Caption { get; set; }

        [XmlElement("DESCRIPTION")]
        public string Description { get; set; }

        [XmlElement("LEVEL_NUMBER")]
        public int LevelNumber { get; set; }

        [XmlIgnore]
        public LevelTypeEnum LevelType { get; set; }

        [XmlElement("LEVEL_TYPE")]
        public int LevelTypeSerializer
        {
            get => (int) LevelType;
            set => LevelType = (LevelTypeEnum) value;
        }

        [XmlElement("")]
        public long MemberCount { get; set; }

        [XmlElement("LEVEL_NAME")]
        public string Name { get; set; }

        public Hierarchy ParentHierarchy => ParentObject as Hierarchy;

        [XmlIgnore]
        public LevelPropertyCollection LevelProperties
        {
            get
            {
                if (_LevelProperties == null)
                {
                    var command = new XmlaCommand("MDSCHEMA_PROPERTIES", Connection);
                    command.CommandRestrictions.CubeName = CubeName;
                    command.CommandRestrictions.DimensionUniqueName = ParentHierarchy.ParentDimension.UniqueName;
                    command.CommandRestrictions.HierarchyUniqueName = ParentHierarchy.UniqueName;
                    command.CommandRestrictions.LevelUniqueName = UniqueName;
                    command.CommandRestrictions.PropertyType = PropertyType.MDPROP_MEMBER;
                    var response = command.Execute() as SoapEnvelope;
                    _LevelProperties = new LevelPropertyCollection(this, response.GetXRows());                    
                }

                return _LevelProperties;
            }
        }

        public async Task<LevelPropertyCollection> GetLevelPropertiesAsync()
        {
            if (_LevelProperties == null)
            {
                var command = new XmlaCommand("MDSCHEMA_PROPERTIES", Connection);
                command.CommandRestrictions.CubeName = CubeName;
                command.CommandRestrictions.DimensionUniqueName = ParentHierarchy.ParentDimension.UniqueName;
                command.CommandRestrictions.HierarchyUniqueName = ParentHierarchy.UniqueName;
                command.CommandRestrictions.LevelUniqueName = UniqueName;
                command.CommandRestrictions.PropertyType = PropertyType.MDPROP_MEMBER;
                var response = await command.ExecuteAsync();
                _LevelProperties = new LevelPropertyCollection(this, response.GetXRows());
            }

            return _LevelProperties;
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

        [XmlElement("LEVEL_UNIQUE_NAME")]
        public string UniqueName { get; set; }

        [XmlIgnore]
        public XmlaConnection Connection { get; set; }

        [XmlIgnore]
        public string CubeName { get; set; }

        [XmlIgnore]
        public bool IsMetadata
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        [XmlIgnore]
        public IXmlaBaseObject ParentObject
        {
            get;
            set;
        }

        [XmlIgnore]
        public SchemaObjectType SchemaObjectType
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException(); 
        }

        [XmlIgnore]
        public XElement SoapRow { get; set; }

        internal Member FindMember(string uname)
        {
            return GetMembers().Find(uname);
        }

        public MemberCollection GetMembers()
        {
            if (_AllMembers == null)
            {
                var command = new XmlaCommand("MDSCHEMA_MEMBERS", Connection);
                command.CommandRestrictions.CubeName = CubeName;
                command.CommandRestrictions.DimensionUniqueName = ParentHierarchy.ParentDimension.UniqueName;
                command.CommandRestrictions.HierarchyUniqueName = ParentHierarchy.UniqueName;
                command.CommandRestrictions.LevelUniqueName = UniqueName;
                var response = command.Execute() as SoapEnvelope;
                _AllMembers = new MemberCollection(this, response.GetXRows());
                //_AllMembers.ToArray();
                //Thread.Sleep(3000);
            }

            return _AllMembers;
        }

        public async Task<MemberCollection> GetMembersAsync()
        {
            if (_AllMembers == null)
            {
                var command = new XmlaCommand("MDSCHEMA_MEMBERS", Connection);
                command.CommandRestrictions.CubeName = CubeName;
                command.CommandRestrictions.DimensionUniqueName = ParentHierarchy.ParentDimension.UniqueName;
                command.CommandRestrictions.HierarchyUniqueName = ParentHierarchy.UniqueName;
                command.CommandRestrictions.LevelUniqueName = UniqueName;
                var response = await command.ExecuteAsync();
                _AllMembers = new MemberCollection(this, response.GetXRows());
                //_AllMembers.ToArray();
                //Thread.Sleep(3000);
            }

            return _AllMembers;
        }


        public MemberCollection GetMembers(long start, long count)
        {
            throw new NotImplementedException();
        }

        public MemberCollection GetMembers(long start, long count, params MemberFilter[] filters)
        {
            throw new NotImplementedException();
        }

        public MemberCollection GetMembers(long start, long count, string[] properties, params MemberFilter[] filters)
        {
            throw new NotImplementedException();
        }
    }
}