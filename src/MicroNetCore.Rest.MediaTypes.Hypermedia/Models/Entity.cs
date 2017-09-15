using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Models
{
    [DataContract]
    public sealed class Entity
    {
        // Optional
        [DataMember(Order = 1)]
        public IEnumerable<string> Class { get; set; }

        // Optional
        [DataMember(Order = 2)]
        public IDictionary<string, object> Properties { get; set; }

        // Optional
        [DataMember(Order = 3)]
        public IEnumerable<SubEntity> Entities { get; set; }

        // Optional
        [DataMember(Order = 4)]
        public IEnumerable<Action> Actions { get; set; }

        // Optional
        [DataMember(Order = 5)]
        public IEnumerable<Link> Links { get; set; }

        // Optional
        [DataMember(Order = 6)]
        public string Title { get; set; }
    }
}