using System.Collections.Generic;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaPropertiesGenerator
    {
        IDictionary<string, object> Generate<TModel>(TModel model)
            where TModel : class, IModel;

        IDictionary<string, object> Generate<TModel>(ICollection<TModel> models)
            where TModel : class, IModel;

        IDictionary<string, object> Generate<TModel>(IPageCollection<TModel> page)
            where TModel : class, IModel;
    }
}