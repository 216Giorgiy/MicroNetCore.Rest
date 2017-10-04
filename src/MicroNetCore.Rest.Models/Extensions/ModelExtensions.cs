using System.Collections.Generic;
using System.Linq;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.Models.Extensions
{
    public static class ModelExtensions
    {
        public static TViewModel ToViewModel<TModel, TViewModel>(this TModel model)
            where TModel : class, IEntityModel
            where TViewModel : class, IResponseViewModel<TModel>, new()
        {
            return (TViewModel) Converter.Convert<TModel, TViewModel>(model);
        }

        public static IEnumerable<TViewModel> ToViewModels<TModel, TViewModel>(this IEnumerable<TModel> models)
            where TModel : class, IEntityModel
            where TViewModel : class, IResponseViewModel<TModel>, new()
        {
            return models.Select(m => m.ToViewModel<TModel, TViewModel>());
        }

        public static IEnumerablePage<TViewModel> ToViewModelsPage<TModel, TViewModel>(
            this IEnumerablePage<TModel> page)
            where TModel : class, IEntityModel
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