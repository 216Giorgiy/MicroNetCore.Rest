using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia
{
    public sealed class HypermediaSerializer : IHypermediaSerializer
    {
        #region Static

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        #endregion

        private readonly IHypermediaActionsService _actionsGenerator;
        private readonly IHypermediaClassService _classGenerator;
        private readonly IHypermediaLinksService _linksGenerator;
        private readonly IHypermediaPropertiesService _propertiesGenerator;
        private readonly IHypermediaSubEntitiesService _subEntitiesGenerator;
        private readonly IHypermediaTitleGenerator _titleGenerator;

        public HypermediaSerializer(
            IHypermediaActionsService actionsGenerator,
            IHypermediaClassService classGenerator,
            IHypermediaLinksService linksGenerator,
            IHypermediaPropertiesService propertiesGenerator,
            IHypermediaSubEntitiesService subEntitiesGenerator,
            IHypermediaTitleGenerator titleGenerator)
        {
            _actionsGenerator = actionsGenerator;
            _classGenerator = classGenerator;
            _linksGenerator = linksGenerator;
            _propertiesGenerator = propertiesGenerator;
            _subEntitiesGenerator = subEntitiesGenerator;
            _titleGenerator = titleGenerator;
        }

        public string Serialize(object obj, Encoding encoding)
        {
            var entity = CreateEntity(obj);
            return JsonConvert.SerializeObject(entity, Settings);
        }

        #region Helpers

        private Entity CreateEntity(object obj)
        {
            switch (obj)
            {
                case IResponseViewModel model:
                    return CreateEntity(model);
                case IEnumerable<IResponseViewModel> models:
                    return CreateEntity(models);
                case IEnumerablePage<IResponseViewModel> page:
                    return CreateEntity(page);
                default:
                    throw new Exception($"{obj.GetType().Name} can not be converted to a Hypermedia Entity.");
            }
        }

        private Entity CreateEntity(IResponseViewModel model)
        {
            return new Entity
            {
                Actions = _actionsGenerator.Get(model),
                Class = _classGenerator.Get(model),
                Links = _linksGenerator.Get(model),
                Properties = _propertiesGenerator.Get(model),
                Entities = _subEntitiesGenerator.Get(model),
                Title = _titleGenerator.Get(model)
            };
        }

        private Entity CreateEntity(IEnumerable<IResponseViewModel> models)
        {
            var modelsArray = models.ToArray();

            return new Entity
            {
                Actions = _actionsGenerator.Get(modelsArray),
                Class = _classGenerator.Get(modelsArray),
                Links = _linksGenerator.Get(modelsArray),
                Properties = _propertiesGenerator.Get(modelsArray),
                Entities = _subEntitiesGenerator.Get(modelsArray),
                Title = _titleGenerator.Get(modelsArray)
            };
        }

        private Entity CreateEntity(IEnumerablePage<IResponseViewModel> page)
        {
            return new Entity
            {
                Actions = _actionsGenerator.Get(page),
                Class = _classGenerator.Get(page),
                Links = _linksGenerator.Get(page),
                Properties = _propertiesGenerator.Get(page),
                Entities = _subEntitiesGenerator.Get(page),
                Title = _titleGenerator.Get(page)
            };
        }

        #endregion
    }
}