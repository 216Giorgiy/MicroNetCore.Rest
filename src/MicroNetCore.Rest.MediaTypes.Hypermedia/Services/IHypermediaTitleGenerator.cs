using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaTitleGenerator
    {
        string Get(IResponseViewModel model);
        string Get(IEnumerable<IResponseViewModel> models);
        string Get(IEnumerablePage<IResponseViewModel> page);
    }
}