using System.Threading.Tasks;
using MicroNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroNetCore.Rest.Abstractions
{
    public interface IRestController<TModel, in TPost, in TPut>
        where TModel : class, IModel, new()
        where TPost : class, IRequestViewModel<TModel>, new()
        where TPut : class, IRequestViewModel<TModel>, new()
    {
        Task<IRestResult> Get();
        Task<IRestResult> Get(long id);

        Task<IActionResult> Post(TPost post);
        Task<IActionResult> Put(long id, TPut put);
        Task<IActionResult> Delete(long id);
    }
}