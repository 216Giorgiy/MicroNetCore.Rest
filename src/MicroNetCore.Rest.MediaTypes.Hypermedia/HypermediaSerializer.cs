using System;
using System.Text;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Services;
using MicroNetCore.Rest.Models.RestResults;
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

        public string Serialize(IRestResult result, Encoding encoding)
        {
            var entity = CreateEntity(result);
            return JsonConvert.SerializeObject(entity, Settings);
        }

        #region Helpers

        private Entity CreateEntity(IRestResult obj)
        {
            switch (obj)
            {
                case ModelRestResult model:
                    return CreateEntity(model);
                case ModelsRestResult models:
                    return CreateEntity(models);
                case PageRestResult page:
                    return CreateEntity(page);
                default:
                    throw new Exception($"{obj.GetType().Name} can not be converted to a Hypermedia Entity.");
            }
        }

        private Entity CreateEntity(ModelRestResult model)
        {
            return new Entity
            {
                Actions = _actionsGenerator.Get(model),
                Class = _classGenerator.Get(model),
                Links = _linksGenerator.Get(model),
                Properties = _propertiesGenerator.Get(model),
                Entities = _subEntitiesGenerator.Get(model),
                Title = _titleGenerator.Get(model.Type)
            };
        }

        private Entity CreateEntity(ModelsRestResult models)
        {
            return new Entity
            {
                Actions = _actionsGenerator.Get(models),
                Class = _classGenerator.Get(models),
                Links = _linksGenerator.Get(models),
                Properties = _propertiesGenerator.Get(models),
                Entities = _subEntitiesGenerator.Get(models),
                Title = _titleGenerator.Get(models.Type)
            };
        }

        private Entity CreateEntity(PageRestResult page)
        {
            return new Entity
            {
                Actions = _actionsGenerator.Get(page),
                Class = _classGenerator.Get(page),
                Links = _linksGenerator.Get(page),
                Properties = _propertiesGenerator.Get(page),
                Entities = _subEntitiesGenerator.Get(page),
                Title = _titleGenerator.Get(page.Type)
            };
        }

        #endregion
    }
}