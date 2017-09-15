using MicroNetCore.Models;

namespace MicroNetCore.Rest.Abstractions
{
    public interface IViewModel
    {
    }

    public interface IViewModel<TModel> : IViewModel
        where TModel : class, IModel
    {
    }
}