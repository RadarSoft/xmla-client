using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Soap
{
    public class Restrictions
    {
        public Restrictions()
        {
        }

        public Restrictions(RestrictionList restrictions)
        {
            RestrictionList = restrictions;
        }

        [XmlElement("RestrictionList")]
        public RestrictionList RestrictionList { get; set; }
    }
}