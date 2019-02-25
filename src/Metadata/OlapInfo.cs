using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    public class OlapInfo
    {
        public OlapInfo(SoapEnvelope envelope)
        {
            _envelope = envelope;
        }

        private SoapEnvelope _envelope;
        //public AxesInfo AxesInfo { get; }
        //public CellInfo CellInfo { get; }
        private CubeInfo _CubeInfo;

        public CubeInfo CubeInfo
        {
            get
            {
                if (_CubeInfo == null)
                    _CubeInfo = new CubeInfo(_envelope);

                return _CubeInfo;
            }
        }
    }
}