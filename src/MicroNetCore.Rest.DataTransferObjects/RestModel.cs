using System;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.DataTransferObjects
{
    public sealed class RestModel : RestObject
    {
        public RestModel(Type type, IModel model) : base(type, model)
        {
        }

        public IModel Model => (IModel) Object;
    }
}