namespace MicroNetCore.Rest.MediaTypes.Xml
{
    public sealed class XmlOutputFormatter : MediaTypeOutputFormatter<XmlSerializer>
    {
        private const string MediaTypeName = "application/xml";

        public XmlOutputFormatter()
            : base(MediaTypeName)
        {
        }
    }
}