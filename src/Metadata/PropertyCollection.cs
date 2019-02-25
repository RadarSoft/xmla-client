using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    public class PropertyCollection : List<Property>
    {
        internal XElement SoapRow { get; set; }

        public Property Find(string name)
        {
            var prop = this.FirstOrDefault(x => x.Name == name);
            if (prop == null)
            {
                var propValue = SoapRow.Element(XName.Get(name, Namespace.ComMicrosoftSchemasXmlaRowset))?.Value;
                if (propValue != null)
                {
                    prop = new Property {Name = name, Value = propValue};
                    Add(prop);
                }
            }

            return prop;
        }
    }
}