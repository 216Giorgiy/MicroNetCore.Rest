using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Attributes;

namespace MicroNetCore.Rest.Sample.Models
{
    [Title("Role")]
    public sealed class Role : IModel
    {
        public string Name { get; set; } = "Some Role";
        public long Id { get; set; } = 1;
    }
}