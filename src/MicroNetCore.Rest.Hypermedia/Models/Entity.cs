using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MicroNetCore.Rest.Hypermedia.Models
{
    [DataContract]
    public sealed class Entity
    {
        // Optional
        [DataMember(Order = 1)]
        public string[] Class { get; set; }

        // Optional
        [DataMember(Order = 2)]
        public IDictionary<string, object> Properties { get; set; }

        // Optional
        [DataMember(Order = 3)]
        public SubEntity[] Entities { get; set; }

        // Optional
        [DataMember(Order = 4)]
        public Action[] Actions { get; set; }

        // Optional
        [DataMember(Order = 5)]
        public Link[] Links { get; set; }

        // Optional
        [DataMember(Order = 6)]
        public string Title { get; set; }
    }
}