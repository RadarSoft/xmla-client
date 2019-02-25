using RadarSoft.XmlaClient;
using RadarSoft.XmlaClient.Metadata;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.XmlaClient.NetCore
{
    public static class TestHelper
    {
        public static XmlaConnection CreateConnectionToSsas(string sessionId = "")
        {
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "http://localhost/OLAP/msmdpump.dll";
            csBuilder.InitialCatalog = @"Analysis Services Tutorial";
            csBuilder.ConnectTimeout = 30;
            //csBuilder.UserID = "sa";
            //csBuilder.Password = "admin@123";

            //Data Source=http://localhost/OLAP/msmdpump.dll;Initial Catalog="Analysis Services Tutorial";Connect Timeout=30
            string cs = csBuilder.ConnectionString;

            var connection = new XmlaConnection(cs);

            if (!string.IsNullOrEmpty(sessionId))
                connection.SessionID = sessionId;

            return connection;
        }

        public static CubeDef GetCube(XmlaConnection connection, string cubeName = "Analysis Services Tutorial")
        {
            return connection.Cubes.Find(cubeName);
        }

        public static async Task<CubeDef> GetCubeAsync(XmlaConnection connection, string cubeName = "Analysis Services Tutorial")
        {
            return await connection.Cubes.FindAsync(cubeName);
        }

    }
}
