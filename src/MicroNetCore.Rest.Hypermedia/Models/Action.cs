using Newtonsoft.Json;

namespace MicroNetCore.Rest.Hypermedia.Models
{
    public sealed class Action
    {
        // Required
        [JsonProperty(Order = 1)]
        public string Name { get; set; }

        // Optional
        [JsonProperty(Order = 2)]
        public string Title { get; set; }

        // Optional
        [JsonProperty(Order = 3)]
        public string Method { get; set; }

        // Required
        [JsonProperty(Order = 4)]
        public string Href { get; set; }

        // Optional
        [JsonProperty(Order = 5)]
        public string Type { get; set; }

        // Optional
        [JsonProperty(Order = 6)]
        public Field[] Fields { get; set; }

        // Optional
        [JsonProperty(Order = 7)]
        public string Class { get; set; }

        public sealed class Field
        {
            // Required
            [JsonProperty(Order = 1)]
            public string Name { get; set; }

            // Optional
            [JsonProperty(Order = 2)]
            public string Title { get; set; }

            // Optional
            [JsonProperty(Order = 3)]
            public string Type { get; set; }

            // Optional
            [JsonProperty(Order = 4)]
            public object Value { get; set; }

            // Optional
            [JsonProperty(Order = 5)]
            public string[] Class { get; set; }
        }
    }
}