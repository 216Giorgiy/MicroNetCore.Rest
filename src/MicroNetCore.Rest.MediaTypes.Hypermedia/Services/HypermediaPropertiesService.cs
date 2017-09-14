using System.Collections.Generic;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaPropertiesService : IHypermediaPropertiesService
    {
        #region IHypermediaPropertiesService

        public IDictionary<string, object> Get(IViewModel viewModel)
        {
            return viewModel
                .GetType()
                .GetProperties()
                .Where(p => !HypermediaSubEntitiesService.IsSubEntityType(p.PropertyType))
                .ToDictionary(p => p.Name, p => p.GetValue(viewModel));
        }

        public IDictionary<string, object> Get(IEnumerable<IViewModel> viewModels)
        {
            return new Dictionary<string, object>();
        }

        public IDictionary<string, object> Get(IEnumerablePage<IViewModel> page)
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