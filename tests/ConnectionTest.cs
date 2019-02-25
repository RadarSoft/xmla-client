using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadarSoft.XmlaClient;

namespace UnitTest.XmlaClient.NetCore
{
    [TestClass]
    public class ConnectionTest
    {
        [TestMethod]
        public void OpenConnection()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();
            Assert.AreNotEqual(connection.SessionID, "");
        }

        [TestMethod]
        public void OpenConnectionWithSessionID()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();
            Assert.AreNotEqual(connection.SessionID, "");

            var connection2 = TestHelper.CreateConnectionToSsas(connection.SessionID);
            connection2.Open();

            Assert.AreEqual(connection2.State, System.Data.ConnectionState.Open);
        }

        [TestMethod]
        public void CloseConnection()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();
            Assert.AreNotEqual(connection.SessionID, "");

            var connection2 = TestHelper.CreateConnectionToSsas(connection.SessionID);
            connection2.Close();

            Assert.AreEqual(connection2.State, System.Data.ConnectionState.Closed);
        }

    }
}
