using System.Collections.Generic;
using MicroNetCore.Rest.DataTransferObjects;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaSubEntitiesService
    {
        IEnumerable<SubEntity> Get(RestModel model);
        IEnumerable<SubEntity> Get(RestModels models);
        IEnumerable<SubEntity> Get(RestPage page);
    }
}