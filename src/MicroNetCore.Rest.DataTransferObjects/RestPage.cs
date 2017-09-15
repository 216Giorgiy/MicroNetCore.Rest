using System;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.DataTransferObjects
{
    public sealed class RestPage : RestObject
    {
        public RestPage(Type type, IEnumerablePage<IModel> page) : base(type, page)
        {
        }

        public IEnumerablePage<IModel> Page => (IEnumerablePage<IModel>) Object;
    }
}