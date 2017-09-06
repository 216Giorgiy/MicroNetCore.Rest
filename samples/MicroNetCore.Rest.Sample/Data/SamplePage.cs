using System.Collections.Generic;
using MicroNetCore.Data.Abstractions;

namespace MicroNetCore.Rest.Sample.Data
{
    public sealed class SamplePage<TModel> : List<TModel>, IPageCollection<TModel>
    {
        public SamplePage(int pageCount, int pageIndex, int pageSize, IEnumerable<TModel> models)
            : base(models)
        {
            PageCount = pageCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageCount { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
    }
}