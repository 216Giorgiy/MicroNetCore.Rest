using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaActionsService
    {
        IEnumerable<Action> Get(IResponseViewModel model);
        IEnumerable<Action> Get(IEnumerable<IResponseViewModel> models);
        IEnumerable<Action> Get(IEnumerablePage<IResponseViewModel> page);
    }
}