using System.Runtime.Serialization;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Models
{
    [DataContract]
    public sealed class Field
    {
        // Required
        [DataMember(Order = 1)]
        public string Name { get; set; }

        // Optional
        [DataMember(Order = 2)]
        public string Title { get; set; }

        // Optional
        [DataMember(Order = 3)]
        public string Type { get; set; }

        // Optional
        [DataMember(Order = 4)]
        public object Value { get; set; }

        // Optional
        [DataMember(Order = 5)]
        public string[] Class { get; set; }
    }
}