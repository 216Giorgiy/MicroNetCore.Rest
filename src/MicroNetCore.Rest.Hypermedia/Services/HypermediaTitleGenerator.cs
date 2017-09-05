using MicroNetCore.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaTitleGenerator : IHypermediaTitleGenerator
    {
        public string Generate<TModel>(TModel model)
            where TModel : class, IModel
        {
            return "Some Title";
        }
    }
}