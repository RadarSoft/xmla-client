using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Soap
{
    [XmlRoot("Properties")]
    public class Properties
    {
        public Properties() { }

        public Properties(PropertyList properties) {
            if (properties == null)
                PropertyList = new PropertyList();
            else
                PropertyList = properties;
        }

        [XmlElement("PropertyList")]
        public PropertyList PropertyList { get; set; }
    }
}