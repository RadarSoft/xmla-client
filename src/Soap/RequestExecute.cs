using System.Data.Common;
using System.Xml.Serialization;

namespace RadarSoft.XmlaClient.Soap
{
    [XmlRoot("Execute", Namespace = Namespace.ComMicrosoftSchemasXmla)]
    public class RequestExecute
    {
        public RequestExecute()
        {
            Command = new RequestCommand();
            Properties = new Properties();
        }

        public RequestExecute(string statement, DbConnection connection)
        {
            Command = new RequestCommand {Statement = statement};
            Properties = new Properties
                         {
                             PropertyList = new PropertyList
                                            {
                                                Format = "Multidimensional",
                                                AxisFormat = "TupleFormat",
                                                ShowHiddenCubes = true,
                                                Content = "",
                                                Catalog = ConnectionStringParser.GetDatabaseName(connection
                                                    .ConnectionString)
                                            }
                         };
        }

        [XmlElement("Command")]
        public RequestCommand Command { get; set; }

        [XmlElement("Properties")]
        public Properties Properties { get; set; }
    }
}