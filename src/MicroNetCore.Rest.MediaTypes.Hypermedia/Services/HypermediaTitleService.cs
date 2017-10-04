using System;
using System.Collections.Generic;
using System.Reflection;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Attributes;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions;
using MicroNetCore.Rest.Models.Extensions;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaTitleService : IHypermediaTitleGenerator
    {
        #region IHypermediaTitleGenerator
        
        public string Get(IResponseViewModel model)
        {
            return Get(model.GetModelType());
        }

        public string Get(IEnumerable<IResponseViewModel> models)
        {
            return Get(models.GetModelType());
        }

        public string Get(IEnumerablePage<IResponseViewModel> page)
        {
            return Get(page.GetModelType());
        }

        #endregion

        #region Helpers
        
        private static string Get(Type type)
        {
            if (!Cache.ContainsKey(type))
                Cache.Add(type, Create(type));

            return Cache[type];
        }

        private static string Create(MemberInfo type)
        {
            return type.GetCustomAttribute<TitleAttribute>()?.Title;
        }

        #endregion

        #region Static

        private static readonly IDictionary<Type, string> Cache;

        static HypermediaTitleService()
        {
            Cache = new Dictionary<Type, string>();
        }

        #endregion
    }
}