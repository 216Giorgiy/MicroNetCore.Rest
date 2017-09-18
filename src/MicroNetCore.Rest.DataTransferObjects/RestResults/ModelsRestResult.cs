using System;
using System.Collections.Generic;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.DataTransferObjects.RestResults
{
    public sealed class ModelsRestResult : IRestResult
    {
        public ModelsRestResult(Type type, IEnumerable<IModel> models)
        {
            Type = type;
            Object = models;
        }

        public IEnumerable<IModel> Models => (IEnumerable<IModel>) Object;

        public object Object { get; }
        public Type Type { get; }
    }
}