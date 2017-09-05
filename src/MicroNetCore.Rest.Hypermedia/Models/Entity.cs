using System.Collections.Generic;
using Newtonsoft.Json;

namespace MicroNetCore.Rest.Hypermedia.Models
{
    public sealed class Entity
    {
        // Optional
        [JsonProperty(Order = 1)]
        public string[] Class { get; set; }

        // Optional
        [JsonProperty(Order = 2)]
        public IDictionary<string, object> Properties { get; set; }

        // Optional
        [JsonProperty(Order = 3)]
        public SubEntity[] Entities { get; set; }

        // Optional
        [JsonProperty(Order = 4)]
        public Action[] Actions { get; set; }

        // Optional
        [JsonProperty(Order = 5)]
        public Link[] Links { get; set; }

        // Optional
        [JsonProperty(Order = 6)]
        public string Title { get; set; }
    }
}