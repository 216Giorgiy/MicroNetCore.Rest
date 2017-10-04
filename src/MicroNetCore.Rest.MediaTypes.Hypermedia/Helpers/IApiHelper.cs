using System;
using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers
{
    public interface IApiHelper
    {
        string GetUri(Type type);
        string GetUri(IResponseViewModel model);
        string GetUri(IEnumerable<IResponseViewModel> models, IDictionary<string, object> query = null);
        string GetUri(IEnumerablePage<IResponseViewModel> page, IDictionary<string, object> query = null);
    }
}