using MicroNetCore.Rest.MediaTypes.Attributes;

namespace MicroNetCore.Rest.MediaTypes.Json
{
    [Encodings(EncodingCode.Utf8)]
    [MediaTypes("application/json")]
    public sealed class JsonOutputFormatter : MediaTypeOutputFormatter<IJsonSerializer>
    {
    }
}