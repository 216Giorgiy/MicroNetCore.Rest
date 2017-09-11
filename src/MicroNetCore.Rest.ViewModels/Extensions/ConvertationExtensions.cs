using System.Collections.Generic;
using System.Linq;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.ViewModels.Extensions
{
    public static class ConvertationExtensions
    {
        public static TModel ToModel<TViewModel, TModel>(this TViewModel viewModel)
            where TViewModel : class, IViewModel<TModel>, new()
            where TModel : class, IModel, new()
        {
            return Convert<TModel, TViewModel>(viewModel);
        }

        public static TViewModel ToViewModel<TModel, TViewModel>(this TModel model)
            where TModel : class, IModel, new()
            where TViewModel : class, IViewModel<TModel>, new()
        {
            return Convert<TViewModel, TModel>(model);
        }

        public static IEnumerable<TViewModel> ToViewModels<TModel, TViewModel>(this IEnumerable<TModel> models)
            where TModel : class, IModel, new()
            where TViewModel : class, IViewModel<TModel>, new()
        {
            return models.Select(m => m.ToViewModel<TModel, TViewModel>());
        }

        public static Page<TViewModel> ToViewModels<TModel, TViewModel>(this Page<TModel> page)
            where TModel : class, IModel, new()
            where TViewModel : class, IViewModel<TModel>, new()
        {
            return new Page<TViewModel>(
                page.PageCount,
                page.PageIndex,
                page.PageSize,
                page.Items.Select(i => i.ToViewModel<TModel, TViewModel>()));
        }

        #region Helpers

        private static TDestination Convert<TDestination, TSource>(TSource from)
            where TDestination : class, new()
        {
            var to = new TDestination();

            foreach (var property in to.GetType().GetProperties())
            {
                var value = from.GetType().GetProperty(property.Name).GetValue(from);

                if (value == null)
                    continue;

                property.SetValue(to, value);
            }

            return to;
        }
        
        #endregion
    }
}