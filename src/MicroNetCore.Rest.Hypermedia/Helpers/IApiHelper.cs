using System;

namespace MicroNetCore.Rest.Hypermedia.Helpers
{
    public interface IApiHelper
    {
        string GetUri(Type type);
        string GetUri(Type type, long id);
    }
}