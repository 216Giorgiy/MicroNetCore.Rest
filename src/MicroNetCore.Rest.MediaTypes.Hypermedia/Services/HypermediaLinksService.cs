using System;
using System.Collections.Generic;
using MicroNetCore.Rest.DataTransferObjects;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaLinksService : IHypermediaLinksService
    {
        private readonly IApiHelper _apiHelper;

        public HypermediaLinksService(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        #region IHypermediaLinksGenerator

        public IEnumerable<Link> Get(Type type, long id)
        {
            return new[]
            {
                GetSelfLink(type, id)
            };
        }

        public IEnumerable<Link> Get(RestModel model)
        {
            return Get(model.Type, model.Model.Id);
        }

        public IEnumerable<Link> Get(RestModels models)
        {
            return new[]
            {
                GetSelfLink(models.Type)
            };
        }

        public IEnumerable<Link> Get(RestPage page)
        {
            var links = new List<Link>
            {
                GetSelfLink(page.Type, page.Page.PageIndex, page.Page.PageSize)
            };

            if (page.Page.PageIndex > 1)
                links.Add(GetPrevLink(page.Type, page.Page.PageIndex, page.Page.PageSize));

            if (page.Page.PageIndex < page.Page.PageCount)
                links.Add(GetNextLink(page.Type, page.Page.PageIndex, page.Page.PageSize));

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