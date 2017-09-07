using System.Runtime.Serialization;
using MicroNetCore.Models;
using MicroNetCore.Models.Markup;
using MicroNetCore.Rest.Hypermedia.Attributes;

namespace MicroNetCore.Rest.Sample.Models
{
    [Title("User")]
    [DataContract]
    public sealed class User : IModel
    {
        [Edit]
        [DataMember]
        public string Name { get; set; } = "Some User";

        [Edit]
        [DataMember]
        public long RoleId { get; set; } = 1;

        [DataMember]
        public Role Role { get; set; } = new Role();

        [DataMember]
        public long Id { get; set; } = 1;
    }
}