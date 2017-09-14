using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers
{
    public sealed class ApiHelper : IApiHelper
    {
        public string GetUri(Type type, IDictionary<string, object> query = null)
        {
            var uriBase = $"/api/{type.Name.Pluralize()}";

            return query == null || query.Count < 1
                ? uriBase.ToLower()
                : $"{uriBase}?{string.Join('&', query.Select(q => $"{q.Key}={q.Value.ToString()}"))}".ToLower();
        }

        public string GetUri(Type type, long id)
        {
            return $"{GetUri(type)}/{id}".ToLower();
        }
    }
}