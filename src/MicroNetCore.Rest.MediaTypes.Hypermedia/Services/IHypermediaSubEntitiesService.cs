using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaSubEntitiesService
    {
        IEnumerable<SubEntity> Get(IResponseViewModel model);
        IEnumerable<SubEntity> Get(IEnumerable<IResponseViewModel> models);
        IEnumerable<SubEntity> Get(IEnumerablePage<IResponseViewModel> page);
    }
}