using System;

namespace MicroNetCore.Rest.Attribites
{
    public sealed class DefaultPageSizeAttribute : Attribute
    {
        public DefaultPageSizeAttribute(int pageSize)
        {
            PageSize = pageSize;
        }

        public int PageSize { get; }
    }
}