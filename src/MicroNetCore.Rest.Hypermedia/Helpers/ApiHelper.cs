using System;
using Humanizer;

namespace MicroNetCore.Rest.Hypermedia.Helpers
{
    public sealed class ApiHelper : IApiHelper
    {
        public string GetUri(Type type)
        {
            return $"/api/{type.Name.Pluralize()}".ToLower();
        }

        public string GetUri(Type type, long id)
        {
            return $"{GetUri(type)}/{id}".ToLower();
        }
    }
}