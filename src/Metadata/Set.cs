using System;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    public class Set
    {
        internal Set(XElement xAxis, CubeDef cube)
        {
            _xAxis = xAxis;
            _cube = cube;
        }

        private XElement _xAxis;
        private CubeDef _cube;

        private TupleCollection _Tuples;
        public HierarchyCollection Hierarchies { get; }

        public TupleCollection Tuples
        {
            get
            {
                if (_Tuples == null)
                    _Tuples = new TupleCollection(_xAxis, _cube);
                return _Tuples;
            }
        }
    }
}