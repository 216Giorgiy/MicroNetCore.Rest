using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.Models.Extensions;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers
{
    public sealed class ApiHelper : IApiHelper
    {
        public string GetUri(Type type)
        {
            return GetUriBase(type);
        }

        public string GetUri(IResponseViewModel model)
        {
            return $"{GetUriBase(model.GetModelType())}/{model.Id}".ToLower();
        }

        public string GetUri(IEnumerable<IResponseViewModel> models, IDictionary<string, object> query = null)
        {
            return query == null || query.Count < 1
                ? GetUriBase(models.GetModelType()).ToLower()
                : $"{GetUriBase(models.GetModelType())}?{GetQueryString(query)})".ToLower();
        }

        public string GetUri(IEnumerablePage<IResponseViewModel> page, IDictionary<string, object> query = null)
        {
            return query == null || query.Count < 1
                ? GetUriBase(page.GetModelType()).ToLower()
                : $"{GetUriBase(page.GetModelType())}?{GetQueryString(query)})".ToLower();
        }

        private static string GetUriBase(Type type)
        {
            return $"/api/{type.Name.Pluralize()}";
        }

        private static string GetQueryString(IDictionary<string, object> query)
        {
            return string.Join('&', query.Select(q => $"{q.Key}={q.Value.ToString()}"));
        }
    }
}