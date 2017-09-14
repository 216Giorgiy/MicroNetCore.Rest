using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaActionsService
    {
        Action[] Get(IResponseViewModel obj);
        Action[] Get(IEnumerable<IResponseViewModel> collection);
        Action[] Get(IEnumerablePage<IResponseViewModel> page);
    }
}