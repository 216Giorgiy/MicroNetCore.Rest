using System.Runtime.Serialization;
using MicroNetCore.Models;
using MicroNetCore.Models.Markup.Attributes;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Attributes;

namespace MicroNetCore.Rest.Sample.Models
{
    [Title("User")]
    [DataContract]
    public sealed class User : IModel
    {
        [DataMember]
        [Add]
        [Edit]
        [Show]
        public string Name { get; set; } = "Some User";

        [Add]
        [Edit]
        [Show]
        [DataMember]
        public long RoleId { get; set; } = 1;

        [DataMember]
        public Role Role { get; set; } = new Role();

        [DataMember]
        [Show]
        public long Id { get; set; } = 1;
    }
}