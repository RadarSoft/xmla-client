using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadarSoft.XmlaClient;
using RadarSoft.XmlaClient.Metadata;

namespace UnitTest.XmlaClient.NetCore.AsyncTests
{
    [TestClass]
    public class ExecuteTest
    {
        [TestMethod]
        public async Task ExecuteAsync()
        {
            bool assert = true;
            var connection = TestHelper.CreateConnectionToSsas();
            var command = new XmlaCommand("", connection);
            try
            {
                var result = await command.ExecuteAsync();
            }
            catch
            {
                assert = false;
            }
            Assert.IsTrue(assert);
        }

        [TestMethod]
        public async Task ExecuteCellSetAsync()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            await connection.OpenAsync();
            var statment = TestHelper.TEST_CELLSET_COMMAND;
            var command = new XmlaCommand(statment, connection);
            CellSet cellset = null;
            cellset = await command.ExecuteCellSetAsync();
            Assert.IsTrue(cellset != null && cellset.Axes.Count > 0 && cellset.Cells.Count > 0);
            await connection.CloseAsync();
        }
    }
}
