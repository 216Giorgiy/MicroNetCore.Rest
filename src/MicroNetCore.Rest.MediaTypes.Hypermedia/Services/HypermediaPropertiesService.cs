using System.Collections.Generic;
using System.Linq;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions;
using MicroNetCore.Rest.Models.Extensions;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaPropertiesService : IHypermediaPropertiesService
    {
        #region IHypermediaPropertiesService
        
        public IDictionary<string, object> Get(IResponseViewModel model)
        {
            return model.GetModelType()
                .GetProperties()
                .Where(p => !p.IsSubEntityType())
                .ToDictionary(p => p.Name, p => p.GetValue(model));
        }

        public IDictionary<string, object> Get(IEnumerable<IResponseViewModel> models)
        {
            return new Dictionary<string, object>();
        }

        public IDictionary<string, object> Get(IEnumerablePage<IResponseViewModel> page)
        {
            return new Dictionary<string, object>
            {
                {nameof(page.PageCount), page.PageCount},
                {nameof(page.PageIndex), page.PageIndex},
                {nameof(page.PageSize), page.PageSize}
            };
        }

        #endregion
    }
}