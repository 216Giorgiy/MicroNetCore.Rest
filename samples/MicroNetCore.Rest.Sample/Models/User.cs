using MicroNetCore.Models;
using MicroNetCore.Models.Markup;
using MicroNetCore.Rest.Hypermedia.Attributes;

namespace MicroNetCore.Rest.Sample.Models
{
    [Title("User")]
    public sealed class User : IModel
    {
        [Edit]
        public string Name { get; set; } = "Some User";

        [Edit]
        public long RoleId { get; set; } = 1;

        public Role Role { get; set; } = new Role();
        public long Id { get; set; } = 1;
    }
}