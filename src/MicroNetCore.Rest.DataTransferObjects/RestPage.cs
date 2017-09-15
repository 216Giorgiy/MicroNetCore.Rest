using System;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.DataTransferObjects
{
    public sealed class RestPage : IRestResult
    {
        public RestPage(Type type, IEnumerablePage<IModel> page)
        {
            Type = type;
            Object = page;
        }

        public IEnumerablePage<IModel> Page => (IEnumerablePage<IModel>) Object;
        public Type Type { get; }
        public object Object { get; }
    }
}