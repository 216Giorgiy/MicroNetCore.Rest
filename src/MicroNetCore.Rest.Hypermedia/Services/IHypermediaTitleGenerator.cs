using MicroNetCore.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaTitleGenerator
    {
        string Generate<TModel>(TModel model)
            where TModel : class, IModel;
    }
}