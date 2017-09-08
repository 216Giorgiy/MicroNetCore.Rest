using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Humanizer;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Models.Markup;
using MicroNetCore.Rest.Hypermedia.Helpers;
using MicroNetCore.Rest.Hypermedia.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaActionsGenerator : IHypermediaActionsGenerator
    {
        private readonly IApiHelper _apiHelper;
        private readonly IHypermediaFieldMapper _fieldMapper;

        public HypermediaActionsGenerator(IApiHelper apiHelper, IHypermediaFieldMapper fieldMapper)
        {
            _apiHelper = apiHelper;
            _fieldMapper = fieldMapper;
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

        public Action[] Generate<TModel>(ICollection<TModel> models)
            where TModel : class, IModel
        {
            return new Action[0];
        }

        public Action[] Generate<TModel>(IPageCollection<TModel> page)
            where TModel : class, IModel
        {
            return new Action[0];
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

        private Action.Field[] GetEditForm<TModel>()
        {
            return typeof(TModel)
                .GetProperties()
                .Where(p => p.GetCustomAttribute<EditAttribute>() != null)
                .Select(p => new Action.Field
                {
                    Name = p.Name.Camelize(),
                    Type = _fieldMapper.Map(p.PropertyType)
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