using System;

namespace MicroNetCore.Rest.DataTransferObjects.ViewModels
{
    public interface IViewModelGenerator
    {
        Type CreatePostModel(Type type);
        Type CreatePutModel(Type type);
        Type CreateGetModel(Type type);
    }
}