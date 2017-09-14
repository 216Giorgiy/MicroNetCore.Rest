using System;
using System.Collections.Generic;
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

        public Link[] Get(IResponseViewModel viewModel)
        {
            return new[]
            {
                GetSelfLink(viewModel.GetType(), viewModel.Id)
            };
        }

        public Link[] Get(IEnumerable<IResponseViewModel> viewModels)
        {
            return new[]
            {
                GetSelfLink(viewModels.GetType().GetGenericArguments()[0])
            };
        }

        public Link[] Get(IEnumerablePage<IResponseViewModel> page)
        {
            var type = page.GetType().GetGenericArguments()[0];

            var links = new List<Link>
            {
                GetSelfLink(type, page.PageIndex, page.PageSize)
            };

            if (page.PageIndex > 1)
                links.Add(GetPrevLink(type, page.PageIndex, page.PageSize));

            if (page.PageIndex < page.PageCount)
                links.Add(GetNextLink(type, page.PageIndex, page.PageSize));

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