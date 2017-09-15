using System;
using System.Collections.Generic;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.DataTransferObjects
{
    public sealed class RestModels : IRestResult
    {
        public RestModels(Type type, IEnumerable<IModel> models)
        {
            Type = type;
            Object = models;
        }

        public IEnumerable<IModel> Models => (IEnumerable<IModel>) Object;
        public Type Type { get; }
        public object Object { get; }
    }
}