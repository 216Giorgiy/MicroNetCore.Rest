using System;

namespace MicroNetCore.Rest.Models.ViewModels
{
    internal sealed class ViewModelTypeBundle
    {
        public ViewModelTypeBundle(
            Type modelType,
            Type getViewModelType,
            Type postViewModelType,
            Type putViewModelType)
        {
            ModelType = modelType;
            GetViewModelType = getViewModelType;
            PostViewModelType = postViewModelType;
            PutViewModelType = putViewModelType;
        }

        public Type ModelType { get; }

        public Type GetViewModelType { get; }
        public Type PostViewModelType { get; }
        public Type PutViewModelType { get; }
    }
}