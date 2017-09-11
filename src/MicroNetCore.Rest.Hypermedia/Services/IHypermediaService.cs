using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaService
    {
        Entity Create<TModel>(TModel model)
            where TModel : class, IModel;

        Entity Create<TModel>(ICollection<TModel> models)
            where TModel : class, IModel;

        Entity Create<TModel>(Page<TModel> page)
            where TModel : class, IModel;
    }
}