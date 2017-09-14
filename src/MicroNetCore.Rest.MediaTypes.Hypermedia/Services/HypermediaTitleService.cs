using System.Collections.Generic;
using System.Reflection;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Attributes;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaTitleService : IHypermediaTitleGenerator
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

        public string Generate<TModel>(IEnumerablePage<TModel> page)
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