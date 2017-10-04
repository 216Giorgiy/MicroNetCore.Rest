using System;

namespace MicroNetCore.Rest.Models
{
    public interface IViewModelGenerator
    {
        Type CreatePostModel(Type type);
        Type CreatePutModel(Type type);
        Type CreateGetModel(Type type);
    }
}