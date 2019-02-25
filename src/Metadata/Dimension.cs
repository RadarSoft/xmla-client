using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("row", Namespace = Namespace.ComMicrosoftSchemasXmlaRowset)]
    public class Dimension : IXmlaBaseObject
    {
        private HierarchyCollection _Hierarchies;

        private PropertyCollection _Properties;

        [XmlElement("DIMENSION_CAPTION")]
        public string Caption { get; set; }

        [XmlElement("DESCRIPTION")]
        public string Description { get; set; }

        [XmlElement("DIMENSION_TYPE")]
        public DimensionTypeEnum DimensionType { get; set; }

        [XmlElement("DIMENSION_NAME")]
        public string Name { get; set; }

        [XmlElement("IS_READWRITE")]
        public bool WriteEnabled { get; set; }

        [XmlElement("DIMENSION_UNIQUE_NAME")]
        public string UniqueName { get; set; }

        [XmlIgnore]
        public SchemaObjectType SchemaObjectType { get; set; }

        [XmlIgnore]
        public XElement SoapRow { get; set; }

        [XmlElement("DIMENSION_ORDINAL")]
        public int Ordinal { get; set; }

        [XmlIgnore]
        public HierarchyCollection Hierarchies
        {
            get
            {
                if (_Hierarchies == null)
                {
                    var command = new XmlaCommand("MDSCHEMA_HIERARCHIES", Connection);
                    command.CommandRestrictions.CubeName = CubeName;
                    command.CommandRestrictions.DimensionUniqueName = UniqueName;
                    var response = command.Execute();
                    _Hierarchies = new HierarchyCollection(this, response.GetXRows());
                }

                return _Hierarchies;
            }
        }

        public async Task<HierarchyCollection> GetHierarchiesAsync()
        {
            if (_Hierarchies == null)
            {
                var command = new XmlaCommand("MDSCHEMA_HIERARCHIES", Connection);
                command.CommandRestrictions.CubeName = CubeName;
                command.CommandRestrictions.DimensionUniqueName = UniqueName;
                var response = await command.ExecuteAsync();
                _Hierarchies = new HierarchyCollection(this, response.GetXRows());
                //_Hierarchies.ToArray();
            }

            return _Hierarchies;
        }

        public CubeDef ParentCube { get; }

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
        public XmlaConnection Connection { get; set; }

        [XmlIgnore]
        public bool IsMetadata { get; set; }

        [XmlIgnore]
        public string CubeName { get; set; }

        [XmlIgnore]
        public IXmlaBaseObject ParentObject
        {
            get;
            set;
        }
    }
}