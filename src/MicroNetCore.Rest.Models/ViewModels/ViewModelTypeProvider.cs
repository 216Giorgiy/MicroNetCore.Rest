using System;
using System.Collections.Generic;

namespace MicroNetCore.Rest.Models.ViewModels
{
    public sealed class ViewModelTypeProvider : IViewModelTypeProvider
    {
        private readonly IViewModelGenerator _viewModelGenerator;
        private readonly IDictionary<Type, ViewModelTypeBundle> _viewModelTypeBundles;

        public ViewModelTypeProvider(IViewModelGenerator viewModelGenerator)
        {
            _viewModelTypeBundles = new Dictionary<Type, ViewModelTypeBundle>();
            _viewModelGenerator = viewModelGenerator;
        }

        #region Helpers

        private ViewModelTypeBundle Generate(Type model)
        {
            var getViewModel = _viewModelGenerator.CreateGetModel(model);
            var postViewModel = _viewModelGenerator.CreatePostModel(model);
            var putViewModel = _viewModelGenerator.CreatePutModel(model);

            return new ViewModelTypeBundle(model, getViewModel, postViewModel, putViewModel);
        }

        #endregion

        #region IViewModelTypeProvider

        public Type GetGetViewModel(Type type)
        {
            if (!_viewModelTypeBundles.ContainsKey(type))
                _viewModelTypeBundles.Add(type, Generate(type));

            return _viewModelTypeBundles[type].GetViewModelType;
        }

        public Type GetPostViewModel(Type type)
        {
            if (!_viewModelTypeBundles.ContainsKey(type))
                _viewModelTypeBundles.Add(type, Generate(type));

            return _viewModelTypeBundles[type].PostViewModelType;
        }

        public Type GetPutViewModel(Type type)
        {
            if (!_viewModelTypeBundles.ContainsKey(type))
                _viewModelTypeBundles.Add(type, Generate(type));

            return _viewModelTypeBundles[type].PutViewModelType;
        }

        #endregion
    }
}