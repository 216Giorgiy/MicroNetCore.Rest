using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MicroNetCore.Rest.Hypermedia.Models
{
    [DataContract]
    [KnownType(typeof(EmbeddedLink))]
    [KnownType(typeof(EmbeddedRepresentation))]
    public abstract class SubEntity
    {
        // Optional
        [DataMember(Order = 1)]
        public string[] Class { get; set; }

        // Required
        [DataMember(Order = 2)]
        public string[] Rel { get; set; }

        // Optional
        [DataMember(Order = int.MaxValue)]
        public string Title { get; set; }
    }

    [DataContract]
    public sealed class EmbeddedLink : SubEntity
    {
        // Required
        [DataMember(Order = 3)]
        public string Href { get; set; }

        // Optional
        [DataMember(Order = 4)]
        public string Type { get; set; }
    }

    [DataContract]
    public sealed class EmbeddedRepresentation : SubEntity
    {
        // Optional
        [DataMember(Order = 3)]
        public IDictionary<string, object> Properties { get; set; }

        // Optional
        [DataMember(Order = 4)]
        public SubEntity[] Entities { get; set; }

        // Optional
        [DataMember(Order = 5)]
        public Action[] Actions { get; set; }

        // Optional
        [DataMember(Order = 6)]
        public Link[] Links { get; set; }
    }
}