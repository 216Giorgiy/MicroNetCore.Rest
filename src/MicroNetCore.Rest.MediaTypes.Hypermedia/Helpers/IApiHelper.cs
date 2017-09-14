using System;
using System.Collections.Generic;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers
{
    public interface IApiHelper
    {
        string GetUri(Type type, IDictionary<string, object> query = null);
        string GetUri(Type type, long id);
    }
}