using System;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.DataTransferObjects
{
    public sealed class RestModel : IRestResult
    {
        public RestModel(Type type, IModel model)
        {
            Type = type;
            Object = model;
        }

        public IModel Model => (IModel) Object;
        public Type Type { get; }
        public object Object { get; }
    }
}