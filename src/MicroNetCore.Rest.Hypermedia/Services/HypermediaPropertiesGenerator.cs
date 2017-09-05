using System.Collections.Generic;
using System.Linq;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaPropertiesGenerator : IHypermediaPropertiesGenerator
    {
        public IDictionary<string, object> Generate<TModel>(TModel model)
            where TModel : class, IModel
        {
            return typeof(TModel)
                .GetProperties()
                .Where(p => !HypermediaService.IsSubEntityType(p.PropertyType))
                .ToDictionary(p => p.Name, p => p.GetValue(model));
        }
    }
}