using System;
using System.Collections.Generic;
using MicroNetCore.Rest.DataTransferObjects;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaPropertiesService
    {
        IDictionary<string, object> Get(Type type, object obj);
        IDictionary<string, object> Get(RestModel model);
        IDictionary<string, object> Get(RestModels models);
        IDictionary<string, object> Get(RestPage page);
    }
}