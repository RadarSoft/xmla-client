using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadarSoft.XmlaClient;
using RadarSoft.XmlaClient.Metadata;

namespace UnitTest.XmlaClient.NetCore
{
    [TestClass]
    public class ExecuteTest
    {
        [TestMethod]
        public void Execute()
        {
            bool assert = true;
            var connection = TestHelper.CreateConnectionToSsas();
            var command = new XmlaCommand("", connection);
            try
            {
                var result = command.Execute();
            }
            catch
            {
                assert = false;
            }
            Assert.IsTrue(assert);
        }

        [TestMethod]
        public void ExecuteCellSet()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();
            var statment = TestHelper.TEST_CELLSET_COMMAND;
            var command = new XmlaCommand(statment, connection);
            CellSet cellset = null;
            cellset = command.ExecuteCellSet();

            Assert.IsTrue(cellset != null && cellset.Axes.Count > 0 && cellset.Cells.Count > 0);

            connection.Close();

        }
    }
}
