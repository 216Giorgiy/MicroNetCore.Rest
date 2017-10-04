using System.Collections.Generic;
using System.Linq;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.Models.Extensions
{
    public static class ViewModelExtensions
    {
        public static TModel ToModel<TViewModel, TModel>(this TViewModel viewModel)
            where TViewModel : class, IRequestViewModel<TModel>
            where TModel : class, IModel, new()
        {
            return (TModel) Converter.Convert<TViewModel, TModel>(viewModel);
        }

        public static IEnumerable<TModel> ToModels<TModel, TViewModel>(this IEnumerable<TViewModel> viewModels)
            where TViewModel : class, IRequestViewModel<TModel>
            where TModel : class, IModel, new()
        {
            return viewModels.Select(vm => vm.ToModel<TViewModel, TModel>());
        }

        public static IEnumerablePage<TModel> ToModelsPage<TModel, TViewModel>(this IEnumerablePage<TViewModel> page)
            where TViewModel : class, IRequestViewModel<TModel>
            where TModel : class, IModel, new()
        {
            return new EnumerablePage<TModel>(
                page.PageCount,
                page.PageIndex,
                page.PageSize,
                page.Items.Select(i => i.ToModel<TViewModel, TModel>()));
        }
    }
}