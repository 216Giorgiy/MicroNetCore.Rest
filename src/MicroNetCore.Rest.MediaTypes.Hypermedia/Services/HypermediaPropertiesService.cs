using System;
using System.Collections.Generic;
using System.Linq;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions;
using MicroNetCore.Rest.Models.RestResults;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaPropertiesService : IHypermediaPropertiesService
    {
        #region IHypermediaPropertiesService

        public IDictionary<string, object> Get(Type type, object obj)
        {
            return type
                .GetProperties()
                .Where(p => !p.IsSubEntityType())
                .ToDictionary(p => p.Name, p => p.GetValue(obj));
        }

        public IDictionary<string, object> Get(ModelRestResult model)
        {
            return Get(model.Type, model.Model);
        }

        public IDictionary<string, object> Get(ModelsRestResult models)
        {
            return new Dictionary<string, object>();
        }

        public IDictionary<string, object> Get(PageRestResult page)
        {
            return new Dictionary<string, object>
            {
                {nameof(page.Page.PageCount), page.Page.PageCount},
                {nameof(page.Page.PageIndex), page.Page.PageIndex},
                {nameof(page.Page.PageSize), page.Page.PageSize}
            };
        }

        #endregion
    }
}