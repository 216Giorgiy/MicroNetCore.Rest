using System.Collections.Generic;
using System.Linq;
using MicroNetCore.Data.Abstractions;
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

        public IDictionary<string, object> Generate<TModel>(ICollection<TModel> models)
            where TModel : class, IModel
        {
            return new Dictionary<string, object>();
        }

        public IDictionary<string, object> Generate<TModel>(IPageCollection<TModel> page)
            where TModel : class, IModel
        {
            return new Dictionary<string, object>
            {
                {nameof(page.PageCount), page.PageCount},
                {nameof(page.PageIndex), page.PageIndex},
                {nameof(page.PageSize), page.PageSize}
            };
        }
    }
}