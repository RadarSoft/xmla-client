using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using RadarSoft.XmlaClient.Metadata;
using RadarSoft.XmlaClient.Soap;
using SimpleSOAPClient;
using SimpleSOAPClient.Exceptions;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient
{
    /// <summary>
    ///     Encapsulates the XmlaCommand intrinsinc object that contains XMLA-specific information about a command.
    /// </summary>
    public class XmlaCommand : DbCommand
    {
        private RestrictionList _CommandRestrictions;
        private string _commandText;
        private XmlaConnection _connection;

        private PropertyList _PropertyList;
        private SoapClient _soapClient;

        /// <summary>
        ///     Initializes a new instance of the XmlaCommand class.
        /// </summary>
        public XmlaCommand()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the MdxCommand class with the text of the query.
        /// </summary>
        /// <param name="commandText">The text of the query.</param>
        public XmlaCommand(string commandText)
        {
            _commandText = commandText;
        }

        /// <summary>
        ///     Initializes a new instance of the MdxCommand class with the text of the query and a MdxConnection.
        /// </summary>
        /// <param name="commandText">The text of the query.</param>
        /// <param name="connection">An MdxConnection representing the connection to SQL Server Analysis Services.</param>
        public XmlaCommand(string commandText, XmlaConnection connection)
        {
            _commandText = commandText;

            if (null == connection)
                throw new ArgumentNullException("connection");

            Connection = connection;
        }

        /// <summary>
        ///     Gets or sets the MdxConnection used by this instance of the MdxCommand.
        /// </summary>
        protected override DbConnection DbConnection
        {
            get => _connection;
            set => _connection = (XmlaConnection) value;
        }

        public PropertyList PropertyList
        {
            get
            {
                if (_PropertyList == null)
                {
                    _PropertyList = new PropertyList();
                    _PropertyList.Catalog = ConnectionStringParser.GetDatabaseName(Connection.ConnectionString);
                }

                return _PropertyList;
            }
            set => _PropertyList = value;
        }

        public RestrictionList CommandRestrictions
        {
            get
            {
                if (_CommandRestrictions == null)
                    _CommandRestrictions = new RestrictionList();
                return _CommandRestrictions;
            }
            set => _CommandRestrictions = value;
        }

        public override string CommandText
        {
            get => _commandText;
            set => _commandText = value;
        }

        public override int CommandTimeout
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override CommandType CommandType
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        protected override DbParameterCollection DbParameterCollection => throw new NotImplementedException();

        protected override DbTransaction DbTransaction
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override bool DesignTimeVisible
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        internal RequestMethod RequestMethod { get; set; }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        ///     <see cref="SoapEnvelope" object that the OLAP server send.
        /// </returns>
        /// <exception cref="FaultException">Thrown if the OLAP server send a fault.</exception>
        public SoapEnvelope Execute()
        {
            Prepare();

            var envelope = SoapEnvelope.Prepare();
            BuildEnvelopeHeader(envelope);
            RequestMethod = BuildEnvelopeBody(envelope);

            var earg = new SoapCallEventArgs(RequestMethod.ToString(), envelope);
            OnCall(this, earg);
            OnCall -= XmlaCommand_OnCall;

            try
            {
                if (string.IsNullOrEmpty(((XmlaConnection) Connection).SessionID))
                {
                    var response =
                        earg.Response.Header<ResponseSessionHeader>(XName.Get("Session",
                            Namespace.ComMicrosoftSchemasXmla));
                    ((XmlaConnection) Connection).SessionID = response?.SessionId;
                }

                earg.Response.ThrowIfFaulted();

                if (earg.Exception != null)
                    throw earg.Exception;

                if (string.IsNullOrEmpty(((XmlaConnection) Connection).SessionID))
                    throw new Exception("Connection faild.");
            }
            catch (FaultException e)
            {
                throw new Exception(e.String);
            }
            catch (Exception e)
            {
                throw e;
            }


            if (earg.Exception == null)
                ((XmlaConnection) Connection)._State = ConnectionState.Open;
            else
                ((XmlaConnection) Connection)._State = ConnectionState.Broken;

            return earg.Response;
        }

        public async Task<SoapEnvelope> ExecuteAsync()
        {
            PrepareInAsync();

            var envelope = SoapEnvelope.Prepare();
            BuildEnvelopeHeader(envelope);
            RequestMethod = BuildEnvelopeBody(envelope);
            SoapEnvelope var;

            try
            {
                var = await CallAsync(RequestMethod.ToString(), envelope);

                if (string.IsNullOrEmpty(((XmlaConnection)Connection).SessionID))
                {
                    var response =
                        var.Header<ResponseSessionHeader>(XName.Get("Session",
                            Namespace.ComMicrosoftSchemasXmla));
                    ((XmlaConnection)Connection).SessionID = response?.SessionId;
                }

                var.ThrowIfFaulted();

                if (string.IsNullOrEmpty(((XmlaConnection)Connection).SessionID))
                    throw new Exception("Connection faild.");
            }
            catch (FaultException e)
            {
                ((XmlaConnection)Connection)._State = ConnectionState.Broken;
                throw e;
            }
            catch (Exception e)
            {
                ((XmlaConnection)Connection)._State = ConnectionState.Broken;
                throw e;
            }


            ((XmlaConnection)Connection)._State = ConnectionState.Open;

            return var;
        }


        /// <inheritdoc />
        public XmlReader ExecuteXmlReader()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public CellSet ExecuteCellSet()
        {
            if (((XmlaConnection) Connection)._State == ConnectionState.Open)
                ((XmlaConnection) Connection)._State = ConnectionState.Fetching;

            var cs = new CellSet(Connection as XmlaConnection,
                Execute() as SoapEnvelope);

            return cs;
        }

        public async Task<CellSet> ExecuteCellSetAsync()
        {
            if (((XmlaConnection)Connection)._State == ConnectionState.Open)
                ((XmlaConnection)Connection)._State = ConnectionState.Fetching;

            var cs = new CellSet(Connection as XmlaConnection,
                await ExecuteAsync() as SoapEnvelope);

            return cs;
        }


        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public override int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        private event SoapCallEventHandler OnCall;

        public override void Prepare()
        {
            if (Connection.State != ConnectionState.Connecting &&
                Connection.State != ConnectionState.Executing &&
                Connection.State != ConnectionState.Fetching)
                if (Connection.State == ConnectionState.Open)
                    ((XmlaConnection) Connection)._State = ConnectionState.Executing;
                else
                    throw new Exception("Before execute the connection must be opened.");

            OnCall += XmlaCommand_OnCall;

            _soapClient = SoapClient.Prepare();
            var user = ConnectionStringParser.GetUsername(Connection.ConnectionString);
            var pass = ConnectionStringParser.GetPassword(Connection.ConnectionString);

            if (!string.IsNullOrEmpty(user))
            {
                var clearTextCredentials = string.Format("{0}:{1}", user, pass == null ? "" : pass);

                _soapClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(clearTextCredentials))
                    );
            }
        }

        public virtual void PrepareInAsync()
        {
            if (Connection.State != ConnectionState.Connecting &&
                Connection.State != ConnectionState.Executing &&
                Connection.State != ConnectionState.Fetching)
                if (Connection.State == ConnectionState.Open)
                    ((XmlaConnection)Connection)._State = ConnectionState.Executing;
                else
                    throw new Exception("Before execute the connection must be opened.");

            _soapClient = SoapClient.Prepare();
            var user = ConnectionStringParser.GetUsername(Connection.ConnectionString);
            var pass = ConnectionStringParser.GetPassword(Connection.ConnectionString);

            if (!string.IsNullOrEmpty(user))
            {
                var clearTextCredentials = string.Format("{0}:{1}", user, pass == null ? "" : pass);
                _soapClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(clearTextCredentials)));
            }
        }

        private void XmlaCommand_OnCall(object sender, SoapCallEventArgs e)
        {
            //Task<SoapEnvelope> callTask = CallAsync(e.Action, e.Request);
            var callTask = Task.Run(() => CallAsync(e.Action, e.Request));
            try
            {
                //callTask.Wait();
                e.Response = callTask.GetAwaiter().GetResult(); // callTask.Result;
            }
            catch (Exception ex)
            {
                if (e.Exception != null)
                    e.Exception = ex;
            }
        }

        public void BuildEnvelopeHeader(SoapEnvelope envelope)
        {
            var sessionId = ((XmlaConnection) Connection).SessionID;
            switch (Connection.State)
            {
                case ConnectionState.Broken:
                    break;
                case ConnectionState.Closed:
                    break;
                case ConnectionState.Connecting:
                    if (string.IsNullOrEmpty(sessionId))
                        envelope.WithHeaders(new BeginSessionHeader());
                    else
                        envelope.WithHeaders(new ContinueSessionHeader(sessionId));
                    break;
                case ConnectionState.Executing:
                    if (string.IsNullOrEmpty(CommandText))
                        envelope.WithHeaders(new EndSessionHeader(sessionId));
                    else
                        envelope.WithHeaders(new ContinueSessionHeader(sessionId));
                    break;
                case ConnectionState.Fetching:
                    envelope.WithHeaders(new ContinueSessionHeader(sessionId));
                    break;
                case ConnectionState.Open:
                    envelope.WithHeaders(new ContinueSessionHeader(sessionId));
                    break;
                default:
                    break;
            }
        }

        public RequestMethod BuildEnvelopeBody(SoapEnvelope envelope)
        {
            RequestMethod = RequestMethod.Execute;

            switch (Connection.State)
            {
                case ConnectionState.Broken:
                    break;
                case ConnectionState.Closed:
                    break;
                case ConnectionState.Connecting:
                    envelope.Body(new RequestExecute());
                    break;
                case ConnectionState.Executing:
                    if (string.IsNullOrEmpty(CommandText))
                    {
                        envelope.Body(new RequestExecute());
                    }
                    else
                    {
                        envelope.Body(new RequestDiscover(CommandText, Connection, CommandRestrictions, PropertyList));
                        RequestMethod = RequestMethod.Discover;
                    }
                    break;
                case ConnectionState.Fetching:
                    envelope.Body(new RequestExecute(CommandText, Connection));
                    break;
                case ConnectionState.Open:
                    envelope.Body(new RequestDiscover(CommandText, Connection, CommandRestrictions, PropertyList));
                    RequestMethod = RequestMethod.Discover;
                    break;
                default:
                    break;
            }

            return RequestMethod;
        }

        public async Task<SoapEnvelope> CallAsync(string action, SoapEnvelope envelope)
        {
            SoapEnvelope response = null;
            using (_soapClient)
            {
                try
                {
#if DEBUG
                    var xml = _soapClient.Settings.SerializationProvider.ToXmlString(envelope);
                    Debug.WriteLine(xml);
#endif

                    response = await _soapClient.SendAsync(
                        ConnectionStringParser.GetDataSourceName(Connection.ConnectionString),
                        action, envelope);
                }
                catch (SoapEnvelopeSerializationException e)
                {
#if DEBUG
                    Debug.WriteLine("Failed to serialize the SOAP Envelope: " + e.Message);
#endif
                    throw e;
                }
                catch (SoapEnvelopeDeserializationException e)
                {
#if DEBUG
                    Debug.WriteLine("Failed to deserialize the response into a SOAP Envelope: " + e.Message);
#endif
                    throw e;
                }
                catch (Exception e)
                {
#if DEBUG
                    Debug.WriteLine("General exception: " + e.Message);
#endif
                    throw e;
                }
            }

            return response;
        }
    }
}