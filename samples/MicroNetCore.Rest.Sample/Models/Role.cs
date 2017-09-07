using System.Runtime.Serialization;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Attributes;

namespace MicroNetCore.Rest.Sample.Models
{
    [Title("Role")]
    [DataContract]
    public sealed class Role : IModel
    {
        [DataMember]
        public string Name { get; set; } = "Some Role";

        [DataMember]
        public long Id { get; set; } = 1;
    }
}