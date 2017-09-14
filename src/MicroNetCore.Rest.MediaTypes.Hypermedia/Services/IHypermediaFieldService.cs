using System;
using System.Collections.Generic;
using System.Reflection;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaFieldService
    {
        Field[] Get(IEnumerable<PropertyInfo> properties);

        string Map(Type type);
    }
}