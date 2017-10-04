using System;
using System.Collections.Generic;
using System.Linq;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.Models.Extensions
{
    public static class GetContentTypeExtensions
    {
        public static Type GetModelType(this IResponseViewModel viewModel)
        {
            return viewModel.GetType().GetModelType();
        }

        public static Type GetModelType(this IEnumerable<IResponseViewModel> viewModels)
        {
            return viewModels.GetType().IsArray
                ? viewModels.GetType().GetElementType().GetModelType()
                : viewModels.GetType().GetGenericArguments().First().GetModelType();
        }

        public static Type GetModelType(this IEnumerablePage<IResponseViewModel> page)
        {
            return page.GetType().GetGenericArguments().First().GetModelType();
        }

        private static Type GetModelType(this Type viewModelType)
        {
            return viewModelType
                .GetInterfaces()
                .Single(i => i.Name == nameof(IRequestViewModel) && i.ContainsGenericParameters)
                .GetGenericArguments()
                .First();
        }
    }
}