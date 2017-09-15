using System.Collections.Generic;
using System.Linq;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.ViewModels.Extensions
{
    public static class ModelExtensions
    {
        public static TViewModel ToViewModel<TModel, TViewModel>(this TModel model)
            where TModel : class, IModel
            where TViewModel : class, IResponseViewModel<TModel>, new()
        {
            return Converter.Convert<TViewModel, TModel>(model);
        }

        public static IEnumerable<TViewModel> ToViewModels<TModel, TViewModel>(this IEnumerable<TModel> models)
            where TModel : class, IModel
            where TViewModel : class, IResponseViewModel<TModel>, new()
        {
            return models.Select(m => m.ToViewModel<TModel, TViewModel>());
        }

        public static IEnumerablePage<TViewModel> ToViewModelsPage<TModel, TViewModel>(
            this IEnumerablePage<TModel> page)
            where TModel : class, IModel
            where TViewModel : class, IResponseViewModel<TModel>, new()
        {
            return new EnumerablePage<TViewModel>(
                page.PageCount,
                page.PageIndex,
                page.PageSize,
                page.Items.Select(i => i.ToViewModel<TModel, TViewModel>()));
        }
    }
}