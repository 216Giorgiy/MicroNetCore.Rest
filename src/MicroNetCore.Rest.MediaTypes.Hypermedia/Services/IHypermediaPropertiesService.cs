using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaPropertiesService
    {
        IDictionary<string, object> Get(IResponseViewModel model);
        IDictionary<string, object> Get(IEnumerable<IResponseViewModel> models);
        IDictionary<string, object> Get(IEnumerablePage<IResponseViewModel> page);
    }
}