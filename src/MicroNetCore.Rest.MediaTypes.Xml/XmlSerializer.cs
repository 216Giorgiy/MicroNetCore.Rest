using System.IO;
using System.Text;
using System.Xml;

namespace MicroNetCore.Rest.MediaTypes.Xml
{
    public sealed class XmlSerializer : IXmlSerializer
    {
        public string Serialize(object obj, Encoding encoding)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());

            using (var stringWriter = new StringWriter())
            {
                var xmlSettings = new XmlWriterSettings {Encoding = encoding};

                using (var xmlWriter = XmlWriter.Create(stringWriter, xmlSettings))
                {
                    serializer.Serialize(xmlWriter, obj);
                    return stringWriter.ToString();
                }
            }
        }
    }
}