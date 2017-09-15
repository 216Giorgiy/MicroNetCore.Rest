using MicroNetCore.Rest.MediaTypes.Attributes;

namespace MicroNetCore.Rest.MediaTypes.Xml
{
    [Encodings(EncodingCode.Utf8)]
    [MediaTypes("application/xml")]
    public sealed class XmlOutputFormatter : MediaTypeOutputFormatter<IXmlSerializer>
    {
    }
}