using System;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.Models.RestResults
{
    public sealed class ModelRestResult : IRestResult
    {
        public ModelRestResult(Type type, IModel model)
        {
            Type = type;
            Object = model;
        }

        public IModel Model => (IModel) Object;

        public object Object { get; }
        public Type Type { get; }
    }
}