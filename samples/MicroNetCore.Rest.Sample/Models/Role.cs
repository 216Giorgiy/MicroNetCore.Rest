using System.Runtime.Serialization;
using MicroNetCore.Models;
using MicroNetCore.Models.Markup.Attributes;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Attributes;

namespace MicroNetCore.Rest.Sample.Models
{
    [Title("Role")]
    [DataContract]
    public sealed class Role : IModel
    {
        [DataMember]
        [Show]
        public string Name { get; set; } = "Some Role";

        [DataMember]
        [Show]
        public long Id { get; set; } = 1;
    }
}