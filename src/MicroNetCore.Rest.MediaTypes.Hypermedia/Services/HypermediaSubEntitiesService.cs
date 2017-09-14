using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaSubEntitiesService : IHypermediaSubEntitiesService
    {
        private readonly IApiHelper _apiHelper;
        private readonly IHypermediaClassService _classGenerator;
        private readonly IHypermediaLinksService _linksGenerator;
        private readonly IHypermediaPropertiesService _propertiesGenerator;
        private readonly IHypermediaTitleGenerator _titleGenerator;

        public HypermediaSubEntitiesService(
            IApiHelper apiHelper,
            IHypermediaClassService classGenerator,
            IHypermediaLinksService linksGenerator,
            IHypermediaPropertiesService propertiesGenerator,
            IHypermediaTitleGenerator titleGenerator)
        {
            _apiHelper = apiHelper;
            _classGenerator = classGenerator;
            _linksGenerator = linksGenerator;
            _propertiesGenerator = propertiesGenerator;
            _titleGenerator = titleGenerator;
        }

        #region IHypermediaSubEntitiesGenerator

        public SubEntity[] Get(IViewModel viewModel)
        {
            return viewModel
                .GetType()
                .GetProperties()
                .Where(p => IsSubEntityType(p.PropertyType))
                .SelectMany(p => GetSubEntities(p, viewModel))
                .ToArray();
        }

        public SubEntity[] Get(IEnumerable<IViewModel> viewModels)
        {
            return viewModels.SelectMany(m => GetParentSubEntity(m, new[] {"item"})).ToArray();
        }

        public SubEntity[] Get(IEnumerablePage<IViewModel> page)
        {
            return page.Items.SelectMany(m => GetParentSubEntity(m, new[] {"item"})).ToArray();
        }

        #endregion

        #region Helpers

        private IEnumerable<SubEntity> GetSubEntities(
            PropertyInfo propertyInfo, IViewModel viewModel)
        {
            var value = propertyInfo.GetValue(viewModel);

            switch (value)
            {
                case null:
                    return new SubEntity[0];
                case IModel iModel:
                    return GetParentSubEntity(iModel, GetParentRelValue(propertyInfo));
                case ICollection<IModel> iModels:
                    return GetChildSubEntities(iModels, GetChildRelValue(propertyInfo));
                default:
                    throw new Exception("Unknown type of sub-entity.");
            }
        }

        private IEnumerable<SubEntity> GetParentSubEntity(
            IModel model, string[] rel)
        {
            return new SubEntity[]
            {
                new EmbeddedRepresentation
                {
                    Class = _classGenerator.GetForSingle(model.GetType()),
                    Links = _linksGenerator.Generate(model),
                    Properties = _propertiesGenerator.Generate(model),
                    Rel = rel,
                    Title = _titleGenerator.Generate(model)
                }
            };
        }

        private IEnumerable<SubEntity> GetChildSubEntities<TModel>(
            IEnumerable<TModel> models, string[] rel)
            where TModel : class, IModel
        {
            return models.Select(m => new EmbeddedLink
            {
                Class = _classGenerator.Generate(m),
                Href = _apiHelper.GetUri(m.GetType(), m.Id),
                Rel = rel,
                Title = _titleGenerator.Generate(m),
                Type = "application/json"
            });
        }

        private static string[] GetParentRelValue(PropertyInfo propertyInfo)
        {
            return new[] {$"{propertyInfo.DeclaringType.Name}-{propertyInfo.Name}".ToLower()};
        }

        private static string[] GetChildRelValue(PropertyInfo propertyInfo)
        {
            return new[] {$"{propertyInfo.DeclaringType.Name}-{propertyInfo.Name}".ToLower()};
        }

        public static bool IsSubEntityType(Type type)
        {
            return typeof(IModel).IsAssignableFrom(type) || typeof(IEnumerable<IModel>).IsAssignableFrom(type);
        }

        #endregion
    }
}