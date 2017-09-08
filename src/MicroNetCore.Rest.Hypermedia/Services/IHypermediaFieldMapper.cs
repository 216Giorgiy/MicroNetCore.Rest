using System;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaFieldMapper
    {
        string Map(Type type);
    }
}