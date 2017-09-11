using System;

namespace MicroNetCore.Rest.ViewModels
{
    public interface IViewModelGenerator
    {
        Type CreatePostModel(Type type);
        Type CreatePutModel(Type type);
        Type CreateGetModel(Type type);
    }
}