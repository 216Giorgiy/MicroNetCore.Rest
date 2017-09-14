namespace MicroNetCore.Rest.MediaTypes.Xml
{
    public sealed class XmlSerializer : IXmlSerializer
    {
        public string Serialize(object obj)
        {
            return new XmlSerializer().Serialize(obj);
        }
    }
}