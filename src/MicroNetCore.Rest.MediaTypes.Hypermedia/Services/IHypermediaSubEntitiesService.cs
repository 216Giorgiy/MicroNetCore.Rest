using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;
using MicroNetCore.Rest.Models.RestResults;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaSubEntitiesService
    {
        IEnumerable<SubEntity> Get(ModelRestResult model);
        IEnumerable<SubEntity> Get(ModelsRestResult models);
        IEnumerable<SubEntity> Get(PageRestResult page);
    }
}