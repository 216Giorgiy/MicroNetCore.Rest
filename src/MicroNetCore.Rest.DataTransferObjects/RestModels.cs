using System;
using System.Collections.Generic;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.DataTransferObjects
{
    public sealed class RestModels : RestObject
    {
        public RestModels(Type type, IEnumerable<IModel> models) : base(type, models)
        {
        }

        public IEnumerable<IModel> Models => (IEnumerable<IModel>) Object;
    }
}