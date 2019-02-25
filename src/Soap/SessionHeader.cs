using System;
using System.Xml.Serialization;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Soap
{
    public class SessionHeader : SoapHeader
    {
        public SessionHeader()
        {
            MustUnderstand = 1;
        }
    }

    [XmlRoot("BeginSession", Namespace = Namespace.ComMicrosoftSchemasXmla)]
    public class BeginSessionHeader : SessionHeader
    {
    }

    [XmlRoot("Session", Namespace = Namespace.ComMicrosoftSchemasXmla)]
    public class ContinueSessionHeader : SessionHeader
    {
        public ContinueSessionHeader()
        {
        }

        public ContinueSessionHeader(string sessionId)
        {
            SessionId = sessionId;// new Guid();
        }

        [XmlAttribute("SessionId")]
        public string SessionId { get; set; }
    }

    [XmlRoot("EndSession", Namespace = Namespace.ComMicrosoftSchemasXmla)]
    public class EndSessionHeader : ContinueSessionHeader
    {
        public EndSessionHeader()
        {
        }

        public EndSessionHeader(string sessionId) : base(sessionId)
        {
        }
    }
}