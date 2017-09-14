using MicroNetCore.Models;

namespace MicroNetCore.Rest.ViewModels
{
    public interface IViewModel
    {
    }

    public interface IViewModel<TModel> : IViewModel
        where TModel : class, IModel
    {
    }
}