using System.Net;
using System.Threading.Tasks;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Rest.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroNetCore.Rest
{
    [Route("api/[controller]")]
    public abstract class RestController<TModel> : Controller
        where TModel : class, IModel, new()
    {
        private readonly IRepository<TModel> _repository;

        // ReSharper disable once PublicConstructorInAbstractClass
        public RestController(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.Create<TModel>();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = Request.Query;

            return Request.Query.HasPaging()
                ? Ok(await _repository.FindPageAsync(query.GetPageIndex(), query.GetPageSize<TModel>()))
                : Ok(await _repository.FindAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await _repository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TModel model)
        {
            await _repository.PostAsync(model);
            return StatusCode((int) HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] TModel model)
        {
            await _repository.PutAsync(id, model);
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