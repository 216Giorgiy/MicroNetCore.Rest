using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaService
    {
        Entity Create<TModel>(TModel model)
            where TModel : class, IModel, new();
    }
}