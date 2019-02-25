using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    public class CubeCollection : List<CubeDef>, IXmlaBaseObject
    {
        public CubeCollection(XmlaConnection connection)
        {
            Connection = connection;
        }

        public CubeDef Find(string name)
        {
            var res = this.FirstOrDefault(x => x.CubeName == name);

            if (res == null)
            {
                var command = new XmlaCommand("MDSCHEMA_CUBES", Connection);
                var response = command.Execute() as SoapEnvelope;

                try
                {
                    foreach (var xrow in response.GetXRows())
                    {
                        if (xrow.Element(XName.Get("CUBE_NAME", Namespace.ComMicrosoftSchemasXmlaRowset)).Value != name)
                            continue;

                        res = xrow.ToObject<CubeDef>();
                        res.Connection = Connection;
                        Add(res);
                        break;
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            return res;
        }

        public async Task<CubeDef> FindAsync(string name)
        {
            var res = this.FirstOrDefault(x => x.CubeName == name);

            if (res == null)
            {
                var command = new XmlaCommand("MDSCHEMA_CUBES", Connection);
                var response = await command.ExecuteAsync();

                try
                {
                    var task = response.GetXRows().FirstOrDefault(xrow => xrow.Element(XName.Get("CUBE_NAME", Namespace.ComMicrosoftSchemasXmlaRowset))?.Value == name).ToXmlaObjectAsync<CubeDef>(this);
                    res = await task;
                    if (res != null)
                    {
                        res.CubeName = name;
                        Add(res);
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            return res;
        }

        [XmlIgnore]
        public XmlaConnection Connection { get; set; }
        [XmlIgnore]
        public string CubeName { get; set; }
        [XmlIgnore]
        public bool IsMetadata { get; set; }
        [XmlIgnore]
        public IXmlaBaseObject ParentObject { get; set; }
        [XmlIgnore]
        public SchemaObjectType SchemaObjectType { get; set; }
        [XmlIgnore]
        public XElement SoapRow { get; set; }
    }
}