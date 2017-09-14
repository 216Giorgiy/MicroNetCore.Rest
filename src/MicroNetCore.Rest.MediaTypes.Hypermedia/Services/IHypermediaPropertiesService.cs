using System.Collections.Generic;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaPropertiesService
    {
        IDictionary<string, object> Get(IViewModel viewModel);
        IDictionary<string, object> Get(IEnumerable<IViewModel> viewModels);
        IDictionary<string, object> Get(IEnumerablePage<IViewModel> page);
    }
}