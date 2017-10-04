using System.Net;
using System.Threading.Tasks;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.Extensions;
using MicroNetCore.Rest.Models.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroNetCore.Rest
{
    [Route("api/[controller]")]
    public abstract class RestController<TModel, TPost, TPut> : Controller, IRestController<TModel, TPost, TPut>
        where TModel : class, IEntityModel, new()
        where TPost : class, IRequestViewModel<TModel>, new()
        where TPut : class, IRequestViewModel<TModel>, new()
    {
        private readonly IRepository<TModel> _repository;

        // Required to be public to be discoverable
        public RestController(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.Create<TModel>();
        }

        #region IRestController

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = Request.Query;

            if (!query.HasPaging())
            {
                return Ok(await _repository.FindAsync());
            }

            var index = query.GetPageIndex();
            var size = query.GetPageSize<TModel>();

            return Ok(await _repository.FindPageAsync(index, size));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await _repository.GetAsync(id));
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

        #endregion
    }
}