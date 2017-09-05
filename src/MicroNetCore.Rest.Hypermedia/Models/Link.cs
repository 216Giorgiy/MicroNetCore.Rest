using Newtonsoft.Json;

namespace MicroNetCore.Rest.Hypermedia.Models
{
    public sealed class Link
    {
        // Required
        [JsonProperty(Order = 1)]
        public string[] Rel { get; set; }

        // Required
        [JsonProperty(Order = 2)]
        public string Href { get; set; }

        // Optional
        [JsonProperty(Order = 3)]
        public string Title { get; set; }

        // Optional
        [JsonProperty(Order = 4)]
        public string Type { get; set; }

        // Optional
        [JsonProperty(Order = 5)]
        public string Class { get; set; }
    }
}