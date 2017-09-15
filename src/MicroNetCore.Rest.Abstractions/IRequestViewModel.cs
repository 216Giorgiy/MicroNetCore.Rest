using MicroNetCore.Models;

namespace MicroNetCore.Rest.Abstractions
{
    public interface IRequestViewModel : IViewModel
    {
    }

    public interface IRequestViewModel<TModel> : IViewModel<TModel>
        where TModel : class, IModel, new()
    {
    }
}