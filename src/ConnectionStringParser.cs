using System;
using System.Linq;

namespace RadarSoft.XmlaClient
{
    public static class ConnectionStringParser
    {
        private static readonly string[] serverAliases =
        {
            "server", "host", "data source", "datasource", "address",
            "addr", "network address"
        };

        private static readonly string[] serverVersion = {"server version"};
        private static readonly string[] databaseAliases = {"database", "initial catalog"};
        private static readonly string[] usernameAliases = {"userid", "user id", "uid", "username", "user name", "user"};
        private static readonly string[] passwordAliases = {"password", "pwd"};

        public static string GetPassword(string connectionString)
        {
            return GetValue(connectionString, passwordAliases);
        }

        public static string GetUsername(string connectionString)
        {
            return GetValue(connectionString, usernameAliases);
        }

        public static string GetDatabaseName(string connectionString)
        {
            return GetValue(connectionString, databaseAliases);
        }

        public static string GetDataSourceName(string connectionString)
        {
            return GetValue(connectionString, serverAliases);
        }

        private static string GetValue(string connectionString, params string[] keyAliases)
        {
            var keyValuePairs = connectionString.Split(';')
                .Where(kvp => kvp.Contains('='))
                .Select(kvp => kvp.Split(new[] {'='}, 2))
                .ToDictionary(kvp => kvp[0].Trim(),
                    kvp => kvp[1].Trim(),
                    StringComparer.CurrentCultureIgnoreCase);
            foreach (var alias in keyAliases)
            {
                string value;
                if (keyValuePairs.TryGetValue(alias, out value))
                    return value.Trim('"');
            }
            return string.Empty;
        }
    }
}