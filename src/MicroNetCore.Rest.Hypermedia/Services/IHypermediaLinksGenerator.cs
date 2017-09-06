using System.Collections.Generic;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaLinksGenerator
    {
        Link[] Generate<TModel>(TModel model)
            where TModel : class, IModel;

        Link[] Generate<TModel>(ICollection<TModel> models)
            where TModel : class, IModel;

        Link[] Generate<TModel>(IPageCollection<TModel> page)
            where TModel : class, IModel;
    }
}