using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaActionsGenerator
    {
        Action[] Generate<TModel>(TModel model)
            where TModel : class, IModel;
    }
}