using System.Collections.Generic;
using System.Reflection;
using Humanizer;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Attributes;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaClassGenerator : IHypermediaClassGenerator
    {
        public string[] Generate<TModel>(TModel model)
            where TModel : class, IModel
        {
            return new[]
            {
                GetClass<TModel>()
            };
        }

        public string[] Generate<TModel>(ICollection<TModel> models)
            where TModel : class, IModel
        {
            return new[]
            {
                "collection",
                GetClass<TModel>()
            };
        }

        public string[] Generate<TModel>(IPageCollection<TModel> page)
            where TModel : class, IModel
        {
            return new[]
            {
                "page",
                GetClass<TModel>()
            };
        }

        private static string GetClass<TModel>()
        {
            return typeof(TModel).GetCustomAttribute<ClassAttribute>()?.Class.Camelize() ??
                   typeof(TModel).Name.Camelize();
        }
    }
}