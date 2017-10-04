using System;
using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaLinksService
    {
        IEnumerable<Link> Get(IResponseViewModel model);
        IEnumerable<Link> Get(IEnumerable<IResponseViewModel> models);
        IEnumerable<Link> Get(IEnumerablePage<IResponseViewModel> page);
    }
}