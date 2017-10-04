using System;
using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models.Actions;
using MicroNetCore.Rest.Models.Extensions;
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

        public IEnumerable<Action> Get(IResponseViewModel model)
        {
            return new[]
            {
                GetEditAction(model),
                GetDeleteAction(model.GetModelType(), model)
            };
        }

        public IEnumerable<Action> Get(IEnumerable<IResponseViewModel> models)
        {
            return new[]
            {
                GetAddAction(models.GetModelType())
            };
        }

        public IEnumerable<Action> Get(IEnumerablePage<IResponseViewModel> page)
        {
            return new[]
            {
                GetAddAction(page.GetModelType())
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

        private Action GetEditAction(IResponseViewModel model)
        {
            return new EditAction(
                model.GetModelType(),
                _apiHelper.GetUri(model),
                _actionFormService.GetEditForm(model.GetModelType()));
        }

        private Action GetDeleteAction(Type modelType, IResponseViewModel model)
        {
            return new DeleteAction(modelType, _apiHelper.GetUri(model));
        }

        #endregion
    }
}