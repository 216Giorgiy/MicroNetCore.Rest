using System.Collections.Generic;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaService
    {
        Entity Create<TModel>(TModel model)
            where TModel : class, IModel, new();

        Entity Create<TModel>(ICollection<TModel> models)
            where TModel : class, IModel, new();

        Entity Create<TModel>(IPageCollection<TModel> page)
            where TModel : class, IModel, new();
    }
}