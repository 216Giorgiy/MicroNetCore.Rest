using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models.Actions;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaActionsService : IHypermediaActionsService
    {
        private readonly IApiHelper _apiHelper;
        private readonly IHypermediaFieldService _fieldMapper;

        public HypermediaActionsService(IApiHelper apiHelper, IHypermediaFieldService fieldMapper)
        {
            _apiHelper = apiHelper;
            _fieldMapper = fieldMapper;
        }

        #region IHypermediaActionsGenerator

        public Action[] Get(IResponseViewModel obj)
        {
            return new[]
            {
                GetEditAction(obj),
                GetDeleteAction(obj)
            };
        }

        public Action[] Get(IEnumerable<IResponseViewModel> collection)
        {
            return new[]
            {
                GetAddAction()
            };
        }

        public Action[] Get(IEnumerablePage<IResponseViewModel> page)
        {
            return new Action[0];
        }

        #endregion

        #region Helpers

        private Action GetAddAction()
        {
            return new AddAction();
        }

        private Action GetEditAction(IResponseViewModel viewModel)
        {
            return new EditAction();
            //return new Action
            //{
            //    Name = $"edit-{typeof(TModel).Name}".ToLower(),
            //    Href = _apiHelper.GetUri(viewModel.GetType(), viewModel.Id),
            //    Method = HttpMethod.Put.ToString(),
            //    Title = $"Edit {typeof(TModel).Name}",
            //    Type = "application/json",
            //    Fields = GetEditForm<TModel>()
            //};
        }

        private Field[] GetEditForm<TModel>()
        {
            return typeof(TModel)
                .GetEditProperties()
                .Select(p => new Field
                {
                    Name = p.Name.Camelize(),
                    Type = _fieldMapper.Map(p.PropertyType)
                }).ToArray();
        }

        private Action GetDeleteAction(IResponseViewModel viewModel)
        {
            return new DeleteAction();
            //return new Action
            //{
            //    Name = $"delete-{typeof(TModel).Name}".ToLower(),
            //    Href = _apiHelper.GetUri(viewModel.GetType(), viewModel.Id),
            //    Method = HttpMethod.Delete.ToString(),
            //    Title = $"Delete {typeof(TModel).Name}"
            //};
        }

        #endregion
    }
}