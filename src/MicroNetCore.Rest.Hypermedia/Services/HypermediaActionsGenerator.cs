using System.Linq;
using System.Net.Http;
using System.Reflection;
using Humanizer;
using MicroNetCore.Models;
using MicroNetCore.Models.Markup;
using MicroNetCore.Rest.Hypermedia.Helpers;
using MicroNetCore.Rest.Hypermedia.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaActionsGenerator : IHypermediaActionsGenerator
    {
        private readonly IApiHelper _apiHelper;

        public HypermediaActionsGenerator(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public Action[] Generate<TModel>(TModel model)
            where TModel : class, IModel
        {
            return new[]
            {
                GetEditAction(model),
                GetDeleteAction(model)
            };
        }

        private Action GetEditAction<TModel>(TModel model)
            where TModel : class, IModel
        {
            return new Action
            {
                Name = $"edit-{typeof(TModel).Name}".ToLower(),
                Href = _apiHelper.GetUri(model.GetType(), model.Id),
                Method = HttpMethod.Put.ToString(),
                Title = $"Edit {typeof(TModel).Name}",
                Type = "application/json",
                Fields = GetEditForm<TModel>()
            };
        }

        private static Action.Field[] GetEditForm<TModel>()
        {
            return typeof(TModel)
                .GetProperties()
                .Where(p => p.GetCustomAttribute<EditAttribute>() != null)
                .Select(p => new Action.Field
                {
                    Name = p.Name.Camelize(),
                    Type = p.PropertyType.Name
                }).ToArray();
        }

        private Action GetDeleteAction<TModel>(TModel model)
            where TModel : class, IModel
        {
            return new Action
            {
                Name = $"delete-{typeof(TModel).Name}".ToLower(),
                Href = _apiHelper.GetUri(model.GetType(), model.Id),
                Method = HttpMethod.Delete.ToString(),
                Title = $"Delete {typeof(TModel).Name}"
            };
        }
    }
}