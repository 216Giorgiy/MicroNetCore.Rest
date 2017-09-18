using System;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.Models.RestResults
{
    public sealed class PageRestResult : IRestResult
    {
        public PageRestResult(Type type, IEnumerablePage<IModel> page)
        {
            Type = type;
            Object = page;
        }

        public IEnumerablePage<IModel> Page => (IEnumerablePage<IModel>) Object;

        public object Object { get; }
        public Type Type { get; }
    }
}