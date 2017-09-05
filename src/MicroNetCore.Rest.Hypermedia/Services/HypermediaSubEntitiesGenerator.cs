using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        private IEnumerable<SubEntity> GetSubEntities<TModel>(PropertyInfo propertyInfo, TModel model)
            where TModel : class, IModel
        {
            var value = propertyInfo.GetValue(model);

            if (value == null)
                return new SubEntity[0];

            if (value is IModel iModel)
                return GetParentSubEntity(propertyInfo, iModel);

            if (value is IEnumerable<IModel> iModels)
                return GetChildSubEntities(propertyInfo, iModels);

            throw new Exception("Unknown type of sub-entity.");
        }

        private IEnumerable<SubEntity> GetParentSubEntity<TModel>(PropertyInfo propertyInfo, TModel model)
            where TModel : class, IModel
        {
            return new SubEntity[]
            {
                new EmbeddedRepresentation
                {
                    Class = _classGenerator.Generate(model),
                    Links = _linksGenerator.Generate(model),
                    Properties = _propertiesGenerator.Generate(model),
                    Rel = new[] {$"{propertyInfo.DeclaringType.Name}-{propertyInfo.Name}".ToLower()},
                    Title = _titleGenerator.Generate(model)
                }
            };
        }

        private IEnumerable<SubEntity> GetChildSubEntities<TModel>(PropertyInfo propertyInfo,
            IEnumerable<TModel> models)
            where TModel : class, IModel
        {
            return models.Select(m => new EmbeddedLink
            {
                Class = new[] {propertyInfo.PropertyType.Name},
                Href = _apiHelper.GetUri(m.GetType(), m.Id),
                Rel = new[] {$"{typeof(TModel)}-{propertyInfo.Name}"},
                Title = _titleGenerator.Generate(m),
                Type = "application/json"
            });
        }
    }
}