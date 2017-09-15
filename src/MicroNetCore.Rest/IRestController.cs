using System.Threading.Tasks;
using MicroNetCore.Models;
using MicroNetCore.Rest.DataTransferObjects;
using MicroNetCore.Rest.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MicroNetCore.Rest
{
    public interface IRestController<TModel, in TPost, in TPut>
        where TModel : class, IModel, new()
        where TPost : class, IRequestViewModel<TModel>, new()
        where TPut : class, IRequestViewModel<TModel>, new()
    {
        Task<RestObject> Get();
        Task<RestObject> Get(long id);

        Task<IActionResult> Post([FromBody] TPost post);
        Task<IActionResult> Put(long id, [FromBody] TPut put);
        Task<IActionResult> Delete(long id);
    }
}