using System.Collections.Generic;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaPropertiesGenerator
    {
        IDictionary<string, object> Generate<TModel>(TModel model)
            where TModel : class, IModel;
    }
}