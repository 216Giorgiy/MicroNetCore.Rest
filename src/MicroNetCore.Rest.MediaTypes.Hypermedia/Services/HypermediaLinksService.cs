using System;
using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;
using MicroNetCore.Rest.Models.Extensions;

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
        
        public IEnumerable<Link> Get(IResponseViewModel model)
        {
            return new[]
            {
                GetSelfLink(model)
            };
        }

        public IEnumerable<Link> Get(IEnumerable<IResponseViewModel> models)
        {
            return new[]
            {
                GetSelfLink(models.GetModelType())
            };
        }

        public IEnumerable<Link> Get(IEnumerablePage<IResponseViewModel> page)
        {
            var links = new List<Link>
            {
                GetSelfLink(page)
            };

            if (page.PageIndex > 1)
                links.Add(GetPrevLink(page));

            if (page.PageIndex < page.PageCount)
                links.Add(GetNextLink(page));

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

        private Link GetSelfLink(IResponseViewModel model)
        {
            return new Link
            {
                Rel = new[] {"self"},
                Href = _apiHelper.GetUri(model)
            };
        }

        private Link GetSelfLink(IEnumerablePage<IResponseViewModel> page)
        {
            return new Link
            {
                Rel = new[] {"self"},
                Href = GetPageHref(page, page.PageIndex, page.PageSize)
            };
        }

        private Link GetPrevLink(IEnumerablePage<IResponseViewModel> page)
        {
            return new Link
            {
                Rel = new[] {"prev"},
                Href = GetPageHref(page, page.PageIndex - 1, page.PageSize)
            };
        }

        private Link GetNextLink(IEnumerablePage<IResponseViewModel> page)
        {
            return new Link
            {
                Rel = new[] {"next"},
                Href = GetPageHref(page, page.PageIndex + 1, page.PageSize)
            };
        }

        private string GetPageHref(IEnumerablePage<IResponseViewModel> page, int pageIndex, int pageSize)
        {
            var query = new Dictionary<string, object>
            {
                {"pageIndex", pageIndex},
                {"pageSize", pageSize}
            };

            return _apiHelper.GetUri(page, query);
        }

        #endregion
    }
}