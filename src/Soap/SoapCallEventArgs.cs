using System;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Soap
{
    public class SoapCallEventArgs : EventArgs
    {
        private readonly SoapEnvelope _request;

        public SoapCallEventArgs(string action, SoapEnvelope request)
        {
            Action = action;
            _request = request;
        }

        public SoapEnvelope Request
        {
            get => _request;
            set => Response = value;
        }

        public SoapEnvelope Response { get; internal set; }

        public string Action { get; internal set; }

        public Exception Exception { get; internal set; }
    }
}