using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("row", Namespace = Namespace.ComMicrosoftSchemasXmlaRowset)]
    public class Hierarchy : IXmlaBaseObject
    {
        public Hierarchy()
        {

        }

        private LevelCollection _Levels;

        private PropertyCollection _Properties;

        [XmlElement("HIERARCHY_CAPTION")]
        public string Caption { get; set; }

        [XmlElement("DEFAULT_MEMBER")]
        public string DefaultMember { get; set; }

        [XmlElement("DESCRIPTION")]
        public string Description { get; set; }

        [XmlElement("HIERARCHY_DISPLAY_FOLDER")]
        public string DisplayFolder { get; set; }

        [XmlIgnore]
        public HierarchyOrigin HierarchyOrigin { get; set; }

        [XmlElement("HIERARCHY_ORIGIN")]
        public int HierarchyOriginSerializer
        {
            get => (int) HierarchyOrigin;
            set => HierarchyOrigin = (HierarchyOrigin) value;
        }

        [XmlElement("HIERARCHY_NAME")]
        public string Name { get; set; }

        [XmlIgnore]
        public LevelCollection Levels
        {
            get
            {
                if (_Levels == null)
                {
                    var command = new XmlaCommand("MDSCHEMA_LEVELS", Connection);
                    command.CommandRestrictions.CubeName = CubeName;
                    command.CommandRestrictions.DimensionUniqueName = ParentDimension.UniqueName;
                    command.CommandRestrictions.HierarchyUniqueName = UniqueName;
                    var response = command.Execute();
                    _Levels = new LevelCollection(this, response.GetXRows());
                }
                return _Levels;
            }
        }

        public async Task<LevelCollection> GetLevelsAsync()
        {
            if (_Levels == null)
            {
                var command = new XmlaCommand("MDSCHEMA_LEVELS", Connection);
                command.CommandRestrictions.CubeName = CubeName;
                command.CommandRestrictions.DimensionUniqueName = ParentDimension.UniqueName;
                command.CommandRestrictions.HierarchyUniqueName = UniqueName;
                var response = await command.ExecuteAsync();
                _Levels = new LevelCollection(this, response.GetXRows());
                //_Levels.ToArray();
            }
            return _Levels;
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
        public Dimension ParentDimension => ParentObject as Dimension;

        [XmlElement("HIERARCHY_UNIQUE_NAME")]
        public string UniqueName { get; set; }

        [XmlIgnore]
        public XmlaConnection Connection { get; set; }

        public bool IsMetadata { get; set; }

        [XmlIgnore]
        public string CubeName { get; set; }

        [XmlIgnore]
        public SchemaObjectType SchemaObjectType { get; set; }
        [XmlIgnore]
        public XElement SoapRow { get; set; }

        [XmlIgnore]
        public IXmlaBaseObject ParentObject
        {
            get;
            set;
        }

        public Level FindLevel(string uname)
        {
            return Levels.FirstOrDefault(x => x.UniqueName == uname);
        }
    }
}