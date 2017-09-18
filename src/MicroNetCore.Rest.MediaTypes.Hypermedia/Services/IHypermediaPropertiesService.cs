using System;
using System.Collections.Generic;
using MicroNetCore.Rest.Models.RestResults;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaPropertiesService
    {
        IDictionary<string, object> Get(Type type, object obj);
        IDictionary<string, object> Get(ModelRestResult model);
        IDictionary<string, object> Get(ModelsRestResult models);
        IDictionary<string, object> Get(PageRestResult page);
    }
}