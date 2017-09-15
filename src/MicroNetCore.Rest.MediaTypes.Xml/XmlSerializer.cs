using System.IO;
using System.Text;
using System.Xml;
using MicroNetCore.Rest.DataTransferObjects;

namespace MicroNetCore.Rest.MediaTypes.Xml
{
    public sealed class XmlSerializer : IXmlSerializer
    {
        public string Serialize(RestObject restObject, Encoding encoding)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(restObject.Object.GetType());

            using (var stringWriter = new StringWriter())
            {
                var xmlSettings = new XmlWriterSettings {Encoding = encoding};

                using (var xmlWriter = XmlWriter.Create(stringWriter, xmlSettings))
                {
                    serializer.Serialize(xmlWriter, restObject.Object);
                    return stringWriter.ToString();
                }
            }
        }
    }
}