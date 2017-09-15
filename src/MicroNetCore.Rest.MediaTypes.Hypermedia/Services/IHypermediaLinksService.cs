using System;
using System.Collections.Generic;
using MicroNetCore.Rest.DataTransferObjects;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaLinksService
    {
        IEnumerable<Link> Get(Type type, long id);
        IEnumerable<Link> Get(RestModel model);
        IEnumerable<Link> Get(RestModels models);
        IEnumerable<Link> Get(RestPage page);
    }
}