using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaClassGenerator
    {
        string[] Generate<TModel>(TModel model)
            where TModel : class, IModel;

        string[] Generate<TModel>(ICollection<TModel> models)
            where TModel : class, IModel;

        string[] Generate<TModel>(Page<TModel> page)
            where TModel : class, IModel;
    }
}