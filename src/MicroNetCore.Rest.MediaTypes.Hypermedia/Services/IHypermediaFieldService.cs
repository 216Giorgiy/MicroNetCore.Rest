using System;
using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaFieldService
    {
        IEnumerable<Field> GetAddFields(Type modelType);
        IEnumerable<Field> GetEditFields(Type modelType);
    }
}