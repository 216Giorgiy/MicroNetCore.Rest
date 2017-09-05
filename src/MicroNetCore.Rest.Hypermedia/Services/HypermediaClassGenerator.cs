using MicroNetCore.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaClassGenerator : IHypermediaClassGenerator
    {
        public string[] Generate<TModel>(TModel model)
            where TModel : class, IModel
        {
            return new[] {model.GetType().Name};
        }
    }
}