using System;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaTitleGenerator
    {
        string Get(Type type);
    }
}