using System;
using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Helpers;
using MicroNetCore.Rest.Hypermedia.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaLinksGenerator : IHypermediaLinksGenerator
    {
        private readonly IApiHelper _apiHelper;

        public HypermediaLinksGenerator(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        #region IHypermediaLinksGenerator

        public Link[] Generate<TModel>(TModel model)
            where TModel : class, IModel
        {
            return new[]
            {
                GetSelfLink(typeof(TModel), model.Id)
            };
        }

        public Link[] Generate<TModel>(ICollection<TModel> models)
            where TModel : class, IModel
        {
            return new[]
            {
                GetSelfLink(typeof(TModel))
            };
        }

        public Link[] Generate<TModel>(Page<TModel> page)
            where TModel : class, IModel
        {
            var links = new List<Link>
            {
                GetSelfLink(typeof(TModel), page.PageIndex, page.PageSize)
            };

            if (page.PageIndex > 1)
                links.Add(GetPrevLink(typeof(TModel), page.PageIndex, page.PageSize));

            if (page.PageIndex < page.PageCount)
                links.Add(GetNextLink(typeof(TModel), page.PageIndex, page.PageSize));

            return links.ToArray();
        }

        #endregion

        #region Helpers

        private Link GetSelfLink(Type type)
        {
            return new Link
            {
                Rel = new[] {"self"},
                Href = _apiHelper.GetUri(type)
            };
        }

        private Link GetSelfLink(Type type, long id)
        {
            return new Link
            {
                Rel = new[] {"self"},
                Href = _apiHelper.GetUri(type, id)
            };
        }

        private Link GetSelfLink(Type type, int pageIndex, int pageSize)
        {
            return new Link
            {
                Rel = new[] {"self"},
                Href = GetPageHref(type, pageIndex, pageSize)
            };
        }

        private Link GetPrevLink(Type type, int pageIndex, int pageSize)
        {
            return new Link
            {
                Rel = new[] {"prev"},
                Href = GetPageHref(type, pageIndex - 1, pageSize)
            };
        }

        private Link GetNextLink(Type type, int pageIndex, int pageSize)
        {
            return new Link
            {
                Rel = new[] {"next"},
                Href = GetPageHref(type, pageIndex + 1, pageSize)
            };
        }

        private string GetPageHref(Type type, int pageIndex, int pageSize)
        {
            var query = new Dictionary<string, object>
            {
                {"pageIndex", pageIndex},
                {"pageSize", pageSize}
            };

            return _apiHelper.GetUri(type, query);
        }

        #endregion
    }
}