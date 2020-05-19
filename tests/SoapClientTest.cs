using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadarSoft.XmlaClient;
using SimpleSOAPClient.Models;
using RadarSoft.XmlaClient.Soap;
using SimpleSOAPClient;
using SimpleSOAPClient.Exceptions;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models.Headers;

namespace UnitTest.XmlaClient.NetCore
{
    [TestClass]
    public class SoapClientTest
    {
        [TestMethod]
        public void BuildEnvelopeHeaderTest()
        {
            bool assert = true;
            var envelope = SoapEnvelope.Prepare().WithHeaders(new BeginSessionHeader());
            using (var _soapClient = SoapClient.Prepare())
            {
                try
                {
                    var xml = _soapClient.Settings.SerializationProvider.ToXmlString(envelope);
                    Debug.WriteLine(xml);
                }
                catch (SoapEnvelopeSerializationException e)
                {
                    assert = false;
                    Debug.WriteLine(e.Message);
                }
                catch (SoapEnvelopeDeserializationException e)
                {
                    assert = false;
                    Debug.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    assert = false;
                    Debug.WriteLine(e.Message);
                }

            }
            Assert.IsTrue(assert);
        }
    }
}
