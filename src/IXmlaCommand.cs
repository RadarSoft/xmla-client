namespace RadarSoft.RadarCube.XmlaClient
{
    using System;
    using System.Data;
    using System.Xml;

    /// <summary>
    /// Interface for XmlaCommand.
    /// </summary>
    public interface IXmlaCommand : IDbCommand
    {
        /// <summary>
        /// Creates a data adapter associated with the command.
        /// Command is used as SelectCommand.
        /// </summary>
        /// <returns>The data adapter.</returns>
        IXmlaDataAdapter CreateDataAdapter();

        /// <summary>
        /// Runs the AdomdCommand, and returns either a CellSet or an AdomdDataReader.
        /// </summary>
        /// <returns>An object.</returns>
        /// <exception cref="AdomdErrorResponseException">The provider returned an error in response.</exception>
        /// <exception cref="AdomdUnknownResponseException">The provider sent an unrecognizable response.</exception>
        /// <exception cref="AdomdConnectionException">The provider sent an unrecognizable response.</exception>
        /// <exception cref="InvalidOperationException">An error occurred because one of the following conditions was met:
        ///  - The connection was not set.
        ///  - The connection was not opened.
        ///  - Either the CommandText property or the CommandStream property was improperly set.
        ///  - Both the CommandText property and the CommandStream property were set.
        ///  - Neither the CommandText property, nor the CommandStream property, was set.
        /// </exception>
        /// <remarks>
        /// The Execute method tries to run the command that is contained by the AdomdCommand. 
        /// Depending on the format of the results returned by the provider, the Execute method returns either an AdomdDataReader or a CellSet. 
        /// If the results cannot be formatted into either an AdomdDataReader or a CellSet, or if the provider returned no results, the method returns a null value.
        /// </remarks>
        object Execute();

        /// <summary>
        /// Runs the AdomdCommand and returns a CellSet.
        /// </summary>
        /// <returns>The cell set.</returns>
        /// <exception cref="AdomdErrorResponseException">The provider returned an error in response.</exception>
        /// <exception cref="AdomdUnknownResponseException">The provider sent an unrecognizable response.</exception>
        /// <exception cref="AdomdConnectionException">The provider sent an unrecognizable response.</exception>
        /// <exception cref="InvalidOperationException">An error occurred because one of the following conditions was met:
        ///  - The connection was not set.
        ///  - The connection was not opened.
        ///  - Either the CommandText property or the CommandStream property was improperly set.
        ///  - Both the CommandText property and the CommandStream property were set.
        ///  - Neither the CommandText property, nor the CommandStream property, was set.
        /// </exception>
        /// <remarks>
        /// The ExecuteCellSet method tries to run the command that is contained by the AdomdCommand and returns a CellSet that contains the results of the command.
        /// If the provider does not return a valid analytical data set, an exception is thrown.
        /// </remarks>
        CellSet ExecuteCellSet();

        /// <summary>
        /// Runs the AdomdCommand and returns an XmlReader.
        /// </summary>
        /// <returns>An XmlReaderthat contains the results of the command.</returns>
        /// <exception cref="AdomdErrorResponseException">The provider returned an error in response.</exception>
        /// <exception cref="AdomdUnknownResponseException">The provider sent an unrecognizable response.</exception>
        /// <exception cref="AdomdConnectionException">The provider sent an unrecognizable response.</exception>
        /// <exception cref="InvalidOperationException">An error occurred because one of the following conditions was met:
        ///  - The connection was not set.
        ///  - The connection was not opened.
        ///  - Either the CommandText property or the CommandStream property was improperly set.
        ///  - Both the CommandText property and the CommandStream property were set.
        ///  - Neither the CommandText property, nor the CommandStream property, was set.
        /// </exception>
        /// <remarks>
        /// Instead of translating the XML for Analysis response from an XML format into an AdomdDataReader or CellSet, this method, returns an XmlReader that directly references the XML for Analysis response in its native XML format.
        /// While the XmlReaderis in use, the associated AdomdConnection is busy serving the XmlReader. While in this state, the AdomdConnection can only be closed; no other operations can be performed on it. This remains the case until the Close method of the XmlReaderis called.
        /// You should be prepared to catch any exception that can be thrown while using the XmlReader, such as the XmlException.
        /// </remarks>
        XmlReader ExecuteXmlReader();
    }
}
