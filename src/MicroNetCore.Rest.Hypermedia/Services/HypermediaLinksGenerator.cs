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
                GetSelfLink(model)
            };
        }

        private Link GetSelfLink<TModel>(TModel model)
            where TModel : class, IModel
        {
            return new Link
            {
                Rel = new[] {"self"},
                Href = _apiHelper.GetUri(model.GetType(), model.Id)
            };
        }
    }
}