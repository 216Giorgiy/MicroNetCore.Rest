using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Humanizer;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Attributes;
using MicroNetCore.Rest.Models.RestResults;

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

        public IEnumerable<string> Get(Type type)
        {
            if (!Cache.ContainsKey(type))
                Cache.Add(type, CreateClass(type));

            return new[] {Cache[type]};
        }

        public IEnumerable<string> Get(ModelRestResult model)
        {
            return Get(model.Type);
        }

        public IEnumerable<string> Get(ModelsRestResult models)
        {
            return Get(models.Type).Concat(new[] {"collection"});
        }

        public IEnumerable<string> Get(PageRestResult page)
        {
            return Get(page.Type).Concat(new[] {"page"});
        }

        #endregion

        #region Static

        private static readonly IDictionary<Type, string> Cache;

        static HypermediaClassService()
        {
            Cache = new Dictionary<Type, string>();
        }

        #endregion
    }
}