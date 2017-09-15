using System;
using System.Collections.Generic;
using MicroNetCore.Models;
using MicroNetCore.Rest.DataTransferObjects;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models.Actions;
using Action = MicroNetCore.Rest.MediaTypes.Hypermedia.Models.Action;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaActionsService : IHypermediaActionsService
    {
        private readonly IHypermediaActionFormService _actionFormService;
        private readonly IApiHelper _apiHelper;

        public HypermediaActionsService(IApiHelper apiHelper, IHypermediaActionFormService actionFormService)
        {
            _apiHelper = apiHelper;
            _actionFormService = actionFormService;
        }

        #region IHypermediaActionsGenerator

        public IEnumerable<Action> Get(RestModel model)
        {
            return new[]
            {
                GetEditAction(model.Type, model.Model),
                GetDeleteAction(model.Type, model.Model)
            };
        }

        public IEnumerable<Action> Get(RestModels models)
        {
            return new[]
            {
                GetAddAction(models.Type)
            };
        }

        public IEnumerable<Action> Get(RestPage page)
        {
            return new[]
            {
                GetAddAction(page.Type)
            };
        }

        #endregion

        #region Helpers

        private Action GetAddAction(Type modelType)
        {
            return new AddAction(
                modelType,
                _apiHelper.GetUri(modelType),
                _actionFormService.GetAddForm(modelType));
        }

        private Action GetEditAction(Type modelType, IModel model)
        {
            return new EditAction(
                modelType,
                _apiHelper.GetUri(modelType, model.Id),
                _actionFormService.GetAddForm(modelType));
        }

        private Action GetDeleteAction(Type modelType, IModel model)
        {
            return new DeleteAction(
                modelType,
                _apiHelper.GetUri(modelType, model.Id));
        }

        #endregion
    }
}