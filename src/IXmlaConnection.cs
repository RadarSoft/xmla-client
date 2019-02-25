namespace RadarSoft.RadarCube.XmlaClient
{
    using System;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// Interface for AdomdConnection.
    /// </summary>
    public interface IXmlaConnection : IDbConnection
    {
        /// <summary>
        /// Creates a new IXmlaCommand associated with this connection and returns its IAdoCommand object.
        /// </summary>
        /// <returns>The IAdoCommand object.</returns>
        new DbCommand CreateCommand();

        /// <summary>
        /// Closes the connection to the database and ends the session.
        /// </summary>
        /// <param name="endSession">A Boolean that indicates whether to end the session associated with the AdomdConnection.</param>
        /// <remarks>
        /// If the Close method is called without specifying the value of the endSession parameter, or if the endSession parameter is set to true, both the connection and the session associated with the connection are closed. If the Close method is called with the endSession parameter set to false, the session associated with the AdomdConnection remains active, but the connection is closed.
        /// If the Dispose method is called with the disposing parameter set to true, the Close method is called without specifying the value of the endSession parameter.
        /// You can reconnect to an active session after calling this method by setting the SessionID property to a valid active session ID and then calling the Open method.
        /// </remarks>
        void Close(bool endSession);

        /// <summary>
        /// Gets an instance of a CubeCollection that represents the collection of cubes contained by an analytical data source.
        /// </summary>
        CubeCollection Cubes { get; }

        /// <summary>
        /// Gets an instance of a MiningModelCollection that represents the collection of mining models that an analytical data source contains.
        /// </summary>
        MiningModelCollection MiningModels { get; }

        /// <summary>
        /// Gets an instance of a MiningStructureCollection that represents the collection of mining structures that an analytical data source contains.
        /// </summary>
        MiningStructureCollection MiningStructures { get; }

        /// <summary>
        /// Gets an instance of a MiningServiceCollection that represents the collection of mining services that an analytical data source contains.
        /// </summary>
        MiningServiceCollection MiningServices { get; }

        /// <summary>
        /// Gets the version of the XML for Analysis provider that the AdomdConnection uses.
        /// </summary>
        string ProviderVersion { get; }

        /// <summary>
        /// Gets the version of the ADOMD.NET client that implements the AdomdConnection.
        /// </summary>
        string ClientVersion { get; }

        /// <summary>
        /// Gets the version of the server used that the AdomdConnection uses.
        /// </summary>
        string ServerVersion { get; }

        /// <summary>
        /// Gets or sets the string identifier of the session that the AdomdConnection opened with the server.
        /// </summary>
        string SessionID { get; }

        /// <summary>
        /// Gets or sets a value that indicates whether hidden objects are returned.
        /// </summary>
        bool ShowHiddenObjects { get; }

        /// <summary>
        /// Returns schema information from a data source by using a Guid object to specify which schema information to return and by applying any specified restrictions to the information.
        /// </summary>
        /// <param name="schema">A Guid object that specifies the schema table to be returned.</param>
        /// <param name="restrictions">An array of Object objects that specifies the values for the restriction columns that the schema table uses. These values are applied in the order of the restriction columns. That is, the first restriction value applies to the first restriction column, the second restriction value applies to the second restriction column, and so on.</param>
        /// <returns>A DataSet that represents the contents of the specified OLE DB schema rowset.</returns>
        DataSet GetSchemaDataSet(Guid schema, object[] restrictions);

        /// <summary>
        /// Returns schema information from a data source by using a schema name to identify which schema to retrieve and by applying any specified restrictions to the information.
        /// </summary>
        /// <param name="schemaName">The name of the schema to be retrieved.</param>
        /// <param name="restrictions">A collection of AdomdRestriction objects that specifies the values for the restriction columns used by the schema table.</param>
        /// <returns>A DataSet that represents the contents of the specified OLE DB schema table.</returns>
        /// <remarks>The schema table is returned as a DataSet that has the same format as the OLE DB schema rowset specified by the schemaName parameter. Use the restrictions parameter to filter the rows to be returned in the DataSet. (For example, you can specify cube name and dimension name restrictions for the dimensions schema.)</remarks>
        DataSet GetSchemaDataSet(string schemaName, AdomdRestrictionCollection restrictions);

        /// <summary>
        /// Returns schema information from a data source by using a Guid object to identify the information, applying any specified restrictions on the information, and optionally throwing an exception when inline errors occur.
        /// </summary>
        /// <param name="schema">A Guid object that specifies the schema table to be returned.</param>
        /// <param name="restrictions">An array of Object objects that specifies the values for the restriction columns that are used by the schema table. These values are applied in the order of the restriction columns. That is, the first restriction value applies to the first restriction column, the second restriction value applies to the second restriction column, and so on.</param>
        /// <param name="throwOnInlineErrors">If true, inline errors cause an exception to be thrown; otherwise DataRow.GetColumnError is used to determine the error generated.</param>
        /// <returns>A DataSet that represents the contents of the specified OLE DB schema rowset.</returns>
        /// <remarks>
        /// If throwOnInlineErrors is true, this method behaves identically to GetSchemaDataSet.If throwOnInlineErrors is false, and there are errors that occur while retrieving schema information, the resulting DataSet may contain null cells that would not normally be null. To determine the details of the errors that occur, you can use the DataRow.GetColumnsInError, DataRow.GetColumnError, DataRow.HasErrors, DataSet.HasErrors, and DataTable.HasErrors methods and properties.
        /// </remarks>
        DataSet GetSchemaDataSet(Guid schema, object[] restrictions, bool throwOnInlineErrors);

        /// <summary>
        /// Returns schema information from a data source by using a schema name to identify the information, applying any specified restrictions to the information, and optionally throwing an exception when inline errors occur.
        /// </summary>
        /// <param name="schema">The name of the schema to be retrieved.</param>
        /// <param name="restrictions">A collection of AdomdRestriction objects that specifies the values for the restriction columns that the schema table uses.</param>
        /// <param name="throwOnInlineErrors">If true, inline errors cause an exception to be thrown; otherwise, DataRow.GetColumnError is used to determine the error generated.</param>
        /// <returns>A DataSet that represents the contents of the specified OLE DB schema table.</returns>
        /// <remarks>
        /// If throwOnInlineErrors is true, this method behaves identically to GetSchemaDataSet. If throwOnInlineErrors is false, and there are errors that occur while retrieving schema information, the resulting DataSet may contain null cells that would not normally be null. To determine the details of the errors that occur, you can use the DataRow.GetColumnsInError, DataRow.GetColumnError, DataRow.HasErrors, DataSet.HasErrors, and DataTable.HasErrors methods and properties.
        /// </remarks>
        DataSet GetSchemaDataSet(string schema, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors);

        /// <summary>
        /// Returns schema information from a data source by using a schema name and namespace to identify the information, and by applying any specified restrictions to the information.
        /// </summary>
        /// <param name="schemaName">The name of the schema to be retrieved.</param>
        /// <param name="schemaNamespace">The name of the schema namespace to be retrieved.</param>
        /// <param name="restrictions">A collection of AdomdRestriction objects that specifies the values for the restriction columns that the schema table uses.</param>
        /// <returns>A DataSet that represents the contents of the specified OLE DB schema table.</returns>
        /// <remarks>
        /// If throwOnInlineErrors is true, this method behaves identically to GetSchemaDataSet. If throwOnInlineErrors is false, and errors occur while retrieving schema information, the resulting DataSet may contain null cells that would not normally be null. To determine the details of the errors that occur, you can use the DataRow.GetColumnsInError, DataRow.GetColumnError, DataRow.HasErrors, DataSet.HasErrors, and DataTable.HasErrors methods and properties.
        /// </remarks>
        DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions);

        /// <summary>
        /// Returns schema information from a data source by using a schema name and namespace to identify the information, applying any specified restrictions to the information, and, optionally throwing an exception when inline errors occur.
        /// </summary>
        /// <param name="schemaName">The name of the schema to be retrieved.</param>
        /// <param name="schemaNamespace">The name of the schema namespace to be retrieved.</param>
        /// <param name="restrictions">A collection of AdomdRestriction objects that specifies the values for the restriction columns that the schema table uses.</param>
        /// <param name="throwOnInlineErrors">If true, inline errors cause an exception to be thrown; otherwise DataRow.GetColumnError is used to determine the error generated.</param>
        /// <returns>A DataSet that represents the contents of the specified OLE DB schema table.</returns>
        /// <remarks>
        /// If throwOnInlineErrors is true, this method behaves identically to GetSchemaDataSet. If throwOnInlineErrors is false, and errors occur while retrieving schema information, the resulting DataSet may contain null cells that would not normally be null. To determine the details of the errors that occur, you can use the DataRow.GetColumnsInError, DataRow.GetColumnError, DataRow.HasErrors, DataSet.HasErrors, and DataTable.HasErrors methods and properties.
        /// </remarks>
        DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors);

        /// <summary>
        /// Forces the connection to repopulate all metadata from the server.
        /// </summary>
        void RefreshMetadata();
    }
}
