using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Humanizer;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Attributes;
using MicroNetCore.Rest.Models.Extensions;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaClassService : IHypermediaClassService
    {
        #region Helpers

        private static string CreateClass(MemberInfo modelType)
        {
            return modelType.GetCustomAttribute<ClassAttribute>()?.Class.Camelize() ??
                   modelType.Name.Camelize();
        }

        #endregion

        #region IHypermediaClassService

        public IEnumerable<string> Get(IResponseViewModel model)
        {
            return Get(model.GetModelType());
        }

        public IEnumerable<string> Get(IEnumerable<IResponseViewModel> models)
        {
            return Get(models.GetModelType()).Concat(new[] {"collection"});
        }

        public IEnumerable<string> Get(IEnumerablePage<IResponseViewModel> page)
        {
            return Get(page.GetModelType()).Concat(new[] {"page"});
        }

        #endregion

        #region Static

        private static IEnumerable<string> Get(Type type)
        {
            if (!Cache.ContainsKey(type))
                Cache.Add(type, CreateClass(type));

            return new[] { Cache[type] };
        }

        private static readonly IDictionary<Type, string> Cache;

        static HypermediaClassService()
        {
            Cache = new Dictionary<Type, string>();
        }

        #endregion
    }
}