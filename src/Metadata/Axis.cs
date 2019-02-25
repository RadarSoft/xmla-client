using System;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    public class Axis
    {
        internal Axis(XElement xAxis, CubeDef cube)
        {
            _xAxis = xAxis;
            _cube = cube;
        }

        private XElement _xAxis;
        private CubeDef _cube;
        private PositionCollection _Positions;
        private Set _Set;

        private string _Name;

        public string Name
        {
            get
            {
                if(string.IsNullOrEmpty(_Name))
                    _Name = _xAxis.Attribute(XName.Get("name"))?.Value;

                return _Name;
            }
        }

        public PositionCollection Positions
        {
            get
            {
                if (_Positions == null)
                    _Positions = new PositionCollection(Set.Tuples);
                return _Positions;
            }
        }

        public Set Set
        {
            get
            {
                if (_Set == null)
                    _Set = new Set(_xAxis, _cube);
                return _Set;
            }
        }
    }
}