using MicroNetCore.Models;

namespace MicroNetCore.Rest.ViewModels
{
    public interface IViewModel<TModel>
        where TModel : class, IModel, new()
    {
    }
}