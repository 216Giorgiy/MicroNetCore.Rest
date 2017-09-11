using System.Net;
using System.Threading.Tasks;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Rest.Extensions;
using MicroNetCore.Rest.ViewModels;
using MicroNetCore.Rest.ViewModels.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroNetCore.Rest
{
    [Route("api/[controller]")]
    public abstract class RestController<TModel, TGet, TPost, TPut> : Controller
        where TModel : class, IModel, new()
        where TGet : class, IViewModel<TModel>, new()
        where TPost : class, IViewModel<TModel>, new()
        where TPut : class, IViewModel<TModel>, new()
    {
        private readonly IRepository<TModel> _repository;
        
        public RestController(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.Create<TModel>();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = Request.Query;

            return query.HasPaging() ? await GetPage(query) : await GetAll();
        }

        private async Task<IActionResult> GetAll()
        {
            return Ok((await _repository.FindAsync()).ToViewModels<TModel, TGet>());
        }

        private async Task<IActionResult> GetPage(IQueryCollection queryCollection)
        {
            var index = queryCollection.GetPageIndex();
            var size = queryCollection.GetPageSize<TModel>();

            return Ok((await _repository.FindPageAsync(index, size)).ToViewModels<TModel, TGet>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var model = await _repository.GetAsync(id);
            var viewModel = model.ToViewModel<TModel, TGet>();

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TPost post)
        {
            await _repository.PostAsync(post.ToModel<TPost, TModel>());
            return StatusCode((int) HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] TPut put)
        {
            await _repository.PutAsync(id, put.ToModel<TPut, TModel>());
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}