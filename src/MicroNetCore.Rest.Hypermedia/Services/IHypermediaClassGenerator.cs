using MicroNetCore.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaClassGenerator
    {
        string[] Generate<TModel>(TModel model)
            where TModel : class, IModel;
    }
}