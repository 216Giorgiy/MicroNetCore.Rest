using MicroNetCore.Models;

namespace MicroNetCore.Rest.Sample.Models
{
    public sealed class Role : IModel
    {
        public string Name { get; set; } = "Some Role";
        public long Id { get; set; } = 1;
    }
}