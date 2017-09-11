using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
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

        Link[] Generate<TModel>(Page<TModel> page)
            where TModel : class, IModel;
    }
}