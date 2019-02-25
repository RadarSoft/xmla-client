namespace RadarSoft.RadarCube.XmlaClient
{
	using System;
	using System.Data;

	/// <summary>
	/// Interface for AdomdDataAdapter.
	/// </summary>
	public interface IXmlaDataAdapter : IDisposable
	{
		/// <summary>
		/// Gets or sets a command used to select records in the data source.
		/// </summary>
		/// <returns>An IXmlaCommand.</returns>
		IXmlaCommand SelectCommand { get; }

		/// <summary>
		/// Indicates or specifies whether unmapped source tables or columns are passed with their source names in order to be filtered or to raise an error.
		/// </summary>
		/// <returns>One of the <see cref="T:System.Data.MissingMappingAction" /> values. The default is Passthrough.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingMappingAction" /> values. </exception>
		MissingMappingAction MissingMappingAction
		{
			get;
			set;
		}

		/// <summary>
		/// Indicates or specifies whether missing source tables, columns, and their relationships are added to the dataset schema, ignored, or cause an error to be raised.
		/// </summary>
		/// <returns>One of the <see cref="T:System.Data.MissingSchemaAction" /> values. The default is Add.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingSchemaAction" /> values. </exception>
		MissingSchemaAction MissingSchemaAction
		{
			get;
			set;
		}

		/// <summary>
		/// Indicates how a source table is mapped to a dataset table.
		/// </summary>
		/// <returns>A collection that provides the master mapping between the returned records and the <see cref="T:System.Data.DataSet" />. The default value is an empty collection.</returns>
		ITableMappingCollection TableMappings
		{
			get;
		}

		/// <summary>
		/// Configures the schema of the specified <see cref="T:System.Data.DataTable" /> based on the specified <see cref="T:System.Data.SchemaType" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information returned from the data source.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		DataTable FillSchema(DataTable dataTable, SchemaType schemaType);

		/// <summary>
		/// Adds a <see cref="T:System.Data.DataTable" /> named "Table" to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.
		/// </summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to insert the schema in. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values that specify how to insert the schema. </param>
		DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType);

		/// <summary>
		/// Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based upon the specified <see cref="T:System.Data.SchemaType" /> and <see cref="T:System.Data.DataTable" />.
		/// </summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to insert the schema in. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values that specify how to insert the schema. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.ArgumentException">A source table from which to get the schema could not be found. </exception>
		DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable);

		/// <summary>
		/// Adds or refreshes rows in the <see cref="T:System.Data.DataSet" />.
		/// </summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		int Fill(DataSet dataSet);

		/// <summary>
		/// Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.
		/// </summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.SystemException">The source table is invalid. </exception>
		int Fill(DataSet dataSet, string srcTable);

		/// <summary>
		/// Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.
		/// </summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.SystemException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid.-or- The connection is invalid. </exception>
		/// <exception cref="T:System.InvalidCastException">The connection could not be found. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="startRecord" /> parameter is less than 0.-or- The <paramref name="maxRecords" /> parameter is less than 0. </exception>
		int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable);

		/// <summary>
		/// Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataTable" /> name.
		/// </summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">The name of the <see cref="T:System.Data.DataTable" /> to use for table mapping. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		int Fill(DataTable dataTable);

		/// <summary>
		/// Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in the data source starting at the specified record and retrieving up to the specified maximum number of records.
		/// </summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This value does not include rows affected by statements that do not return rows.</returns>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="dataTables">The <see cref="T:System.Data.DataTable" /> objects to fill from the data source.</param>
		int Fill(int startRecord, int maxRecords, params DataTable[] dataTables);

		/// <summary>
		/// Gets the parameters set by the user when executing an SQL SELECT statement.
		/// </summary>
		/// <returns>An array of <see cref="T:System.Data.IDataParameter" /> objects that contains the parameters set by the user.</returns>
		IDataParameter[] GetFillParameters();
	}
}
