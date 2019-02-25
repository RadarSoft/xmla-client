using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SimpleSOAPClient.Models;
using System.Linq;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("row", Namespace = Namespace.ComMicrosoftSchemasXmlaRowset)]
    public class CubeDef : IXmlaBaseObject
    {
        private DimensionCollection _Dimensions;

        private KpiCollection _Kpis;

        private MeasureGroupCollection _MeasureGroups;

        private MeasureCollection _Measures;

        private NamedSetCollection _NamedSets;

        [XmlElement("CUBE_NAME")]
        public string CubeName
        {
            get;
            set;
        }

        [XmlElement("CUBE_CAPTION")]
        public string Caption { get; set; }

        [XmlElement("DESCRIPTION")]
        public string Description { get; set; }


        [XmlElement("LAST_DATA_UPDATE")]
        public DateTime LastProcessed { get; set; }

        [XmlElement("LAST_SCHEMA_UPDATE")]
        public DateTime LastUpdated { get; set; }

        [XmlElement("CATALOG_NAME")]
        public string Catalog { get; set; }

        [XmlElement("CUBE_TYPE")]
        public CubeType Type { get; set; }

        [XmlIgnore]
        public DimensionCollection Dimensions
        {
            get
            {
                if (_Dimensions == null)
                {
                    var command = new XmlaCommand("MDSCHEMA_DIMENSIONS", Connection);
                    command.CommandRestrictions.CubeName = CubeName;
                    var response = command.Execute();
                    _Dimensions = new DimensionCollection(this, response.GetXRows());
                }

                return _Dimensions;
            }
        }

        public async Task<DimensionCollection> GetDimensionsAsync()
        {
            if (_Dimensions == null)
            {
                var command = new XmlaCommand("MDSCHEMA_DIMENSIONS", Connection);
                command.CommandRestrictions.CubeName = CubeName;
                var response = await command.ExecuteAsync();
                _Dimensions = new DimensionCollection(this, response.GetXRows());
            }

            return _Dimensions;
        }

        [XmlIgnore]
        public MeasureGroupCollection MeasureGroups
        {
            get
            {
                if (_MeasureGroups == null)
                    _MeasureGroups = new MeasureGroupCollection();

                if (_MeasureGroups.Count == 0)
                {
                    var command = new XmlaCommand("MDSCHEMA_MEASUREGROUPS", Connection);
                    command.CommandRestrictions.CubeName = CubeName;
                    var response = command.Execute();

                    try
                    {
                        foreach (var xrow in response.GetXRows())
                        {
                            var mgroup = xrow.ToObject<MeasureGroup>();
                            _MeasureGroups.Add(mgroup);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }

                return _MeasureGroups;
            }
        }

        public async Task<MeasureGroupCollection> GetMeasureGroupsAync()
        {
            if (_MeasureGroups == null)
                _MeasureGroups = new MeasureGroupCollection();

            if (_MeasureGroups.Count == 0)
            {
                var command = new XmlaCommand("MDSCHEMA_MEASUREGROUPS", Connection);
                command.CommandRestrictions.CubeName = CubeName;

                var response = await command.ExecuteAsync();

                try
                {
                    var tasks = response.GetXRows().Select(xrow => xrow.ToXmlaObjectAsync<MeasureGroup>(this));
                    var results = await Task.WhenAll(tasks);

                    _MeasureGroups.AddRange(results);
                }
                catch
                {
                    throw;
                }
            }

            return _MeasureGroups;
        }


        [XmlIgnore]
        public MeasureCollection Measures
        {
            get
            {
                if (_Measures == null)
                    _Measures = new MeasureCollection();

                if (_Measures.Count == 0)
                {
                    var command = new XmlaCommand("MDSCHEMA_MEASURES", Connection);
                    command.CommandRestrictions.CubeName = CubeName;
                    var response = command.Execute();

                    try
                    {
                        foreach (var xrow in response.GetXRows())
                        {
                            var meas = xrow.ToObject<Measure>();
                            meas.SoapRow = xrow;
                            meas.CubeName = CubeName;
                            meas.Connection = Connection;
                            _Measures.Add(meas);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }

                return _Measures;
            }
        }

        public async Task<MeasureCollection> GetMeasuresAsinc()
        {
            if (_Measures == null)
                _Measures = new MeasureCollection();

            if (_Measures.Count == 0)
            {
                var command = new XmlaCommand("MDSCHEMA_MEASURES", Connection);
                command.CommandRestrictions.CubeName = CubeName;
                var response = await command.ExecuteAsync();

                try
                {
                    var tasks = response.GetXRows().Select(xrow => xrow.ToXmlaObjectAsync<Measure>(this));
                    var results = await Task.WhenAll(tasks);

                    _Measures.AddRange(results);
                }
                catch
                {
                    throw;
                }
            }

            return _Measures;
        }

        [XmlIgnore]
        public KpiCollection Kpis
        {
            get
            {
                if (_Kpis == null)
                    _Kpis = new KpiCollection();

                if (_Kpis.Count == 0)
                {
                    var command = new XmlaCommand("MDSCHEMA_KPIS", Connection);
                    command.CommandRestrictions.CubeName = CubeName;
                    var response = command.Execute() as SoapEnvelope;

                    try
                    {
                        foreach (var xrow in response.GetXRows())
                        {
                            var kpi = xrow.ToObject<Kpi>();
                            kpi.SoapRow = xrow;
                            kpi.Connection = Connection;
                            kpi.CubeName = CubeName;
                            _Kpis.Add(kpi);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }

                return _Kpis;
            }
        }

        public async Task<KpiCollection> GetKpisAsync()
        {
            if (_Kpis == null)
                _Kpis = new KpiCollection();

            if (_Kpis.Count == 0)
            {
                var command = new XmlaCommand("MDSCHEMA_KPIS", Connection);
                command.CommandRestrictions.CubeName = CubeName;
                var response = await command.ExecuteAsync();

                try
                {
                    var tasks = response.GetXRows().Select(xrow => xrow.ToXmlaObjectAsync<Kpi>(this));
                    var results = await Task.WhenAll(tasks);

                    _Kpis.AddRange(results);
                }
                catch
                {
                    throw;
                }
            }

            return _Kpis;
        }


        [XmlIgnore]
        public NamedSetCollection NamedSets
        {
            get
            {
                if (_NamedSets == null)
                    _NamedSets = new NamedSetCollection();

                if (_NamedSets.Count == 0)
                {
                    var command = new XmlaCommand("MDSCHEMA_SETS", Connection);
                    command.CommandRestrictions.CubeName = CubeName;
                    var response = command.Execute() as SoapEnvelope;

                    try
                    {
                        foreach (var xrow in response.GetXRows())
                        {
                            var sets = xrow.ToObject<NamedSet>();
                            sets.SoapRow = xrow;
                            sets.Connection = Connection;
                            sets.CubeName = CubeName;
                            _NamedSets.Add(sets);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }

                return _NamedSets;
            }
        }

        public async Task<NamedSetCollection> GetNamedSetsAsync()
        {
            if (_NamedSets == null)
                _NamedSets = new NamedSetCollection();

            if (_NamedSets.Count == 0)
            {
                var command = new XmlaCommand("MDSCHEMA_SETS", Connection);
                command.CommandRestrictions.CubeName = CubeName;
                var response = await command.ExecuteAsync();

                try
                {
                    var tasks = response.GetXRows().Select(xrow => xrow.ToXmlaObjectAsync<NamedSet>(this));
                    var results = await Task.WhenAll(tasks);

                    _NamedSets.AddRange(results);
                }
                catch
                {
                    throw;
                }
            }

            return _NamedSets;
        }


        public string Name { get; }

        [XmlIgnore]
        public XmlaConnection ParentConnection { get; set; }

        public PropertyCollection Properties { get; set; }

        public string UniqueName { get; set; }

        [XmlIgnore]
        public XmlaConnection Connection { get; set; }

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
        public SchemaObjectType SchemaObjectType { get; set; }

        [XmlIgnore]
        public XElement SoapRow { get; set; }
    }
}