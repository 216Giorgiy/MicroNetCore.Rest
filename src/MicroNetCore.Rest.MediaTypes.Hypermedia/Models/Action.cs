using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Models
{
    [DataContract]
    public abstract class Action
    {
        // Required
        [DataMember(Order = 1)]
        public string Name { get; set; }

        // Optional
        [DataMember(Order = 2)]
        public string Title { get; set; }

        // Optional
        [DataMember(Order = 3)]
        public string Method { get; set; }

        // Required
        [DataMember(Order = 4)]
        public string Href { get; set; }

        // Optional
        [DataMember(Order = 5)]
        public string Type { get; set; }

        // Optional
        [DataMember(Order = 6)]
        public IEnumerable<Field> Fields { get; set; }

        // Optional
        [DataMember(Order = 7)]
        public string Class { get; set; }
    }
}