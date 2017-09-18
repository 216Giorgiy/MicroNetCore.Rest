using System;
using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;
using MicroNetCore.Rest.Models.RestResults;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaLinksService
    {
        IEnumerable<Link> Get(Type type, long id);
        IEnumerable<Link> Get(ModelRestResult model);
        IEnumerable<Link> Get(ModelsRestResult models);
        IEnumerable<Link> Get(PageRestResult page);
    }
}