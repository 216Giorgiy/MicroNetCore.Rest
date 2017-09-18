using System;

namespace MicroNetCore.Rest.DataTransferObjects.ViewModels
{
    public interface IViewModelTypeProvider
    {
        Type GetGetViewModel(Type type);
        Type GetPostViewModel(Type type);
        Type GetPutViewModel(Type type);
    }
}