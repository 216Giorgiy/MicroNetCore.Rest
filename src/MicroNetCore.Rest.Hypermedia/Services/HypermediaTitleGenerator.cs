using System.Collections.Generic;
using System.Reflection;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Attributes;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaTitleGenerator : IHypermediaTitleGenerator
    {
        public string Generate<TModel>(TModel model)
            where TModel : class, IModel
        {
            return GetTitle<TModel>();
        }

        public string Generate<TModel>(ICollection<TModel> models)
            where TModel : class, IModel
        {
            return GetTitle<TModel>();
        }

        public string Generate<TModel>(IPageCollection<TModel> page)
            where TModel : class, IModel
        {
            return GetTitle<TModel>();
        }

        private static string GetTitle<TModel>()
        {
            return typeof(TModel).GetCustomAttribute<TitleAttribute>()?.Title;
        }
    }
}