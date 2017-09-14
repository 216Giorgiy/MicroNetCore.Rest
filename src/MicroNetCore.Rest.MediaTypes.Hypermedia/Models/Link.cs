using System.Runtime.Serialization;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Models
{
    [DataContract]
    public sealed class Link
    {
        // Required
        [DataMember(Order = 1)]
        public string[] Rel { get; set; }

        // Required
        [DataMember(Order = 2)]
        public string Href { get; set; }

        // Optional
        [DataMember(Order = 3)]
        public string Title { get; set; }

        // Optional
        [DataMember(Order = 4)]
        public string Type { get; set; }

        // Optional
        [DataMember(Order = 5)]
        public string Class { get; set; }
    }
}