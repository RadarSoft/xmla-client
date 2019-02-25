using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RadarSoft.XmlaClient.Metadata;
using SimpleSOAPClient.Exceptions;
using SimpleSOAPClient.Models;
using Action = RadarSoft.XmlaClient.Metadata.Action;

namespace RadarSoft.XmlaClient
{
    /// <summary>
    ///     Encapsulates the AdomdConnection intrinsinc object that contains ADOMD-specific information about a connection.
    /// </summary>
    public class XmlaConnection : DbConnection
    {
        private string _ConnectionString;
#if DEBUG
        public ConnectionState _State;
#else
        internal ConnectionState _State;
#endif
        private CubeCollection _Cubes;

        /// <summary>
        ///     Initializes a new instance of the XmlaConnection class.
        /// </summary>
        public XmlaConnection()
        {
            _State = ConnectionState.Closed;
        }

        /// <summary>
        ///     Initializes a new instance of the MdxConnection class when given a string that contains the connection string.
        /// </summary>
        /// <param name="connectionString">
        ///     The connection used to open the SQL Server Analysis Services database.
        /// </param>
        public XmlaConnection(string connectionString)
        {
            _State = ConnectionState.Closed;
            _ConnectionString = connectionString;
        }

        public override string ConnectionString
        {
            get => _ConnectionString;
            set => _ConnectionString = value;
        }

        public override string Database => ConnectionStringParser.GetDatabaseName(ConnectionString);

        public override string DataSource => ConnectionStringParser.GetDataSourceName(ConnectionString);

        /// <summary>
        ///     ServerVersion was called while the returned Task was not completed and the connection was not opened after a call
        ///     to OpenAsync.
        /// </summary>
        public override string ServerVersion { get; }

        public override ConnectionState State => _State;

        /// <summary>
        ///     Gets or sets the string identifier of the session that opened on the server.
        /// </summary>
        public string SessionID { get; set; } = "";

        public CubeCollection Cubes
        {
            get
            {
                if (_Cubes == null)
                    _Cubes = new CubeCollection(this);
                return _Cubes;
            }
        }


        public bool ShowHiddenObjects { get; set; }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotSupportedException();
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Close(bool endSession)
        {
            if (endSession)
                Close();
        }

        public override void Close()
        {
            var command = new XmlaCommand("", this);
            try
            {
                _State = ConnectionState.Executing;
                command.Execute();
                _State = ConnectionState.Closed;
            }
            catch (FaultException e)
            {
                _State = ConnectionState.Closed;
                throw e;
            }
            catch (Exception e)
            {
                _State = ConnectionState.Closed;
                throw e;
            }
        }

        public async Task CloseAsync()
        {
            var command = new XmlaCommand("", this);
            try
            {
                _State = ConnectionState.Executing;
                await command.ExecuteAsync();
                _State = ConnectionState.Closed;
            }
            catch (FaultException e)
            {
                _State = ConnectionState.Closed;
                throw e;
            }
            catch (Exception e)
            {
                _State = ConnectionState.Closed;
                throw e;
            }
        }

        protected override DbCommand CreateDbCommand()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Opens a database connection with the property settings specified by the connection string.
        /// </summary>
        public override void Open()
        {
            var command = new XmlaCommand("", this);
            _State = ConnectionState.Connecting;
            try
            {
                command.Execute();
                _State = ConnectionState.Open;
            }
            catch (FaultException e)
            {
                _State = ConnectionState.Closed;
                throw e;
            }
            catch (Exception e)
            {
                _State = ConnectionState.Closed;
                throw e;
            }
        }

        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
            var command = new XmlaCommand("", this);
            _State = ConnectionState.Connecting;
            try
            {
                await command.ExecuteAsync();
                _State = ConnectionState.Open;
            }
            catch (FaultException e)
            {
                _State = ConnectionState.Closed;
                throw e;
            }
            catch (Exception e)
            {
                _State = ConnectionState.Closed;
                throw e;
            }
        }


        public ActionCollection GetActions(string cubeName, string coordinate, CoordinateType coordinateType)
        {
            var actions = new ActionCollection();

            var command = new XmlaCommand("MDSCHEMA_ACTIONS", this);
            command.CommandRestrictions.CubeName = cubeName;
            command.CommandRestrictions.Coordinate = coordinate;
            command.CommandRestrictions.CoordinateType = coordinateType;
            var response = command.Execute() as SoapEnvelope;

            try
            {
                foreach (var xrow in response.GetXRows())
                {
                    var action = xrow.ToObject<Action>();
                    action.SoapRow = xrow;
                    actions.Add(action);
                }
            }
            catch
            {
                throw;
            }

            return actions;
        }

        public async Task<ActionCollection> GetActionsAsync(string cubeName, string coordinate, CoordinateType coordinateType)
        {
            var actions = new ActionCollection();

            var command = new XmlaCommand("MDSCHEMA_ACTIONS", this);
            command.CommandRestrictions.CubeName = cubeName;
            command.CommandRestrictions.Coordinate = coordinate;
            command.CommandRestrictions.CoordinateType = coordinateType;
            var response = await command.ExecuteAsync();

            try
            {
                var tasks = response.GetXRows().Select(xrow => xrow.ToXmlaObjectAsync<Action>());
                var results = await Task.WhenAll(tasks);

                actions.AddRange(results);
            }
            catch
            {
                throw;
            }

            return actions;
        }
    }
}