using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using RadarSoft.XmlaClient.Metadata;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient
{
    public static class Namespace
    {
        /// <summary>
        ///     Microsoft XML/A namespace
        /// </summary>
        public const string ComMicrosoftSchemasXmla =
            "urn:schemas-microsoft-com:xml-analysis";

        public const string ComMicrosoftSchemasXmlaRowset =
            "urn:schemas-microsoft-com:xml-analysis:rowset";

        public const string ComMicrosoftSchemasXmlaMddataset =
            "urn:schemas-microsoft-com:xml-analysis:mddataset";
    }

    public static class XmlaPropertiesNames
    {
        public const string TupleAxis_TupleFormat = "TupleFormat";
        public const string Format_Tabular = "Tabular";
        public const string Format_Multidimensional = "Multidimensional";
        public const string Content_Data = "Data";
    }

    /// <summary>
    ///     Helper methods for working with <see cref="SoapEnvelope" /> instances.
    /// </summary>
    public static class EnvelopeHelpers
    {
        public static IEnumerable<XElement> GetXRows(this SoapEnvelope envelope)
        {
            return envelope.Body.Value.Descendants(XName.Get("row", Namespace.ComMicrosoftSchemasXmlaRowset));
        }

        public static bool MamberUNameIs(this XElement xElement, string uniqName)
        {
            return xElement.Descendants(XName.Get("MEMBER_UNIQUE_NAME", Namespace.ComMicrosoftSchemasXmlaRowset))
                .FirstOrDefault()?.Value == uniqName;
        }

        public static string GetMamberUName(this XElement xElement)
        {
            return xElement.Descendants(XName.Get("MEMBER_UNIQUE_NAME", Namespace.ComMicrosoftSchemasXmlaRowset))
                       .FirstOrDefault()?.Value;
        }

        public static XElement GetXOlapInfo(this SoapEnvelope envelope)
        {
            return envelope.Body.Value.Descendants(XName.Get("OlapInfo", Namespace.ComMicrosoftSchemasXmlaMddataset))
                .FirstOrDefault();
        }

        public static IEnumerable<XElement> GetXCubes(XElement olapInfo)
        {
            return olapInfo.Descendants(XName.Get("Cube", Namespace.ComMicrosoftSchemasXmlaMddataset));
        }

        public static IEnumerable<XElement> GetXAxes(this SoapEnvelope envelope)
        {
            return envelope.Body.Value.Descendants(XName.Get("Axis", Namespace.ComMicrosoftSchemasXmlaMddataset));
        }

        public static IEnumerable<XElement> GetXCells(this SoapEnvelope envelope)
        {
            return envelope.Body.Value.Descendants(XName.Get("Cell", Namespace.ComMicrosoftSchemasXmlaMddataset));
        }

        public static IEnumerable<XElement> GetXTuples(XElement axis)
        {
            return axis.Descendants(XName.Get("Tuple", Namespace.ComMicrosoftSchemasXmlaMddataset));
        }

        public static IEnumerable<XElement> GetXMembers(XElement tuple)
        {
            return tuple.Descendants(XName.Get("Member", Namespace.ComMicrosoftSchemasXmlaMddataset));
        }
    }

    /// <summary>
    ///     Helper class with extensions for XML manipulation
    /// </summary>
    public static class XmlHelpers
    {
        /// <summary>
        ///     Deserializes a given XML string to a new object of the expected type.
        ///     If null or white spaces the default(T) will be returned;
        /// </summary>
        /// <typeparam name="T">The type to be deserializable</typeparam>
        /// <param name="xml">The XML string to deserialize</param>
        /// <returns>The deserialized object</returns>
        public static T ToObject<T>(this string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return default(T);

            using (var textWriter = new StringReader(xml))
            {
                var result = (T) new XmlSerializer(typeof(T)).Deserialize(textWriter);

                return result;
            }
        }

        //public static async Task<T> ToXmlaObjectAsync<T>(this string xml)
        //{
        //    return await  Task.Run(() =>
        //        {
        //            if (string.IsNullOrWhiteSpace(xml)) return default(T);

        //            using (var textWriter = new StringReader(xml))
        //            {
        //                var result = (T)new XmlSerializer(typeof(T)).Deserialize(textWriter);

        //                return result;
        //            }
        //        });
        //}

        /// <summary>
        ///     Deserializes a given <see cref="XElement" /> to a new object of the expected type.
        ///     If null the default(T) will be returned.
        /// </summary>
        /// <typeparam name="T">The type to be deserializable</typeparam>
        /// <param name="xml">The <see cref="XElement" /> to deserialize</param>
        /// <returns>The deserialized object</returns>
        public static T ToObject<T>(this XElement xml, IXmlaBaseObject owner = null) where T : IXmlaBaseObject
        {
            var xstr = xml.ToString();
            if (string.IsNullOrWhiteSpace(xstr)) return default(T);

            using (var textWriter = new StringReader(xstr))
            {
                var result = (T)new XmlSerializer(typeof(T)).Deserialize(textWriter);

                result.SoapRow = xml;
                if (owner != null)
                {
                    result.Connection = owner.Connection;
                    result.CubeName = owner.CubeName;
                    result.ParentObject = owner;
                }

                return result;
            }
        }

        public static async Task<T> ToXmlaObjectAsync<T>(this XElement xml, IXmlaBaseObject owner = null) where T : IXmlaBaseObject
        {
            return await Task.Run(() =>
                                  {
                                      var xstr = xml.ToString();
                                      if (string.IsNullOrWhiteSpace(xstr)) return default(T);

                                      using (var textWriter = new StringReader(xstr))
                                      {
                                          var result = (T)new XmlSerializer(typeof(T)).Deserialize(textWriter);

                                          result.SoapRow = xml;
                                          if (owner != null)
                                          {
                                              result.Connection = owner.Connection;
                                              result.CubeName = owner.CubeName;
                                              result.ParentObject = owner;
                                          }
                                          return result;
                                      }
                                  });
        }

    }
}