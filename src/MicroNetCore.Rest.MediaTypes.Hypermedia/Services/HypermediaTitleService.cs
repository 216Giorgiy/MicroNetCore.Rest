using System;
using System.Collections.Generic;
using System.Reflection;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Attributes;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaTitleService : IHypermediaTitleGenerator
    {
        #region IHypermediaTitleGenerator

        public string Get(Type type)
        {
            if (!Cache.ContainsKey(type))
                Cache.Add(type, Create(type));

            return Cache[type];
        }

        #endregion

        #region Helpers

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