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
        public const string TEST_DATASOURCE = "***MISSING***";
        private const string TEST_USERNAME = "***MISSING***";
        private const string TEST_PASSWORD = "***MISSING***";
        
        public const string TEST_CATALOG = "***MISSING***";
        public const string TEST_CUBE_NAME = "***MISSING***";
        public const string TEST_HIERARCHY = "***MISSING***";
        public const string TEST_CELLSET_COMMAND = @"***MISSING***";

        public static XmlaConnection CreateConnectionToSsas(string sessionId = "")
        {
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = TEST_DATASOURCE;
            csBuilder.InitialCatalog = TEST_CATALOG;
            csBuilder.ConnectTimeout = 30;
            csBuilder.UserID = TEST_USERNAME;
            csBuilder.Password = TEST_PASSWORD;

            string cs = csBuilder.ConnectionString;

            var connection = new XmlaConnection(cs);

            if (!string.IsNullOrEmpty(sessionId))
                connection.SessionID = sessionId;

            return connection;
        }

        public static CubeDef GetCube(XmlaConnection connection, string cubeName = TEST_CUBE_NAME)
        {
            return connection.Cubes.Find(cubeName);
        }

        public static async Task<CubeDef> GetCubeAsync(XmlaConnection connection, string cubeName = TEST_CUBE_NAME)
        {
            return await connection.Cubes.FindAsync(cubeName);
        }

    }
}
