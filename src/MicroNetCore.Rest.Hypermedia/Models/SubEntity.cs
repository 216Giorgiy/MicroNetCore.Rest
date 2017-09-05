using System.Collections.Generic;
using Newtonsoft.Json;

namespace MicroNetCore.Rest.Hypermedia.Models
{
    public abstract class SubEntity
    {
        // Optional
        [JsonProperty(Order = 1)]
        public string[] Class { get; set; }

        // Required
        [JsonProperty(Order = 2)]
        public string[] Rel { get; set; }

        // Optional
        [JsonProperty(Order = int.MaxValue)]
        public string Title { get; set; }
    }

    public sealed class EmbeddedLink : SubEntity
    {
        // Required
        [JsonProperty(Order = 3)]
        public string Href { get; set; }

        // Optional
        [JsonProperty(Order = 4)]
        public string Type { get; set; }
    }

    public sealed class EmbeddedRepresentation : SubEntity
    {
        // Optional
        [JsonProperty(Order = 3)]
        public IDictionary<string, object> Properties { get; set; }

        // Optional
        [JsonProperty(Order = 4)]
        public SubEntity[] Entities { get; set; }

        // Optional
        [JsonProperty(Order = 5)]
        public Action[] Actions { get; set; }

        // Optional
        [JsonProperty(Order = 6)]
        public Link[] Links { get; set; }
    }
}