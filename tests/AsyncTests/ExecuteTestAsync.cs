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
            var connection = new XmlaConnection("Data Source=http://localhost/OLAP/msmdpump.dll;Initial Catalog=Analysis Services Tutorial");
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
            var connection = new XmlaConnection("Data Source=http://localhost/OLAP/msmdpump.dll;Initial Catalog=Analysis Services Tutorial");
            await connection.OpenAsync();
            //var statment = "SELECT FROM [Analysis Services Tutorial] WHERE [Measures].[Internet Sales Count]";
            //var statment = "SELECT {HEAD(NONEMPTY({{[Product].[Category].[Category].ALLMEMBERS}}), 250000)} DIMENSION PROPERTIES MEMBER_TYPE ON 0 FROM [Analysis Services Tutorial] WHERE [Measures].[Internet Sales Count]";
            var statment = "SELECT {HEAD(NONEMPTY({{[Product].[Category].&[4],[Product].[Category].&[1]}*{[Date].[Fiscal Year].[Fiscal Year].ALLMEMBERS}*{[Reseller Geography].[Country-Region].[Country-Region].ALLMEMBERS}}), 250000)} DIMENSION PROPERTIES MEMBER_TYPE ON 0 FROM [Analysis Services Tutorial] WHERE [Measures].[Internet Sales Count]";
            var command = new XmlaCommand(statment, connection);
            CellSet cellset = null;
            cellset = await command.ExecuteCellSetAsync();
            Assert.IsTrue(cellset != null && cellset.Axes.Count > 0 && cellset.Cells.Count > 0);
            await connection.CloseAsync();
        }
    }
}
