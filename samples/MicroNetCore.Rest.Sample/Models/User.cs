using System.Collections.Generic;
using System.Runtime.Serialization;
using MicroNetCore.Models;
using MicroNetCore.Models.Markup.Attributes;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Attributes;

namespace MicroNetCore.Rest.Sample.Models
{
    [Title("User")]
    [DataContract]
    public sealed class User : IEntityModel
    {
        [DataMember]
        [Add]
        [Edit]
        [Show]
        public string Name { get; set; } = "User1";

        [DataMember]
        [Show]
        public ICollection<UserRole> Roles { get; set; }

        [DataMember]
        [Show]
        public long Id { get; set; } = 1;

        public User()
        {
            Roles = new List<UserRole>
            {
                new UserRole {Entity1 = this, Entity1Id = 1, Entity2 = new Role {Id = 1, Name = "Test1"}},
                new UserRole {Entity1 = this, Entity1Id = 1, Entity2 = new Role {Id = 2, Name = "Test2"}}
            };
        }
    }
}