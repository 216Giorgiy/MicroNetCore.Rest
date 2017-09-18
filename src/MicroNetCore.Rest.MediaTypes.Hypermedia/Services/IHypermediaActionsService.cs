using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;
using MicroNetCore.Rest.Models.RestResults;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaActionsService
    {
        IEnumerable<Action> Get(ModelRestResult model);
        IEnumerable<Action> Get(ModelsRestResult models);
        IEnumerable<Action> Get(PageRestResult page);
    }
}