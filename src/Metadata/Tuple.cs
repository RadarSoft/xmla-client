using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Metadata
{
    [XmlRoot("Tuple", Namespace = Namespace.ComMicrosoftSchemasXmlaMddataset)]
    public class Tuple
    {
        internal Tuple(XElement xTuple, CubeDef cube)
        {
            _xTuple = xTuple;
            _cube = cube;
        }

        private XElement _xTuple;
        private CubeDef _cube;

        private MemberCollection _Members;

        public MemberCollection Members
        {
            get
            {
                if (_Members == null)
                    _Members = new MemberCollection(_xTuple, _cube);

                return _Members;
            }
        }
    }
}