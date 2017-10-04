using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaClassService
    {
        IEnumerable<string> Get(IResponseViewModel model);
        IEnumerable<string> Get(IEnumerable<IResponseViewModel> models);
        IEnumerable<string> Get(IEnumerablePage<IResponseViewModel> page);
    }
}