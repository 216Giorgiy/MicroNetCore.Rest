using MicroNetCore.Rest.MediaTypes.Attributes;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia
{
    [Encodings(EncodingCode.Utf8)]
    [MediaTypes("application/vnd.micronetcore+json")]
    public sealed class HypermediaOutputFormatter : MediaTypeOutputFormatter<IHypermediaSerializer>
    {
    }
}