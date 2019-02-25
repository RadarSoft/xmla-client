using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    public class CellSet
    {
        public CellSet(XmlaConnection connection, SoapEnvelope envelope)
        {
            _connection = connection;
            _envelope = envelope;
        }

        private SoapEnvelope _envelope;
        private CubeDef _cube;
        private XmlaConnection _connection;
        internal CubeDef Cube
        {
            get
            {
                if (_cube == null)
                    _cube = _connection.Cubes.Find(OlapInfo.CubeInfo.Cubes[0].CubeName);
                return _cube;
            }
        }

        private AxisCollection _Axes;
        private CellCollection _Cells;

        public AxisCollection Axes
        {
            get
            {
                if (_Axes == null)
                    _Axes = new AxisCollection(_envelope, Cube);

                return _Axes;
            }
        }

        public CellCollection Cells
        {
            get
            {
                if (_Cells == null)
                    _Cells = new CellCollection(_envelope);

                return _Cells;
            }
        }

        public Axis FilterAxis => Axes.FilterAxis;

        private OlapInfo _olapInfo;

        public OlapInfo OlapInfo
        {
            get
            {
                if (_olapInfo != null)
                    return _olapInfo;

                _olapInfo = new OlapInfo(_envelope);
                return _olapInfo;
            }
        }

        public static CellSet LoadXml(XmlReader xmlTextReader)
        {
            throw new NotImplementedException();
        }
    }
}