using System;
using System.Collections.Generic;
using System.Linq;
using MicroNetCore.Rest.DataTransferObjects;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions;

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

        public IDictionary<string, object> Get(RestModel model)
        {
            return Get(model.Type, model.Model);
        }

        public IDictionary<string, object> Get(RestModels models)
        {
            return new Dictionary<string, object>();
        }

        public IDictionary<string, object> Get(RestPage page)
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