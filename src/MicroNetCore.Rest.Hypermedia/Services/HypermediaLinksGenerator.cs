using System;
using System.Collections.Generic;
using MicroNetCore.Data.Abstractions;
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

        public Link[] Generate<TModel>(IPageCollection<TModel> page)
            where TModel : class, IModel
        {
            return new[]
            {
                GetSelfLink(typeof(TModel), page.PageIndex, page.PageSize)
            };
        }


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
            var query = new Dictionary<string, object>
            {
                {"pageIndex", pageIndex},
                {"pageSize", pageSize}
            };

            return new Link
            {
                Rel = new[] {"self"},
                Href = _apiHelper.GetUri(type, query)
            };
        }
    }
}