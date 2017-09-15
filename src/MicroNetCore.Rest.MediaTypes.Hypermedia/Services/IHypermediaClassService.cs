using System;
using System.Collections.Generic;
using MicroNetCore.Rest.DataTransferObjects;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaClassService
    {
        IEnumerable<string> Get(Type type);
        IEnumerable<string> Get(RestModel model);
        IEnumerable<string> Get(RestModels models);
        IEnumerable<string> Get(RestPage page);
    }
}