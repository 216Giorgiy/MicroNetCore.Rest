using System;
using System.Collections.Generic;
using MicroNetCore.Data.Abstractions;
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
            where TModel : class, IModel
        {
            return new Entity
            {
                Actions = _actionsGenerator.Generate(model),
                Class = _classGenerator.Generate(model),
                Links = _linksGenerator.Generate(model),
                Properties = _propertiesGenerator.Generate(model),
                Entities = _subEntitiesGenerator.Generate(model),
                Title = _titleGenerator.Generate(model)
            };
        }

        public Entity Create<TModel>(ICollection<TModel> models)
            where TModel : class, IModel
        {
            return new Entity
            {
                Actions = _actionsGenerator.Generate(models),
                Class = _classGenerator.Generate(models),
                Links = _linksGenerator.Generate(models),
                Properties = _propertiesGenerator.Generate(models),
                Entities = _subEntitiesGenerator.Generate(models),
                Title = _titleGenerator.Generate(models)
            };
        }

        public Entity Create<TModel>(IPageCollection<TModel> page)
            where TModel : class, IModel
        {
            return new Entity
            {
                Actions = _actionsGenerator.Generate(page),
                Class = _classGenerator.Generate(page),
                Links = _linksGenerator.Generate(page),
                Properties = _propertiesGenerator.Generate(page),
                Entities = _subEntitiesGenerator.Generate(page),
                Title = _titleGenerator.Generate(page)
            };
        }

        #region Helpers

        public static bool IsSubEntityType(Type type)
        {
            return typeof(IModel).IsAssignableFrom(type) || typeof(IEnumerable<IModel>).IsAssignableFrom(type);
        }

        #endregion
    }
}