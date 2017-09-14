namespace MicroNetCore.Rest.MediaTypes.Json
{
    public sealed class JsonOutputFormatter : MediaTypeOutputFormatter<JsonSerializer>
    {
        private const string MediaTypeName = "application/json";

        public JsonOutputFormatter()
            : base(MediaTypeName)
        {
        }
    }
}