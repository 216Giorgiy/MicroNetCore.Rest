using System;
using System.Collections.Generic;
using MicroNetCore.Rest.Models.RestResults;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaClassService
    {
        IEnumerable<string> Get(Type type);
        IEnumerable<string> Get(ModelRestResult model);
        IEnumerable<string> Get(ModelsRestResult models);
        IEnumerable<string> Get(PageRestResult page);
    }
}