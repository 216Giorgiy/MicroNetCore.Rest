using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Helpers;
using MicroNetCore.Rest.Hypermedia.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaSubEntitiesGenerator : IHypermediaSubEntitiesGenerator
    {
        private readonly IApiHelper _apiHelper;
        private readonly IHypermediaClassGenerator _classGenerator;
        private readonly IHypermediaLinksGenerator _linksGenerator;
        private readonly IHypermediaPropertiesGenerator _propertiesGenerator;
        private readonly IHypermediaTitleGenerator _titleGenerator;

        public HypermediaSubEntitiesGenerator(
            IApiHelper apiHelper,
            IHypermediaClassGenerator classGenerator,
            IHypermediaLinksGenerator linksGenerator,
            IHypermediaPropertiesGenerator propertiesGenerator,
            IHypermediaTitleGenerator titleGenerator)
        {
            _apiHelper = apiHelper;
            _classGenerator = classGenerator;
            _linksGenerator = linksGenerator;
            _propertiesGenerator = propertiesGenerator;
            _titleGenerator = titleGenerator;
        }

        public SubEntity[] Generate<TModel>(TModel model)
            where TModel : class, IModel
        {
            return typeof(TModel)
                .GetProperties()
                .Where(p => HypermediaService.IsSubEntityType(p.PropertyType))
                .SelectMany(p => GetSubEntities(p, model))
                .ToArray();
        }

        public SubEntity[] Generate<TModel>(ICollection<TModel> models)
            where TModel : class, IModel
        {
            return models.SelectMany(m => GetParentSubEntity(m, new[] {"item"})).ToArray();
        }

        public SubEntity[] Generate<TModel>(Page<TModel> page)
            where TModel : class, IModel
        {
            return page.Items.SelectMany(m => GetParentSubEntity(m, new[] {"item"})).ToArray();
        }

        private IEnumerable<SubEntity> GetSubEntities<TModel>(
            PropertyInfo propertyInfo, TModel model)
            where TModel : class, IModel
        {
            var value = propertyInfo.GetValue(model);

            if (value == null)
                return new SubEntity[0];

            if (value is IModel iModel)
                return GetParentSubEntity(iModel, GetParentRelValue(propertyInfo));

            if (value is ICollection<IModel> iModels)
                return GetChildSubEntities(iModels, GetChildRelValue(propertyInfo));

            throw new Exception("Unknown type of sub-entity.");
        }

        private IEnumerable<SubEntity> GetParentSubEntity<TModel>(
            TModel model, string[] rel)
            where TModel : class, IModel
        {
            return new SubEntity[]
            {
                new EmbeddedRepresentation
                {
                    Class = _classGenerator.Generate(model),
                    Links = _linksGenerator.Generate(model),
                    Properties = _propertiesGenerator.Generate(model),
                    Rel = rel,
                    Title = _titleGenerator.Generate(model)
                }
            };
        }

        private IEnumerable<SubEntity> GetChildSubEntities<TModel>(
            ICollection<TModel> models, string[] rel)
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
    }
}