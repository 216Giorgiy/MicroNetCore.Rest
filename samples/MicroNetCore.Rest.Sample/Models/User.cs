using MicroNetCore.Models;
using MicroNetCore.Models.Markup;

namespace MicroNetCore.Rest.Sample.Models
{
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