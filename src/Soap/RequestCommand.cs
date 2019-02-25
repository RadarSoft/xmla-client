using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Soap
{
    public class RequestCommand
    {
        public RequestCommand()
        {
            Statement = "";
        }

        [XmlElement("Statement")]
        public string Statement { get; set; }
    }
}