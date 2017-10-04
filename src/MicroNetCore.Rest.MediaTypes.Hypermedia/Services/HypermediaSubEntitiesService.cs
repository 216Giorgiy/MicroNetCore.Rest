using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;
using MicroNetCore.Rest.Models.Extensions;

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

        public IEnumerable<SubEntity> Get(IResponseViewModel model)
        {
            return model.GetModelType()
                .GetProperties()
                .Where(p => p.IsSubEntityType())
                .SelectMany(p => GetSubEntities(p, model))
                .ToArray();
        }

        public IEnumerable<SubEntity> Get(IEnumerable<IResponseViewModel> models)
        {
            var modelsArray = models as IResponseViewModel[] ?? models.ToArray();
            return modelsArray.SelectMany(m => GetParentSubEntity(m, new[] {"item"}));
        }

        public IEnumerable<SubEntity> Get(IEnumerablePage<IResponseViewModel> page)
        {
            return page.Items.SelectMany(m => GetParentSubEntity(m, new[] {"item"}));
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
                return GetParentSubEntity((IResponseViewModel) value, GetParentRelValue(propertyInfo));

            if (typeof(IEnumerable<IModel>).IsAssignableFrom(propertyInfo.PropertyType))
                return GetChildSubEntities((IEnumerable<IResponseViewModel>) value, GetChildRelValue(propertyInfo));

            throw new Exception("Unknown type of sub-entity.");
        }

        private IEnumerable<SubEntity> GetParentSubEntity(IResponseViewModel model, IEnumerable<string> rel)
        {
            return new SubEntity[]
            {
                new EmbeddedRepresentation
                {
                    Class = _classGenerator.Get(model),
                    Links = _linksGenerator.Get(model),
                    Properties = _propertiesGenerator.Get(model),
                    Rel = rel,
                    Title = _titleGenerator.Get(model)
                }
            };
        }

        private IEnumerable<SubEntity> GetChildSubEntities(IEnumerable<IResponseViewModel> models, IEnumerable<string> rel)
        {
            var modelsArray = models as IResponseViewModel[] ?? models.ToArray();
            return modelsArray.Select(m => new EmbeddedLink
            {
                Class = _classGenerator.Get(modelsArray),
                Href = _apiHelper.GetUri(modelsArray),
                Rel = rel,
                Title = _titleGenerator.Get(modelsArray),
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