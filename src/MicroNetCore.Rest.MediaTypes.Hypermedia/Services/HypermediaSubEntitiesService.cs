using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MicroNetCore.Models;
using MicroNetCore.Rest.DataTransferObjects;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions;
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

        public IEnumerable<SubEntity> Get(RestModel model)
        {
            return model.Type
                .GetProperties()
                .Where(p => p.IsSubEntityType())
                .SelectMany(p => GetSubEntities(p, model.Model))
                .ToArray();
        }

        public IEnumerable<SubEntity> Get(RestModels models)
        {
            return models.Models.SelectMany(m => GetParentSubEntity(models.Type, m, new[] {"item"}));
        }

        public IEnumerable<SubEntity> Get(RestPage page)
        {
            return page.Page.Items.SelectMany(m => GetParentSubEntity(page.Type, m, new[] {"item"}));
        }

        #endregion

        #region Helpers

        private IEnumerable<SubEntity> GetSubEntities(
            PropertyInfo propertyInfo, object parent)
        {
            var value = propertyInfo.GetValue(parent);

            if (value == null)
                return new List<SubEntity>();

            if (typeof(IModel).IsAssignableFrom(propertyInfo.PropertyType))
                return GetParentSubEntity(
                    propertyInfo.PropertyType,
                    (IModel) value,
                    GetParentRelValue(propertyInfo));

            if (typeof(IEnumerable<IModel>).IsAssignableFrom(propertyInfo.PropertyType))
                return GetChildSubEntities(
                    propertyInfo.PropertyType.GetGenericArguments().First(),
                    (IEnumerable<IModel>) value,
                    GetChildRelValue(propertyInfo));

            throw new Exception("Unknown type of sub-entity.");
        }

        private IEnumerable<SubEntity> GetParentSubEntity(
            Type type, IModel model, IEnumerable<string> rel)
        {
            return new SubEntity[]
            {
                new EmbeddedRepresentation
                {
                    Class = _classGenerator.Get(type),
                    Links = _linksGenerator.Get(type, model.Id),
                    Properties = _propertiesGenerator.Get(type, model),
                    Rel = rel,
                    Title = _titleGenerator.Get(type)
                }
            };
        }

        private IEnumerable<SubEntity> GetChildSubEntities(
            Type type, IEnumerable<IModel> models, IEnumerable<string> rel)
        {
            return models.Select(m => new EmbeddedLink
            {
                Class = _classGenerator.Get(type),
                Href = _apiHelper.GetUri(type, m.Id),
                Rel = rel,
                Title = _titleGenerator.Get(type),
                Type = "application/json"
            });
        }

        private static IEnumerable<string> GetParentRelValue(MemberInfo propertyInfo)
        {
            return new[] {$"{propertyInfo.DeclaringType.Name}-{propertyInfo.Name}".ToLower()};
        }

        private static IEnumerable<string> GetChildRelValue(MemberInfo propertyInfo)
        {
            return new[] {$"{propertyInfo.DeclaringType.Name}-{propertyInfo.Name}".ToLower()};
        }

        #endregion
    }
}