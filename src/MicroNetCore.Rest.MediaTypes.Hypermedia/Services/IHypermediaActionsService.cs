using System.Collections.Generic;
using MicroNetCore.Rest.DataTransferObjects;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaActionsService
    {
        IEnumerable<Action> Get(RestModel model);
        IEnumerable<Action> Get(RestModels models);
        IEnumerable<Action> Get(RestPage page);
    }
}