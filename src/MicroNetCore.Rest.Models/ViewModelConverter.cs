using System;
using System.Collections.Generic;
using System.Linq;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.Models
{
    public sealed class ViewModelConverter : IViewModelConverter
    {
        private delegate object ConvertationDelegate(object model);

        private static readonly IDictionary<Type, ConvertationDelegate> Cache;

        static ViewModelConverter()
        {
            Cache = new Dictionary<Type, ConvertationDelegate>();
        }

        private readonly IViewModelGenerator _viewModelGenerator;

        public ViewModelConverter(IViewModelGenerator viewModelGenerator)
        {
            _viewModelGenerator = viewModelGenerator;
        }

        public IResponseViewModel Convert(IEntityModel model)
        {
            var modelType = model.GetType();

            if (!Cache.ContainsKey(modelType))
                Cache.Add(modelType, CreateFunc(modelType));

            return (IResponseViewModel) Cache[modelType](model);
        }

        public IEnumerable<IResponseViewModel> Convert(IEnumerable<IEntityModel> models)
        {
            return models.Select(Convert);
        }

        public IEnumerablePage<IResponseViewModel> Convert(IEnumerablePage<IEntityModel> page)
        {
            return new EnumerablePage<IResponseViewModel>(
                page.PageCount,
                page.PageIndex,
                page.PageSize,
                page.Items.Select(Convert));
        }

        private ConvertationDelegate CreateFunc(Type modelType)
        {
            var viewModelType = _viewModelGenerator.CreateGetModel(modelType);
            
            var convertationDelegate = typeof(Converter)
                .GetMethods()
                .Single(m => m.Name == nameof(Converter.Convert))
                .MakeGenericMethod(modelType, viewModelType)
                .CreateDelegate(typeof(ConvertationDelegate));

            return (ConvertationDelegate) convertationDelegate;
        }
    }
}