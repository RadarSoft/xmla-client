using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    public class CubeInfo
    {
        public CubeInfo(SoapEnvelope envelope)
        {
            _envelope = envelope;
        }

        private SoapEnvelope _envelope;


        private OlapInfoCubeCollection _Cubes;


        public OlapInfoCubeCollection Cubes
        {
            get
            {
                if (_Cubes == null)
                    _Cubes = new OlapInfoCubeCollection(_envelope);
                return _Cubes;
            }
        }
    }
}