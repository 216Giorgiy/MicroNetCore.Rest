using System;
using System.Collections.Generic;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaService : IHypermediaService
    {
        private readonly IHypermediaActionsGenerator _actionsGenerator;
        private readonly IHypermediaClassGenerator _classGenerator;
        private readonly IHypermediaLinksGenerator _linksGenerator;
        private readonly IHypermediaPropertiesGenerator _propertiesGenerator;
        private readonly IHypermediaSubEntitiesGenerator _subEntitiesGenerator;
        private readonly IHypermediaTitleGenerator _titleGenerator;

        public HypermediaService(
            IHypermediaActionsGenerator actionsGenerator,
            IHypermediaClassGenerator classGenerator,
            IHypermediaLinksGenerator linksGenerator,
            IHypermediaPropertiesGenerator propertiesGenerator,
            IHypermediaSubEntitiesGenerator subEntitiesGenerator,
            IHypermediaTitleGenerator titleGenerator)
        {
            _actionsGenerator = actionsGenerator;
            _classGenerator = classGenerator;
            _linksGenerator = linksGenerator;
            _propertiesGenerator = propertiesGenerator;
            _subEntitiesGenerator = subEntitiesGenerator;
            _titleGenerator = titleGenerator;
        }

        public Entity Create<TModel>(TModel model)
            where TModel : class, IModel, new()
        {
            var entity = new Entity
            {
                Actions = _actionsGenerator.Generate(model),
                Class = _classGenerator.Generate(model),
                Links = _linksGenerator.Generate(model),
                Properties = _propertiesGenerator.Generate(model),
                Entities = _subEntitiesGenerator.Generate(model),
                Title = _titleGenerator.Generate(model)
            };

            return entity;
        }

        #region Helpers

        public static bool IsSubEntityType(Type type)
        {
            return typeof(IModel).IsAssignableFrom(type) || typeof(IEnumerable<IModel>).IsAssignableFrom(type);
        }

        #endregion
    }
}